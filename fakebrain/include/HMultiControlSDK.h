#ifndef HMULTICONTROLSDK_H
#define HMULTICONTROLSDK_H


#include <QObject>
#include <QtCore/qglobal.h>
#include <QLabel>
#include <QDir>
#include "hdatasystem_interface.h"

#if defined(HMULTICONTROLSDK_LIBRARY)
#  define HMULTICONTROLSDK_EXPORT Q_DECL_EXPORT
#else
#  define HMULTICONTROLSDK_EXPORT Q_DECL_IMPORT
#endif

/*
 * 多模态综合控制模块，该模块完成眨眼检测、注意力检测和陀螺仪坐标转换等功能，眨眼检测算法有用户模型算法和大数据模型算法两种。
 * 使用步骤：
 * 1 用户登录（新用户需要先注册）。
 * 2 搜索并连接头环设备
 * 3 校准：可设置校准的眨眼时间间隔，启动校准后，用户需要跟随校准信号执行同步眨眼操作
 * 4 选择算法类别
 *    通过校准结果（分数）用户自己决定选用何种眨眼检测算法，一般0.6以上可以考虑用户模型，否则建议使用大数据模型
 * 5 启动算法检测，此时可实时获取到SDK返回的眨眼检测结果、注意力检测结果和陀螺仪坐标值
 * 6 可根据需要调整陀螺仪坐标的灵敏度，也可随时进行坐标回正操作
 * 7 不需要检测时可随时停止算法检测
 */
#define QStrErrMsg  QString

namespace hnnk
{
class HMULTICONTROLSDK_EXPORT HMultiControlSDK : public QObject
{
    Q_OBJECT
//以下为SDK接口函数
public:
    explicit HMultiControlSDK(QObject *parent = nullptr);
    virtual ~HMultiControlSDK();
    //设置License
    void setLicense(QString lincese);
    //获取图形验证码
    void getGraphValidateCode(QPixmap &lable, QString &imageId);
    //用户注册
    QStrErrMsg registAccounter(const QString accountName, const QString pwd,
                               const QString graphCode, const QString imagId);
    //用户登录
    QString login(const QString accountName, const QString pwd,
                  const QString graphCode, const QString imagId);
    //初始化数据采集模块
    void lauchCollector(DSPROTOCOLTYPE type, DSNETTYPE netType);
    //搜索设备
    void searchDeviceList();
    //连接设备
    void connectDevice(QString name);
    //陀螺仪坐标回正
    void resetLocation();
    //设置陀螺仪坐标灵敏度
    void setSensitivity(int sensitivity);
    //启动眨眼校准
    QString launchCalibration(int timeStep, bool isOnline);
    //获取眨眼检测算法用户模型所在目录
    QDir getModelDir();
    //启动算法检测
    QString launchBlinkDetection(int algoType, QString modelName ="");
    //停止算法检测
    void stopBlinkDetection();
signals:
    //SDK事件:获取眨眼检测计算结果
    void notifyBlinkDetectionResult(int);
    //SDK事件:获取注意力检测计算结果
    void notifyAttenDetectionResult(double);
    //SDK事件:网络错误
    void notifyErrorMsg(QString);
    //SDK事件:搜索到的蓝牙设备名称
    void notifyDeviceNameUpdate(QString);
    //SDK事件:设备连接状态发生改变
    void notifyConnectState(int state);
    //SDK事件:获取眨眼校准分数
    void notifyCalibrationResult(bool, float score);
    //SDK事件:校准标识触发信号
    void notifyCaliTrigger();
    //SDK事件:获取陀螺仪坐标值
    void emitGyroData(double x,double y);

//以下为内部私有成员（非接口函数）
private:
    class BlinkDetectionManager;
    BlinkDetectionManager *m_bcManager;
};
}//hnnk
#endif // HMULTICONTROLSDK_H
