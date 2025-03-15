#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // 初始化控制SDK
    m_multiControl = new HMultiControlSDK(this);
    //设置License
    m_multiControl->setLicense("c180dec8f4d94af6be5860436ca26003");
    //启动数据采集模块
    m_multiControl->lauchCollector(DATA_FIRSTGENERAL, NET_COM);

    // 初始化数据SDK
    m_dataSystemSDK = new hnnk::HDataSystem_interface();

    // 初始化TCP socket
    tcpSocket_python = new QTcpSocket(this);
    tcpSocket_game = new QTcpSocket(this);

    tcpSocket_python->connectToHost("localhost",9529);
    tcpSocket_game->connectToHost("localhost",9528);

    //tcpSocket_python->write("hello");

    // 绑定QT事件槽
    connect(m_multiControl, &HMultiControlSDK::notifyDeviceNameUpdate, this, &MainWindow::onDeviceName);
    connect(m_multiControl, &HMultiControlSDK::notifyConnectState, this, &MainWindow::onConnectChange);
    connect(m_multiControl, &HMultiControlSDK::notifyCaliTrigger, this, &MainWindow::onCaliTrigger);
    connect(m_multiControl, &HMultiControlSDK::notifyCalibrationResult, this, &MainWindow::onCalibrationResult);
    connect(m_multiControl, &HMultiControlSDK::notifyBlinkDetectionResult, this, &MainWindow::onBlinkDetectionResult);
    connect(m_multiControl, &HMultiControlSDK::notifyAttenDetectionResult, this, &MainWindow::onAttenDetectionResult);
    connect(m_multiControl ,&HMultiControlSDK::emitGyroData,this,&MainWindow::onGyroData);

    connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitEvent, this, &MainWindow::onReciveEegData);
    connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitChsAndSampRate, this, &MainWindow::onDeviceChannelAndSampRate);
    connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitAddtionData, this, &MainWindow::onAddtionData);
    connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitUpdateAmpTestInfo, this, &MainWindow::onUpdateSelfCheckInfo);

    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitDeviceName, this, &MainWindow::onDeviceActive);
    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitSearchNetDeviceOver, this, &MainWindow::onSearchNetDeviceOver);
    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitConnectChange, this, &MainWindow::onConnectState);
    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitEdfData, this, &MainWindow::onReadEdfDataToDouble);
    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitSearchNetDeviceOver, this, &MainWindow::onSearchNetDeviceOver);
    // connect(m_dataSystemSDK, &hnnk::HDataSystem_interface::emitMsgBox, this, &MainWindow::onMsg);

    // 初始化ui
    ui->labelOnnxPath->setText("ONNX Path:" + m_multiControl->getModelDir().path());

    //为方便显示陀螺仪坐标结果，设置一个虚拟光标图标
    main_vmouse = new VMouseMainWindow();
    main_vmouse->setStyleSheet("background-color:yellow");

}

MainWindow::~MainWindow()
{
    tcpSocket_game->abort();
    tcpSocket_python->abort();
    delete ui;
}

void MainWindow::on_ButtonConnect_clicked()
{
    QString deviceName = ui->listDevice->item(ui->listDevice->currentRow())->text();
    QString errMsg;

    qDebug() << "链接到设备:" << deviceName;
    m_multiControl->connectDevice(deviceName);

    //获取设备信息
    hnnk::BasicParameter devicedata = m_dataSystemSDK->getParameter();

    //启动算法检测
    //errMsg = m_multiControl->launchBlinkDetection(1,"C:/workspace/github/fakebrain/fakebrain/model_files/converted_blink_network.onnx");
    errMsg = m_multiControl->launchBlinkDetection(2);
    qDebug() << "启动算法检测:" << errMsg;

    //启动数据采集
    m_dataSystemSDK->eventDispatcher(DataAppOperator::DAO_READEEGDATA, QVariant(QVariant::Int));
}


void MainWindow::on_ButtonResetXY_clicked()
{
    qDebug("陀螺仪坐标回正");
    m_multiControl->resetLocation();
}


void MainWindow::on_ButtonRefresh_clicked()
{
    qDebug("刷新设备列表...");
    m_multiControl->searchDeviceList();
}

void MainWindow::on_ButtonLogin_clicked()
{
    QString account = ui->lineUserName->text();
    QString password = ui->lineUserPassword->text();
    QString captcha = ui->lineCodeValue->text();
    QString captchaId = ui->lineCodeID->text();

    QString msgErr;

    qDebug() << "尝试登录...";
    // msgErr = m_multiControl->registAccounter(account, password, captcha, captchaId); // 注册
    msgErr = m_multiControl->login(account, password, captcha, captchaId);           // 登录

    qDebug() << "登录结果:" << msgErr;
}

void MainWindow::on_ButtonCode_clicked()
{
    QPixmap pixmapValidateCode;
    QString stringValidateCode;

    m_multiControl->getGraphValidateCode(pixmapValidateCode,stringValidateCode);
    qDebug() << "生成验证码" << stringValidateCode;

    ui->LabelCode->setScaledContents(true);
    ui->LabelCode->setPixmap(pixmapValidateCode);
}

void MainWindow::on_SliderSensitive_valueChanged(int value)
{
    qDebug() << "更改灵敏度:" << value;
    m_multiControl->setSensitivity(value);

    std::string stringsensitive;
    stringsensitive += "灵敏度=";
    stringsensitive += std::to_string(value);
    ui->labelSensitive->setText(stringsensitive.c_str());
}







void MainWindow::onGyroData(double GyroX,double GyroY)
{

    // qDebug() << "接受到陀螺仪数据:" << GyroX << "|" << GyroY;

    std::string ss;
    ss += "1,";
    ss += std::to_string(GyroX);
    ss += ",";
    ss += std::to_string(GyroY);

    // qDebug("向TCP端口发送数据");
    tcpSocket_python->write(ss.c_str());
    //tcpSocket_python->write("123");

    int Gy = (int)GyroY;
    int Gx = (int)GyroX;

    ui->lcdGyroX->display(Gx);
    ui->lcdGyroY->display(Gy);

    main_vmouse->move(GyroX,GyroY);
}

void MainWindow::onDeviceName(QString name)
{
    qDebug() << "获取设备名称" << name;
    ui->listDevice->addItem(name);
}

void MainWindow::onConnectChange(int state)
{
    if(state == 0)
    {
        qDebug() << "设备未链接";
    }
    else
    {
        qDebug() << "设备已链接";
    }
}

void MainWindow::onBlinkDetectionResult(int val)
{
    qDebug() << "接收到眨眼数据:" << val;
}

void MainWindow::onAttenDetectionResult(double val)
{
    qDebug() << "接受到注意力数据:" << val;
    ui->lcdAtten->display(val);
}

void MainWindow::onCaliTrigger()
{
    qDebug() << "校准标识触发";
}

void MainWindow::onCalibrationResult(bool isOk, float score)
{
    qDebug() << "眨眼检测校准:" << isOk << "得分:" << score;
}






void MainWindow::onDeviceChannelAndSampRate(int channels, int srate)
{
    qDebug() << "通道数:" << channels << "srate:" << srate;
}

void MainWindow::onUpdateSelfCheckInfo(AmpTestInfo ampInfo)
{
    qDebug()<<" amp status "<<ampInfo.m_ampStatus;
    qDebug()<<" battary status "<<ampInfo.m_battaryStatus;
    qDebug()<<" gyr status "<<ampInfo.m_gyrStatus;
    qDebug()<<" blue status "<<ampInfo.m_blueStatus;
}

void MainWindow::onReciveEegData(QVector<EegDataChan> eegVec)
{
    std::vector<double> eegData;
    for(auto &it : eegVec){
        eegData.insert(eegData.end(), it.data.begin(), it.data.end());
    }

    BasicParameter parameter = m_dataSystemSDK->getParameter();

    qDebug() << "数据格式:" << parameter.m_battaryStatus << parameter.m_Magnification;
}

void MainWindow::onAddtionData(QVector<GYRODATA> gyroDatas,QVector<unsigned char>channoff, double battery)
{
    //附加数据事件
    QString gyroStr = QString("陀螺仪数据:r:%1,y:%2,p:%3").
                      arg(QString::number((double)gyroDatas[0].x_angle,'f',2), -6, '0').
                      arg(QString::number((double)gyroDatas[0].y_angle,'f',2), -6, '0').
                      arg(QString::number((double)gyroDatas[0].z_angle,'f',2), -6, '0');

    qDebug() << gyroStr;
}
