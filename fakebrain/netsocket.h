#ifndef NETSOCKET_H
#define NETSOCKET_H

#include <QObject>
#include <QMutex>
#include <QTcpSocket>
#include <QHostAddress>
#include <QTimer>
//脑机AI鼠标收
class ReceiveCommand{
public:
    int program_id;
    int command_id;
    int data1;
    int data2;
};
//发给外部设备
class SendCommand {
public:
    int deviceID;          //设备ID（一代头环、二代头环或neuroscan）
    float gyroscopeX;      //陀螺仪原始数据，不是坐标
    float gyroscopeY;
    float attention;       //注意力检测结果（每秒更新一次）
    int click_state;       //眨眼检测结果（每100毫秒更新一次）
    int battery;           //头环电量
    int head_flag;         //头环脱落标识
};
class NetSocket : public QObject
{
    Q_OBJECT
public:
    typedef void (*EEGDataListener)(char  *data  ,int  len);
    static NetSocket* GetInstance();

    void connectHost(QString ip,int port);
    void disConnect();
    void readData();
    //eeg
   void sendAllData(    int program_id,
   int command_id,
   int data1,
   int data2);

public:
    static NetSocket *m_instance;
    static QMutex  m_mutex;
    bool isConnect=false;
    QTcpSocket* tcpSocket=nullptr;
    QByteArray hexStringtoByteArray(QString hex);
    QTimer timer;
    SendCommand  m_mouse_sendCmd;
    void setMouseAddress(QString ip,int port);
    QString getMouseAddress();
private:
    NetSocket(QObject *parent = nullptr);



signals:
    void signalConnectState(bool state);
    void signalMouseData(SendCommand *mouse_sendCmd);

public slots:
    void slotErrorConnection(QAbstractSocket::SocketError er);
    void slotConnectSuccess();
    void slotConnectFail();
    void onProgressReConnect();
};





#endif // ANDROIDFILEUTILS_H
