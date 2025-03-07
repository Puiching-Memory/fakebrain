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


    // 绑定QT事件槽
}

MainWindow::~MainWindow()
{
    delete ui;
}
