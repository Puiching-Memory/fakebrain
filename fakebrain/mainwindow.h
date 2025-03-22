#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTcpServer>
#include <QTcpSocket>
#include <QDebug>
#include <QShortcut>

#include "HMultiControlSDK.h"
#include "dataset.h"
#include "hdatasystem_interface.h"
#include "vmousemainwindow.h"

#include "windows.h"
#include <QCursor>

using namespace hnnk;

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

    // 声明对象

    HMultiControlSDK *m_multiControl;
    hnnk::HDataSystem_interface *m_dataSystemSDK;

    QTcpSocket *tcpSocket_python;
    QTcpSocket *tcpSocket_game;

    VMouseMainWindow *main_vmouse;

    int lastPosX=0,lastPosY=0;
    int focusCount = 0;
    int blink = 0;
    double atten = 0;

signals:
    // void sig_ButtonRefresh_clicked();

private slots:
    void on_ButtonConnect_clicked();
    void on_ButtonResetXY_clicked();
    void on_ButtonRefresh_clicked();
    void on_ButtonLogin_clicked();
    void on_ButtonCode_clicked();
    void on_SliderSensitive_valueChanged(int value);

    // SDK
    void onDeviceName(QString name);
    void onConnectChange(int state);
    void onCaliTrigger();
    void onCalibrationResult(bool isOk, float score);
    void onBlinkDetectionResult(int val);
    void onAttenDetectionResult(double val);
    void onGyroData(double x, double y);

    /**
     * @brief onReciveEegData 接收脑电数据
     * @param eegVec  脑电数据
     */
    void onReciveEegData(QVector<hnnk::EegDataChan> eegVec);

    /**
     * @brief onDeviceChannelAndSampRate 通道及采样率信息
     * @param chs 通道数
     * @param srate 采集率
     */
    void onDeviceChannelAndSampRate(int chs, int srate);

    /**
     * @brief onAddtionData  附加数据，陀螺仪，通道状态数据，电量数据
     * @param gyroDatas  陀螺仪
     * @param channoff  通道状态数据
     * @param battery  电量数据
     */
    void onAddtionData(QVector<GYRODATA> gyroDatas, QVector<unsigned char>channoff, double battery);

    /**
     * @brief deviceActive 搜索到的设备名信息：名称由两部分组成：name:mac
     * @param devices
     */
    //void onDeviceActive(QString devices);

    /**
     * @brief onConnectState 设备连接状态
     */
    //void onConnectState(CONNECTUPDATE);

    /**
     * @brief onUpdateSelfCheckInfo 一键诊断返回信息
     * @note 更新通道号及采样频率信息显示在界面
     */
    void onUpdateSelfCheckInfo(AmpTestInfo);

    /**
     * @brief onChooseBlueEvent 蓝牙选择
     */
    //void onChooseBlueEvent(hnnk::DataAppOperator type, QString name);

    /**
     * @brief readEdfDataToDouble 读取静态edf文件数据
     * @param dVecs 数据长度
     * @return
     */
    //void  onReadEdfDataToDouble(QVector<hnnk::DataPoint> &dVecs, QVariant &otherData, int ch);

    /**
     * @brief onSearchNetDeviceOver
     */
    //void onSearchNetDeviceOver();

private:
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H

double getDpiScale();
