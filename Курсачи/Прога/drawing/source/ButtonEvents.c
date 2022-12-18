#include "ButtonEvents.h"

extern BOOL drawing;
extern TypeElement currentElement;
extern Element elem[elemBufferSize];
extern int currentIndex;
extern int elemCount;
extern Display display;
extern BOOL zooming;
extern POINT p1;

int step = 50;

ButtonLine()
{
    currentElement.shape = shapeLine;
}
ButtonRect()
{
    currentElement.shape = shapeRectangle;
}
ButtonEllipse()
{
    currentElement.shape = shapeEllipse;
}
ButtonRed()
{
    currentElement.colour = RGB(255, 0, 0);
}
ButtonGreen()
{
    currentElement.colour = RGB(0, 255, 0);
}
ButtonBlue()
{
    currentElement.colour = RGB(0, 0, 255);
}
ButtonWhite()
{
    currentElement.colour = RGB(255, 255, 255);
}
ButtonSizePlus()
{
    if (currentElement.size < 228) currentElement.size++;
}
ButtonSizeMinus()
{
    if (currentElement.size > 1) currentElement.size--;
}
ButtonZoomIn(HWND hwnd)
{
    display.zoom.x*=1.07;
    display.zoom.y*=1.07;
    UpdateWin(hwnd);
}

ButtonZoomOut(HWND hwnd)
{
    display.zoom.x/=1.07;
    display.zoom.y/=1.07;
    UpdateWin(hwnd);
}
ButtonLeft(HWND hwnd)
{
    display.center.x+=step;
    UpdateWin(hwnd);
}
ButtonRight(HWND hwnd)
{
    display.center.x-=step;
    UpdateWin(hwnd);
}
ButtonUp(HWND hwnd)
{
    display.center.y+=step;
    UpdateWin(hwnd);
}
ButtonDown(HWND hwnd)
{
    display.center.y-=step;
    UpdateWin(hwnd);
}
ButtonVersionControl(HWND hwnd)
{
    printf("hello, version control\n");
    UpdateWin(hwnd);
}