#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // 初始化SDK
    m_multiControl = new HMultiControlSDK(this);
    //设置License
    m_multiControl->setLicense("c180dec8f4d94af6be5860436ca26003");
    //启动数据采集模块
    m_multiControl->lauchCollector(DATA_SECONDGENERAL, NET_COM);
}

MainWindow::~MainWindow()
{
    delete ui;
}
