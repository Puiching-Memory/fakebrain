#include "netsocket.h"
#include <QDir>
#include <QDebug>
#include <QSettings>
#include "qabstractsocket.h"
#include  <QMessageBox>
NetSocket::NetSocket(QObject *parent) : QObject(parent)
{

    tcpSocket = nullptr;
    QDir *qd = new QDir;
    bool exist = qd->exists("HNNK_UI/extra_ip_info.ini");
    if(exist){
        QSettings*      setting=new QSettings("HNNK_UI/extra_ip_info.ini",QSettings::IniFormat);
        QString ip=setting->value("info/ip").toString();
        QString port=setting->value("info/port").toString();
        if(ip.endsWith("0.0.0.0")==false){
        // connectHost(ip,port.toInt());
        }

    }else{
          QSettings*      setting=new QSettings("HNNK_UI/extra_ip_info.ini",QSettings::IniFormat);
        setting->setValue("info/ip","127.0.0.1");
        setting->setValue("info/port","9528");


    }
    connect(&timer,SIGNAL(timeout()),this,SLOT(onProgressReConnect()));
    timer.start(1800);
}
NetSocket* NetSocket::m_instance = nullptr;
QMutex  NetSocket::m_mutex;
NetSocket *NetSocket::GetInstance()
{
    if (m_instance == nullptr){
        QMutexLocker locker(&m_mutex);
        if (m_instance == nullptr){
            m_instance = new NetSocket();
        }
    }
    return m_instance;
}



QByteArray NetSocket::hexStringtoByteArray(QString hex)
{
    QByteArray ret;

    hex=hex.trimmed();

    QStringList sl=hex.split(" ");

    foreach(QString s,sl){
        if(!s.isEmpty())
            ret.append((char)s.toInt(0,16)&0xFF);
    }
    qDebug()<<"sendData arr"<<ret.toHex();
    return ret;
}

void NetSocket::setMouseAddress(QString ip, int port)
{
    QSettings*      setting=new QSettings("HNNK_UI/extra_ip_info.ini",QSettings::IniFormat);
  setting->setValue("info/ip",ip);
  setting->setValue("info/port",QString::number(port));

}

QString NetSocket::getMouseAddress()
{
QString address="";
QSettings*      setting=new QSettings("HNNK_UI/extra_ip_info.ini",QSettings::IniFormat);
QString ip=setting->value("info/ip").toString();
QString port=setting->value("info/port").toString();
address=ip+":"+port;
return address;
}




void NetSocket::connectHost(QString ip, int port)
{
 //   qDebug()<<"sendData connect"<<ip<<port;
    if(tcpSocket != nullptr){
        tcpSocket->disconnectFromHost();
        tcpSocket->close();
        tcpSocket=nullptr;
    }

    //分配空间，指定父对象
    tcpSocket = new QTcpSocket(this);
    disconnect(tcpSocket, SIGNAL(connected()),this,SLOT(slotConnectSuccess()));
    connect(tcpSocket, SIGNAL(connected()),this,SLOT(slotConnectSuccess()));
    disconnect(tcpSocket, SIGNAL(disconnected()),this,SLOT(slotConnectFail()));
    connect(tcpSocket,    SIGNAL(disconnected()),this,SLOT(slotConnectFail()));
    disconnect(tcpSocket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(slotErrorConnection(QAbstractSocket::SocketError)));
    connect(tcpSocket, SIGNAL(error(QAbstractSocket::SocketError)), this, SLOT(slotErrorConnection(QAbstractSocket::SocketError)));

    connect(tcpSocket, &QTcpSocket::connected,
             [=]()
             {

         emit  signalConnectState(true);
      //  QMessageBox::information(NULL, "Title", QString::fromLocal8Bit("成功和服务器建立好连接"));
                 //  qDebug()<<"sendData connect  success signalConnectState";
                  isConnect=true;
             }
             );
    if(port!=0&&ip.isEmpty()==false){
      tcpSocket->connectToHost(QHostAddress(ip), port);
       //  qDebug()<<"sendData connecting start";
    }


    connect(tcpSocket, &QTcpSocket::readyRead, this, &NetSocket::readData);
}

void NetSocket::disConnect()
{
     if(tcpSocket!=nullptr){
         tcpSocket->disconnectFromHost();
         tcpSocket->close();//这里释放连接，前面connect的时候会建立连接
     }

}

void NetSocket::readData()
{
    if(tcpSocket==nullptr || (isConnect==false)){
        return ;
    }


    QByteArray readBuff= tcpSocket->readAll();
    memcpy(&m_mouse_sendCmd,readBuff,sizeof(SendCommand));
    emit signalMouseData(&m_mouse_sendCmd);
}

void NetSocket::sendAllData(int program_id, int command_id, int data1, int data2)
{
    ReceiveCommand cmd = { program_id,  command_id,  data1,  data2};
  //  qDebug()<<"$$$$$$$$$$$$$$$the send struct data size:"<<sizeof(UICommand);

    if(tcpSocket!=nullptr&&tcpSocket->isValid()&&isConnect){

        QByteArray buff;
        buff.append((char*)&cmd, sizeof(ReceiveCommand));
            tcpSocket->write(buff,buff.size() );
            tcpSocket->flush();
          //   qDebug()<<"$$$$$$$$$$$$$$$the send struct data size:"<<flag<<buff.size();
    }
}



void NetSocket::slotErrorConnection(QAbstractSocket::SocketError er)
{
    emit  signalConnectState(false);

    isConnect=false;
}
void NetSocket::slotConnectSuccess()
{
    emit  signalConnectState(true);
  //  QMessageBox::information(NULL, "Title", "成功和服务器建立好连接");

    isConnect=true;
}

void NetSocket::slotConnectFail()
{
    emit  signalConnectState(false);

    isConnect=false;
}

void NetSocket::onProgressReConnect()
{
    if(this->isConnect==false){
        QDir *qd = new QDir;
        bool exist = qd->exists("HNNK_UI/extra_ip_info.ini");
        if(exist){
            QSettings* setting=new QSettings("HNNK_UI/extra_ip_info.ini",QSettings::IniFormat);
            QString ip=setting->value("info/ip").toString();
            QString port=setting->value("info/port").toString();
            if(ip.endsWith("0.0.0.0")==false&&ip.isEmpty()==false){
                   connectHost(ip,port.toInt());
            }
        }
    }
}
