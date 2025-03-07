#ifndef HDATASYSTEM_INTERFACE_H
#define HDATASYSTEM_INTERFACE_H

#include <QObject>
#include "dataset.h"
#include <QMetaType>
#if defined(HDATASYSTEM_LIBRARY)
#  define HDATASYSTEM_EXPORT Q_DECL_EXPORT
#else
#  define HDATASYSTEM_EXPORT Q_DECL_IMPORT
#endif

namespace hnnk
{

class HDATASYSTEM_EXPORT HDataSystem_interface : public QObject
{
    Q_OBJECT
public:
    explicit HDataSystem_interface(QObject *parent = nullptr);
    virtual ~HDataSystem_interface();

    /**
     * @brief initEegCollectorEnv 初始化采集器sdk
     */
    bool initEegDataCollectorEnv(hnnk::DSPROTOCOLTYPE type = DATA_SECONDGENERAL, hnnk::DSNETTYPE netType = NET_SYSBLUE);

    /**
     * @brief getParameter 获取本地保存的设备基本信息, 调用该函数前发起SPO_SELECTINFO 调用
     * @return 本地保存的设备基本信息
     */
    hnnk::BasicParameter getParameter();

    /**
     * @brief setEpochInfo 设置epoch参数, 在读取epoch前需要首先设置该值
     * @param eegch  通道数
     * @param eventch 事件数
     * @param epochlen 单个通道的epoch数据长度
     * @param other    预留其它的通道设置，比如：陀螺仪
     */

    void setEpochInfo(int eegch, int eventch, int epochlen, int other = 0);

    /**
     * @brief setEpochInfo 设置epoch参数, 在读取epoch前需要首先设置该值
     * @param eegchs 指定特定的通道
     * @param eventch 事件通道
     * @param epochlen 单个通道的epoch数据长度
     * @param other 预留其它的通道设置，比如：陀螺仪
     * @return
     */
    bool setEpochInfo(std::vector<int> eegchs, int eventch, int epochlen, int other = 0);

    /**
     * @brief getOneEpoch 获取一个epoch
     * @param flage true: 删除该epoch; false:不删除该epoch
     * @return  epoch指针对象
     */
    Epoch * getOneEpoch(bool flage);

    /**
     * @brief getEpochs 获取所有的epoch对象
     * @param epochs 读取到的epoch列表
     */
    void getEpoches(std::list<Epoch*> &epochs);

    /**
     * @brief removeAllEpoches 删除所有的epoch 对象
     */
    void removeAllEpoches();

    /**
     * @brief eventDispatcher 事件分发. 与头环的所有交互都通过该接口实现
     * @param type 操作类型
     * @param val 操作所需的参数
     */
    void eventDispatcher(hnnk::DataAppOperator type, QVariant val);

    /**
     * @brief saveEpochToEdf 将epoch文件保存到edf
     * @param one epoch 对象
     * @param score 本次的分数，作为一个通道处理
     */
    void saveEpochToEdf(Epoch *one, double score);

    /**
     * @brief setHightPassFilter    设置是否开启高通滤波
     * @param flag  true: 开启；false: 关闭
     */
    void setHightPassFilter(bool flag);

    /**
     * @brief setUdpAddressInfo     设置udp的地址和端口信息，本接口目前在nueroscan时要用到，用到打标过程
     * @param ipStr     ip地址
     * @param port      端口号
     */
    void setUdpAddressInfo(QString ipStr, long port);
signals:
    void emitEvent(QVector<hnnk::EegDataChan> eogVec);
    void emitMsgBox(int type, QString msg);
    void emitChsAndSampRate(int chs, int srate);
    void emitAddtionData(QVector<hnnk::GYRODATA> gyroDatas, QVector<unsigned char> channoff, double battery);
    void emitDeviceName(QString devices);
    void emitUpdateBleName(QString);        //更新蓝牙名称
    void emitUpdateAmpTestInfo(hnnk::AmpTestInfo);        //自检返回结果
    void emitConnectChange(hnnk::CONNECTUPDATE);
    void emitEdfData(QVector<hnnk::DataPoint> &, QVariant &, int);
    void emitSearchNetDeviceOver();
    void emitEvent_Ex(QVector<hnnk::EegDataChan> eogVec, QVector<hnnk::GYRODATA> gyroDatas, QVector<unsigned char> channoff, double battery);
    void emitUnpair();
    void emitGyroCalibration(int val);      //更新陀螺仪
private:
    class NetReader;
    NetReader *m_pNetReader;

};
//}

}//hnnk
#endif // HDATASYSTEM_INTERFACE_NEW_H
