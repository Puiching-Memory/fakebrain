#ifndef EEGDATA_H
#define EEGDATA_H
#include <vector>
#include <iostream>
#include <cstring>
#include <cstdlib>
#include <QDebug>
#include <QString>
#include <QMetaType>
namespace hnnk
{
enum DataAppOperator
{
    DAO_UNKOWN = 0,
    DAO_CONNET,         //连接设备
    DAO_SELECT,         //查询设备信息
    DAO_READEEGDATA,         //开始渲染
    DAO_ENDREADEEGDATA,      //停止渲染
    DAO_MARK,           //开始打标
    DAO_CHECKSELF,      //自检
    DAO_MODIFYBLENAME,      //修改蓝牙名称
    DAO_MODIFYPCBAPAR,      //修改设备信息
    DAO_SAVEEDF,        //保存实时采集的脑电数据到edf文件
    DAO_ENDSAVE,        //停止保存实时脑电数据
    DAO_READEDF,        //读取静态edf文件数据
    DAO_FILTER,         //陷波
    DAO_ENDFILTER,      //停止限波
    DAO_SEARCHDEVICE,   //搜索设备
    DAO_GYROCALIBRATION,    //陀螺仪较准
    DAO_END

};

enum CONNECTUPDATE{
    CU_UNCON,
    CU_ONCON
};

/**
 * @brief The NETTYPE enum 通信协议类型
 */
enum DSNETTYPE{
    NET_UNKOWN = 0,
    NET_COM,
    NET_SYSBLUE,
    NET_TCP,
    NET_END
};

/**
 * @brief The PROTOCOLTYPE enum 数据协议类型
 */
enum DSPROTOCOLTYPE{
    DATA_UNKOWN = 0,
    DATA_FIRSTGENERAL,//一代头环
    DATA_SECONDGENERAL,//二代头环
    DATA_NUEROSCAN,
    DATA_END
};

struct EegDataChan
{
    int eventFirst = 0;
    int order = 0;
    std::vector<double> data;
};

struct UnitEog
{
    double m_gyroscopeData[3] = {0.0};
    double m_battery = 0.0;
    std::vector<unsigned char> m_chStatus;
    std::vector<EegDataChan> m_eogData;
};

struct GYRODATA{
    double x_angle = 0.0;
    double y_angle = 0.0;
    double z_angle = 0.0;
};

struct DataPoint
{
    double xAxis;
    double yAxis;

};

struct BasicParameter
{
    int m_gyroscope;            //陀螺仪的个数
    int m_samRate;              //采样率
    int m_channals;             //通道数
    int m_blockFre;             //采样点次数
    int m_chWidth;              //通道数据精度，用几个字节表示
    int m_evChannals;           //事件通道数
    int m_evWidth;              //事件数据精度，用几个字节表示
    int m_orderWidth;           //序号数据精度，用几个字节表示
    int m_chStatu;              //通道检测状态
    int m_battaryStatus;        //电池状态
    int m_blueStatus;           //蓝牙状态
    int m_gyrStatus;            //陀螺仪状态
    int m_ampStatus;            //放大器状态
    int m_Magnification;        //放大倍数
    char m_portName[64];        //串口名
    char m_version[12];         //版本号
    char m_productModel[8];     //产品型号
    bool m_isFilter;            //是否过滤

    BasicParameter():m_gyroscope(0), m_samRate(0),
                       m_channals(0), m_blockFre(0),
                       m_chWidth(0), m_evChannals(0),
                       m_evWidth(0), m_orderWidth(0),
                       m_chStatu(0), m_battaryStatus(0),
                       m_blueStatus(0), m_gyrStatus(0),
                       m_ampStatus(0),
                       m_portName{'\0'}, m_version{'\0'},
                       m_isFilter(false){

    }
};

/**
 * @brief The AmpTestInfo struct 设备自检信息返回值
 */
struct AmpTestInfo{
    int m_blueStatus;            //蓝牙状态
    int m_gyrStatus;            //陀螺仪状态
    int m_ampStatus;            //放大器状态
    int m_battaryStatus;        //电池状态
    int m_other1;               //保留字段
    int m_other2;               //保留字段
    int m_other3;               //保留字段
    char m_version[9];
};

/**
 * @brief The ConParInfo struct 参数配置结构体，
 * 可通过上位机向下位机发送命令修改下位机的参数值
 */
struct ConParInfo
{
    int m_nEegChan;
    int m_nRate;
    int m_nBlockPnts;
    int m_nMagnification;
    int m_nOther;
};


class Epoch {
public:
    int event;
    int channels;
    int epochlen = 0;
    double * data;
    int pointindex = 0;

    Epoch(int channels, int epochlen) {
        init(channels, epochlen);
    }

    void init(int channels, int epochlen)
    {
        this->channels = channels;
        this->epochlen = epochlen;

        this->event = 0;
        if (this->channels * this->epochlen > 0)
            this->data = new double[channels * epochlen];
        else
            this->data = 0;

        this->pointindex = 0;
    }

    void insertSample(double * data)
    {
        if (pointindex < epochlen) {
            for (int ch = 0; ch < channels; ch++)
            {
                this->data[pointindex * channels + ch] = data[ch];
            }
            pointindex += 1;

        }
    }

    void insertSample(double * data, const std::vector<int> &chs)
    {
        if (pointindex < epochlen) {
            for (int ch = 0; ch < channels; ch++) {
                this->data[pointindex * channels + ch] = data[chs[ch]];     //注意：有一个隐含的前提：chs[ch]的值一定是在data长度范围内，否则报错，待优化
            }
            pointindex += 1;

        }
    }
    bool isFull()
    {
        return (pointindex >= epochlen);
    }
};

}//hnnk
Q_DECLARE_METATYPE(hnnk::BasicParameter);
Q_DECLARE_METATYPE(hnnk::AmpTestInfo);
Q_DECLARE_METATYPE(hnnk::ConParInfo);
#endif // EEGDATA_H
