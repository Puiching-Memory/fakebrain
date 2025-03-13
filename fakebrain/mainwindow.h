#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTcpServer>
#include <QTcpSocket>
#include <QDebug>

#include "HMultiControlSDK.h"
#include "dataset.h"
#include "hdatasystem_interface.h"

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

private:
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
