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
    //connect(m_multiControl, &HMultiControlSDK::notifyCaliTrigger, this, &MainWindow::onCaliTrigger);
    //connect(m_multiControl, &HMultiControlSDK::notifyCalibrationResult, this, &MainWindow::onCalibrationResult);
    connect(m_multiControl, &HMultiControlSDK::notifyBlinkDetectionResult, this, &MainWindow::onBlinkDetectionResult);
    connect(m_multiControl, &HMultiControlSDK::notifyAttenDetectionResult, this, &MainWindow::onAttenDetectionResult);
    connect(m_multiControl ,&HMultiControlSDK::emitGyroData,this,&MainWindow::onGyroData);

    //connect(this, &MainWindow::sig_ButtonRefresh_clicked, this, &MainWindow::on_ButtonRefresh_clicked);
}

MainWindow::~MainWindow()
{
    delete ui;
    tcpSocket_game->abort();
    tcpSocket_python->abort();
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
    QPixmap pixmapValidateCode;
    QString stringValidateCode;

    m_multiControl->getGraphValidateCode(pixmapValidateCode,stringValidateCode);
    qDebug("生成验证码:");
    qDebug() << stringValidateCode;
}




void MainWindow::onGyroData()
{
    qDebug("接受到陀螺仪数据");
    qDebug("向TCP端口发送数据");
    tcpSocket_python->write("hello");
}

void MainWindow::onDeviceName()
{
    qDebug("获取设备名称");
}

void MainWindow::onConnectChange()
{
    qDebug("设备链接状态改变");
}

void MainWindow::onBlinkDetectionResult()
{
    qDebug("接收到眨眼数据");
}

void MainWindow::onAttenDetectionResult()
{
    qDebug("接受到注意力数据");
}

