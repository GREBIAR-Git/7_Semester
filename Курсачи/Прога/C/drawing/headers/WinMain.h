#ifndef WINMAIN_H
#define WINMAIN_H

#include <windows.h>

LRESULT CALLBACK FrameWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
void RegClass(WNDPROC Proc,LPCTSTR szName);

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow);

#endif