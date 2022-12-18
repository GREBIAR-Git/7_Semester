#ifndef BUTTONEVENTS_H
#define BUTTONEVENTS_H

#include <windows.h>
#include "g_var.h"
#include "versionControl.h"

ButtonLine();
ButtonRect();
ButtonEllipse();
ButtonRed();
ButtonGreen();
ButtonBlue();
ButtonWhite();
ButtonSizePlus();
ButtonSizeMinus();
ButtonZoomIn(HWND hwnd);

ButtonZoomOut(HWND hwnd);
ButtonLeft(HWND hwnd);
ButtonRight(HWND hwnd);
ButtonUp(HWND hwnd);
ButtonDown(HWND hwnd);
ButtonVersionControl(HWND hwnd);

#endif