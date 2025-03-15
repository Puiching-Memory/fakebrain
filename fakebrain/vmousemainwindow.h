#ifndef VMOUSEMAINWINDOW_H
#define VMOUSEMAINWINDOW_H

#include <QMainWindow>

namespace Ui {
 class VMouseMainWindow;
}

 class VMouseMainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit VMouseMainWindow(QWidget *parent = nullptr);
    ~VMouseMainWindow();
    void mousePressEvent(QMouseEvent *event);
    void mouseMoveEvent(QMouseEvent *event);
    void mouseReleaseEvent(QMouseEvent *event);
    bool nativeEvent(const QByteArray &eventType, void *message, long *result);
    void  setBgColor(QColor  color);
    void  setVMouseVisible(bool visible);
    void  showmouse(bool flag);
public:
    Ui::VMouseMainWindow *ui;
    QPoint m_lastPos;
    bool isPressedWidget;
    QTimer *m_pTimer;
    float m_width=0;
    float m_height=0;
};

#endif // MAINWINDOW_H
