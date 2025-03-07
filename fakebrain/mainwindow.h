#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
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

    HMultiControlSDK *m_multiControl;
    hnnk::HDataSystem_interface *m_dataSystemSDK;

private:
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
