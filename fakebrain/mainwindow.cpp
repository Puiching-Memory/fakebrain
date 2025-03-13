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
    m_multiControl->lauchCollector(DATA_SECONDGENERAL, NET_COM);

    // 初始化数据SDK
    m_dataSystemSDK = new hnnk::HDataSystem_interface();

    // 初始化TCP socket
    tcpSocket_python = new QTcpSocket(this);
    tcpSocket_game = new QTcpSocket(this);

    tcpSocket_python->connectToHost("localhost",9529);
    tcpSocket_game->connectToHost("localhost",9528);

    tcpSocket_python->write("hello");

    // 绑定QT事件槽
    connect(m_multiControl, &HMultiControlSDK::notifyDeviceNameUpdate, this, &MainWindow::onDeviceName);
    connect(m_multiControl, &HMultiControlSDK::notifyConnectState, this, &MainWindow::onConnectChange);
    connect(m_multiControl, &HMultiControlSDK::notifyCaliTrigger, this, &MainWindow::onCaliTrigger);
    connect(m_multiControl, &HMultiControlSDK::notifyCalibrationResult, this, &MainWindow::onCalibrationResult);
    connect(m_multiControl, &HMultiControlSDK::notifyBlinkDetectionResult, this, &MainWindow::onBlinkDetectionResult);
    connect(m_multiControl, &HMultiControlSDK::notifyAttenDetectionResult, this, &MainWindow::onAttenDetectionResult);
    connect(m_multiControl ,&HMultiControlSDK::emitGyroData,this,&MainWindow::onGyroData);

    // 初始化ui
    ui->labelOnnxPath->setText("ONNX Path:" + m_multiControl->getModelDir().path());

}

MainWindow::~MainWindow()
{
    tcpSocket_game->abort();
    tcpSocket_python->abort();
    delete ui;
}

void MainWindow::on_ButtonConnect_clicked()
{
    qDebug("链接到设备");
    m_multiControl->connectDevice("COM1");
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
    qDebug("尝试登录...");
}

void MainWindow::on_ButtonCode_clicked()
{
    QPixmap pixmapValidateCode;
    QString stringValidateCode;

    m_multiControl->getGraphValidateCode(pixmapValidateCode,stringValidateCode);
    qDebug() << "生成验证码" << stringValidateCode;

    pixmapValidateCode = pixmapValidateCode.scaled(ui->LabelCode->size(), Qt::KeepAspectRatio, Qt::SmoothTransformation);
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

    qDebug() << "接受到陀螺仪数据:" << GyroX << "|" << GyroY;

    std::string ss;
    ss += "1,";
    ss += std::to_string(GyroX);
    ss += ",";
    ss += std::to_string(GyroY);

    qDebug("向TCP端口发送数据");
    tcpSocket_python->write(ss.c_str());

    ui->lcdGyroX->setDigitCount(GyroX);
    ui->lcdGyroY->setDigitCount(GyroY);
}

void MainWindow::onDeviceName(QString name)
{
    qDebug() << "获取设备名称" << name;
}

void MainWindow::onConnectChange(int state)
{
    qDebug() << "设备链接状态改变:" << state;
}

void MainWindow::onBlinkDetectionResult(int val)
{
    qDebug() << "接收到眨眼数据:" << val;
}

void MainWindow::onAttenDetectionResult(double val)
{
    qDebug() << "接受到注意力数据:" << val;
    ui->lcdAtten->setDigitCount(val);
}

void MainWindow::onCaliTrigger()
{
    qDebug() << "校准标识触发";
}

void MainWindow::onCalibrationResult(bool isOk, float score)
{
    qDebug() << "眨眼检测校准:" << isOk << "得分:" << score;
}
