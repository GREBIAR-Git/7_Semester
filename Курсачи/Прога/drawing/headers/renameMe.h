#ifndef RENAMEME_H
#define RENAMEME_H

#include <windows.h>
#include "Elements.h"
#include "g_var.h"
#include "MenuTools.h"
#include "MenuSettings.h"

void NextElem();
BOOL Line(HDC hdc, int x1, int y1, int x2, int y2);
PointD Zoom(double x,double y,RECT window);
PointD ZoomReverce(double x,double y,RECT window);
VOID UpdateWin(HWND hwnd);
VOID UpdateWin1(HWND hwnd,RECT window);
VOID ZoomRectangle(RECT window,int x1, int y1, int x2, int y2);
VOID DrawAxes(HDC memDc,RECT window);
LRESULT CALLBACK FrameWndProc(HWND hwnd, UINT Message, WPARAM wParam, LPARAM lParam);
int MenuButtonPressed(HWND hwnd, BOOL skip_processing);

#endif