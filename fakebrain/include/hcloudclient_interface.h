#ifndef HCLOUDCLIENT_INTERFACE_H
#define HCLOUDCLIENT_INTERFACE_H

#include <QObject>

#include <QtNetwork/QNetworkReply>
#include <QEventLoop>

#if defined(HCLOUDCLIENT_LIBRARY)
#  define HCLOUDCLIENT_EXPORT Q_DECL_EXPORT
#else
#  define HCLOUDCLIENT_EXPORT Q_DECL_IMPORT
#endif

namespace hnnk
{
class HCLOUDCLIENT_EXPORT HCloudClient_Interface : public QObject
{
    Q_OBJECT
public:
    explicit HCloudClient_Interface(QObject *parent = nullptr);

    //附加数据,QString fileKey:如果有要上传文件类型，需要指定fileKey的值，这个值必须是val中的键值
    void addtionData(QString url, QString license, QMap<QString, QVariant> val, int caseType, QString fileKey = "");

    //首次上传
    void firstUploadData(QString url, QString license, QMap<QString, QVariant> val, int caseType);

    //图形码
    void graphVerificationCode(QString url, int caseType);

    //登录
    void login(QString url, QMap<QString, QVariant> val, int caseType);

    //模型标定
    void modelCalibration(QString url, QString license, QMap<QString, QVariant> val, int caseType, QString fileKey);

    //模型下载
    void modelDownload(QString url, QString license, QMap<QString, QVariant> val, int caseType);

    //模型查询
    void modelSelect(QString url, QString license, QMap<QString, QVariant> val, int caseType);

    //查询指定用户的模型信息
    void modelSelectByUser(QString url, QString license, QMap<QString, QVariant> val, int caseType);

    //忘记密码
    void modifyPassword(QString url, QMap<QString, QVariant> val, int caseType);

    //上传脑电
    void uploadEegdata(QString url, QString license, QMap<QString, QVariant> val, int caseType, QString fileKey);

    //用户信息 setuser:0 获取用户信息；1 修改用户信息
    void userInfo(QString url, QString license, QMap<QString, QVariant> val, int caseType, int setuser = 0);

    //注册用户时用户名和密码至少6位字符或数字
    void userRegister(QString url, QMap<QString, QVariant> val, int caseType);

    //绑定手机号
    void bindPhonNumber(QString url, QMap<QString, QVariant> val, int caseType);

    //脑电片段计算
    void eegSliceCalculate(QString url, QString license, QMap<QString, QVariant> val, int caseType);

    //通过用户名获取用户id和绑定手机号
    void accountGetIdPhone(QString url, QMap<QString, QVariant> val, int caseType);

    /**
     * @brief bindPhoneVerificationCode     获取绑定手机号的图片验证码
     * 返回值QMap<QString, QVariant> result; 其中key == data时，QVariant 保存为QMap<QString, QVariant> == QMap<QString, QMap<QString, QString>>
     * @param url       请求url
     * @param val       请求参数
     * @param caseType  场景类型
     */
    void bindPhoneVerificationCode(QString url, QMap<QString, QVariant> val, int caseType);

    /**
     * @brief modifyPwdPhoneVerificationCode    获取通过手机号修改密码的验证码
     * 返回值QMap<QString, QVariant> result; 其中key == data时，QVariant 保存为bool, true or false, 表示成功或失败
     * @param url       请求url
     * @param val       请求参数
     * @param caseType  场景类型
     */
    void modifyPwdPhoneVerificationCode(QString url, QMap<QString, QVariant> val, int caseType);

    /**
     * @brief addtionDataForConsciousness 上传意识障碍附加数据.
     * 返回值QMap<QString, QVariant> result; 其中key == data时，QVariant 保存为bool, true or false, 表示成功或失败
     * @param url       请求url
     * @param license       license
     * @param val       请求参数
     * @param caseType      场景类型
     * @param fileKey       标识本次需要上传文件， 文件在参数中对应的key字段
     */
    void addtionDataForConsciousness(QString url, QString license, QMap<QString, QVariant> val, int caseType, QString fileKey);

    /**
     * @brief checkoutEnumInfo      获取指定类型的枚举值, 返回值QMap<QString, QVariant> result;
     * 其中key == data时，QVariant 保存的是QMap<QString, QString> . key : enumvalue
     * @param url   请求url
     * @param val   请求参数
     * @param caseType      场景类型，可为空
     */
    void checkoutEnumInfo(QString url, QMap<QString, QVariant> val, int caseType);
signals:
    /**
     * @brief notifyEvent
     * @param result 有四个key值，其中key == error、key == message 时QVariant 保存QString类型数据值;
     * key == data 时，QVariant 保存的值根据接口不同会有不一样;
     * key == case 时，表示场景值，或者调用者在调用接口时传入的caseType 值,为整形;
     */
    void notifyEvent(QMap<QString, QVariant> result);
    void notifyError(QNetworkReply::NetworkError,int caseType);
    void notifyProgress(qint64, qint64);

};
}//hnnk
#endif // HCLOUDCLIENT_INTERFACE_H
