QT       += core gui network printsupport xml

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    main.cpp \
    mainwindow.cpp \
    vmousemainwindow.cpp

HEADERS += \
    mainwindow.h \
    vmousemainwindow.h

FORMS += \
    mainwindow.ui \
    vmousemainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target


INCLUDEPATH += $$PWD/include
LIBS += $$PWD/libs/win/debug/HCloudClient6d.lib
LIBS += $$PWD/libs/win/debug/HDataSystem6d.lib
LIBS += $$PWD/libs/win/debug/HMultiControlSDKd.lib

INCLUDEPATH += $$PWD/thirdparty_libs
LIBS += $$PWD/thirdparty_libs/onnxruntime/onnxruntime.lib
LIBS += $$PWD/thirdparty_libs/onnxruntime/onnxruntime_providers_shared.lib
LIBS += $$PWD/thirdparty_libs/onnxruntime/onnxruntime_providers_sharedd.lib
LIBS += $$PWD/thirdparty_libs/onnxruntime/onnxruntimed.lib
LIBS += $$PWD/thirdparty_libs/fftw3/libfftw3-3.lib
