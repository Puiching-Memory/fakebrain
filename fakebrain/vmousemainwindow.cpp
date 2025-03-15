#include "vmousemainwindow.h"
#include "ui_vmousemainwindow.h"
#include <QMouseEvent>
#include <QTimer>
#include <QDebug>
#include <windows.h>
#pragma comment(lib,"user32")


VMouseMainWindow::VMouseMainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::VMouseMainWindow)
{
    ui->setupUi(this);

    Qt::WindowFlags flags = windowFlags();
    flags |= Qt::FramelessWindowHint;
    flags |= Qt::WindowSystemMenuHint;
    flags |= Qt::WindowMinMaxButtonsHint;
    flags |= Qt::Tool;
    flags |= Qt::WindowStaysOnTopHint;
    flags |= Qt::X11BypassWindowManagerHint;
    setWindowFlags(flags);

    this->setAttribute(Qt::WA_TranslucentBackground, true);
    m_width = GetSystemMetrics(SM_CXCURSOR);//
    m_height = GetSystemMetrics(SM_CYCURSOR);//
    //禁止窗口调节大小（右下角拉伸）
    //32 20
    m_width=m_width*20/32.0;
    m_height=m_height*20/32.0;
    setFixedSize(m_width, m_height);
    show();

    m_pTimer = new QTimer();
    //m_pTimer->start(300); //程序每隔300毫秒置顶一次
    connect(m_pTimer, &QTimer::timeout, [=]{
//        SetWindowPos((HWND)this->winId(),HWND_TOPMOST,this->pos().x(),this->pos().y(),this->width(),this->height(), SWP_NOACTIVATE | SWP_SHOWWINDOW);
        QScreen* screen = QApplication::primaryScreen();
        qreal scaleFactor = screen->devicePixelRatio();
        SetWindowPos((HWND)this->winId(),HWND_TOPMOST,this->pos().x()* scaleFactor,this->pos().y()* scaleFactor,this->width()* scaleFactor,this->height()* scaleFactor, SWP_NOACTIVATE | SWP_SHOWWINDOW);

    });
    setStyleSheet("background-color:red");
    //ShowWindow((HWND)this->winId(), SW_HIDE);
}

VMouseMainWindow::~VMouseMainWindow()
{
    m_pTimer->stop();

    delete m_pTimer;
    delete ui;
}

bool VMouseMainWindow::nativeEvent(const QByteArray &eventType, void *message, long *result)
{
    MSG* msg = (MSG*)message;
    switch(msg->message)
    {
    case 10001:
        exit(0);
        return true;
    case 10002:
        m_pTimer->stop();

        this->setFixedSize(1,1);
        ShowWindow((HWND)this->winId(), SW_HIDE);
        return true;
    case 10003:
        this->setFixedSize(20,20);
        ShowWindow((HWND)this->winId(), SW_SHOW);
        m_pTimer->start(300);
        return true;
    }

    return false;
}

void VMouseMainWindow::setBgColor(QColor color)
{
    if(color==Qt::red){
        this->setStyleSheet("background-color:red");
    }
    if(color==Qt::green){
        this->setStyleSheet("background-color:green");
    }
    if(color==Qt::white){

        this->setStyleSheet("background-color:white");
    }
}

void VMouseMainWindow::setVMouseVisible(bool visible)
{
    this->setVisible(visible);
    if(visible){
        this->setMinimumHeight(m_height);
        this->setMaximumHeight(m_height);
        this->setMaximumWidth(m_width);
        this->setMinimumWidth(m_width);
    }else{
        this->setMinimumHeight(0);
        this->setMaximumHeight(0);
        this->setMaximumWidth(0);
        this->setMinimumWidth(0);
    }
}

void VMouseMainWindow::showmouse(bool flag)
{
    if(flag){
        if(!m_pTimer->isActive()){
            m_pTimer->start(300);
            ShowWindow((HWND)this->winId(), SW_SHOW);
        }
    }else{
        if(m_pTimer->isActive()){
            m_pTimer->stop();
            ShowWindow((HWND)this->winId(), SW_HIDE);
        }
    }
}

void VMouseMainWindow::mousePressEvent(QMouseEvent *event)
{
    m_lastPos = event->globalPos();
    isPressedWidget = true; // 当前鼠标按下的即是QWidget而非界面上布局的其它控件
}

void VMouseMainWindow::mouseMoveEvent(QMouseEvent *event)
{
    if(isPressedWidget)
    {
        this->move(this->x() + (event->globalX() - m_lastPos.x()),
                   this->y() + (event->globalY() - m_lastPos.y()));
    }

    m_lastPos = event->globalPos();
}

void VMouseMainWindow::mouseReleaseEvent(QMouseEvent *event)
{
    // 其实这里的mouseReleaseEvent函数可以不用重写
    m_lastPos = event->globalPos();
    isPressedWidget = false; // 鼠标松开时，置为false
}
