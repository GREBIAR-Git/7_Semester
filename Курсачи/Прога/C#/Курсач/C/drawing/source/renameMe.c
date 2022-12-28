#include "renameMe.h"

extern BOOL drawing;
extern ElementProperties currentElement;
extern Element elem[elemBufferSize];
extern int currentIndex;
extern int elemCount;
extern Display display;
extern BOOL zooming;
extern POINT p1;

extern struct menu menu;
extern char debugLine[500];

void NextElem()
{
	currentIndex++;
	elemCount++;
	if (elemCount > elemBufferSize) elemCount = elemBufferSize;
	if (currentIndex > elemCount - 1) currentIndex = 0;
}

BOOL Line(HDC hdc, int x1, int y1, int x2, int y2)
{
	MoveToEx(hdc, x1, y1, NULL);
	return LineTo(hdc, x2, y2);
}

PointD Zoom(double x,double y,RECT window)
{
	PointD center;
	center.x = (window.right-window.left - (menu.area.botRight.x - menu.area.topLeft.x))/2.0;
	center.y = (window.bottom-window.top)/2.0;
	PointD inZoom;
	inZoom.x=center.x + (center.x + display.center.x - x)*display.zoom.x;
	inZoom.y=center.y + (center.y + display.center.y - y)*display.zoom.y;
	return inZoom;
}

PointD ZoomReverce(double x,double y,RECT window)
{
	PointD center;
	center.x = (window.right-window.left - (menu.area.botRight.x - menu.area.topLeft.x))/2.0;
	center.y = (window.bottom-window.top)/2.0;
	PointD inZoom;
	inZoom.x=center.x + display.center.x + (center.x - x)/(display.zoom.x);
	inZoom.y=center.y + display.center.y + (center.y - y)/(display.zoom.y);
	return inZoom;
}

VOID UpdateWin(HWND hwnd)
{
	RECT window;  
	GetClientRect(hwnd, &window);
	window.left = 0;
	InvalidateRect(hwnd, &window, 1);
}

VOID UpdateWin1(HWND hwnd,RECT window)
{
	InvalidateRect(hwnd, &window, 1);
}

VOID ZoomRectangle(RECT window,int x1, int y1, int x2, int y2)
{
	if(fabs(x1-x2)!=0&&fabs(y1-y2)!=0)
	{
		PointD f1 = ZoomReverce(x1,y1,window);
		PointD f2 = ZoomReverce(x2,y2,window);
		display.zoom.x = fabs(display.zoom.x)*(fabs((window.right-window.left-(menu.area.botRight.x - menu.area.topLeft.x))/fabs(x1-x2)));
		display.zoom.y = fabs(display.zoom.y)*(fabs((window.bottom-window.top)/fabs(y1-y2)));
		double min = min(f1.x,f2.x);
		display.center.x=((min-(window.right-window.left - (menu.area.botRight.x - menu.area.topLeft.x))/2.0)+fabs(f1.x-f2.x)/2);
		min = min(f1.y,f2.y);
		display.center.y=(min-window.bottom/2.0)+fabs(f1.y-f2.y)/2;
	}
}

VOID DrawAxes(HDC memDc,RECT window)
{
	double w = window.right-window.left - (menu.area.botRight.x - menu.area.topLeft.x);
	double h = window.bottom-window.top;
	PointD point = Zoom(w/2.0,0,window);
	HPEN hPen = CreatePen(PS_DASH,2,RGB(0, 0, 0));
	SelectObject(memDc, hPen);
	Line(memDc, point.x, 0, point.x, h);
	point = Zoom(0,h/2.0,window);
	Line(memDc,0, point.y, w, point.y);
	DeleteObject(hPen);
}

LRESULT CALLBACK FrameWndProc(HWND hwnd, UINT Message, WPARAM wParam, LPARAM lParam) {
	switch (Message) {
	case WM_CREATE:
	{
		ShowWindow(hwnd, SW_SHOWMAXIMIZED);
		RECT window;
		GetClientRect(hwnd,&window);
		currentElement.shape = shapeLine;
		currentElement.size = 1;
		display.zoom.x=1;
		display.zoom.y=1;
		display.center.x = 0;
		display.center.y = 0;
		currentIndex = 0;
		elemCount = 1;
		UpdateWin1(hwnd,window);
		break;
	}
	case WM_LBUTTONDOWN:
	{
		if (MenuButtonPressed(hwnd, FALSE)) break;

		RECT window;
		GetClientRect(hwnd,&window);
		PointD firstPoint = ZoomReverce(LOWORD(lParam), HIWORD(lParam), window);
		elem[currentIndex].coords.point1 = firstPoint;
		elem[currentIndex].properties.shape = currentElement.shape;
		elem[currentIndex].properties.colour = currentElement.colour;
		elem[currentIndex].properties.size = currentElement.size;
		drawing = TRUE;
		break;
	}
	case WM_MOUSEMOVE:
	{
		if (!drawing) break;

		RECT window;
		GetClientRect(hwnd,&window);
		PointD secondPoint = ZoomReverce(LOWORD(lParam), HIWORD(lParam), window);
		elem[currentIndex].coords.point2 = secondPoint;
		UpdateWin1(hwnd,window);
		break;
	}
	case WM_LBUTTONUP:
	{
		if (MenuButtonPressed(hwnd, TRUE)) break;

		drawing = FALSE;
		NextElem();
		RECT window;
		GetClientRect(hwnd,&window);
		UpdateWin1(hwnd,window);
		break;
	}
	case WM_RBUTTONDOWN:
	{
		zooming = TRUE;
		p1.x = LOWORD(lParam);
		p1.y = HIWORD(lParam);
		break;
	}
	case WM_RBUTTONUP:
	{
		if (!zooming) break;

		zooming = FALSE;
		RECT window;  
		GetClientRect(hwnd, &window);
		ZoomRectangle(window, p1.x, p1.y, LOWORD(lParam), HIWORD(lParam));
		UpdateWin(hwnd);
		break;
	}
	case WM_PAINT:
	{
		PAINTSTRUCT ps;
		HDC hdc = BeginPaint(hwnd, &ps);
		HDC memDc = CreateCompatibleDC(hdc);
		RECT window;
		GetClientRect(hwnd, &window);
		HBITMAP bmp = CreateCompatibleBitmap(hdc, window.right, window.bottom);
		SelectObject(memDc, bmp);
		FillRect(memDc, &window, (HBRUSH)(RGB(255,255,255)));
		//DrawAxes(memDc, window);
		for (int i = 0; i < elemCount; i++)
		{
			double x1 = elem[i].coords.point1.x;
			double y1 = elem[i].coords.point1.y;
			double x2 = elem[i].coords.point2.x;
			double y2 = elem[i].coords.point2.y;
			PointD f1 = Zoom(x1,y1,window);
			PointD f2 = Zoom(x2,y2,window);
			if (elem[i].properties.shape == shapeLine)
			{
				HPEN hPen = CreatePen(PS_SOLID, elem[i].properties.size, elem[i].properties.colour);
				SelectObject(memDc, hPen);
				Line(memDc, f1.x, f1.y, f2.x, f2.y);
				DeleteObject(hPen);
			}
			else if (elem[i].properties.shape == shapeRectangle)
			{
				HPEN hPen = CreatePen(PS_DASH, elem[i].properties.size, elem[i].properties.colour);
				HBRUSH hBrush = CreateHatchBrush(HS_BDIAGONAL, elem[i].properties.colour);
				SelectObject(memDc, hPen);
				SelectObject(memDc, hBrush);

				Rectangle(memDc, f1.x, f1.y, f2.x, f2.y);

				DeleteObject(hBrush);
				DeleteObject(hPen);
			}
			else if (elem[i].properties.shape == shapeEllipse)
			{
				HPEN hPen = CreatePen(PS_SOLID, elem[i].properties.size, elem[i].properties.colour);
				HBRUSH hBrush = CreateSolidBrush(elem[i].properties.colour);
				SelectObject(memDc, hBrush);
				SelectObject(memDc, hPen);

				Ellipse(memDc,  f1.x, f1.y, f2.x, f2.y);

				DeleteObject(hBrush);
				DeleteObject(hPen);
			}
		}
		if(zooming)
		{
			double x1 = elem[currentIndex].coords.point2.x;
			double y1 = elem[currentIndex].coords.point2.y;
			PointD f1 = Zoom(x1,y1,window);
			HPEN hPen = CreatePen(PS_DASH,3,RGB(255, 0, 0));
			HBRUSH hBrush = GetStockObject(HOLLOW_BRUSH);
			SelectObject(memDc, hPen);
			SelectObject(memDc, hBrush);
			Rectangle(memDc, p1.x, p1.y, f1.x, f1.y);
			DeleteObject(hPen);
			DeleteObject(hBrush);
		}
		DrawUI(memDc, window);

		char str[4];
		//sprintf(str,"Zx-%lf; Zy-%lf; Cx-%lf; Cy-%lf;clientWin; X-%lu;Y-%lu;",display.zoom.x,display.zoom.y,display.center.x,display.center.y,window.right,window.bottom);
		//sprintf(str,"sizeof(Element)=%d, sizeof(elem)=%d (%d)",sizeof(Element), sizeof(elem), sizeof(elem)/1024/1024);
		//SetWindowText(hwnd, str);


		BitBlt(hdc, 0, 0, window.right, window.bottom, memDc, 0, 0, SRCCOPY);
		DeleteDC(memDc);
		DeleteObject(bmp);
		EndPaint(hwnd, &ps);
		break;
	}
	case WM_ERASEBKGND:
	{
		return 0;
	}
	case WM_KEYDOWN:
	{
		switch (wParam)
		{
			case VK_OEM_PLUS:
			{
				ButtonZoomIn(hwnd);
				break;
			}
			case VK_OEM_MINUS:
			{
				ButtonZoomOut(hwnd);
				break;
			}
			case VK_LEFT:
			{
				ButtonLeft(hwnd);
				break;
			}
			case VK_RIGHT:
			{
				ButtonRight(hwnd);
				break;
			}
			case VK_UP:
			{
				ButtonUp(hwnd);
				break;
			}
			case VK_DOWN:
			{
				ButtonDown(hwnd);
				break;
			}
			case VK_DELETE:
			{
				display.zoom.x*=1.1;
				UpdateWin(hwnd);
				break;
			}
			case VK_INSERT:
			{
				display.zoom.x/=1.1;;
				UpdateWin(hwnd);
				break;
			}
		}
		break;
	}
	case WM_DESTROY: {
		PostQuitMessage(0);
		break;
	}
	default:
		return DefWindowProc(hwnd, Message, wParam, lParam);
	}
	return 0;
}

int MenuButtonPressed(HWND hwnd, BOOL skip_processing)
{
	POINT cursorCoords;
	GetCursorPos(&cursorCoords);
	ScreenToClient(hwnd, &cursorCoords);

	if (CursorInsideArea(menu.buttons[0].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonLine();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[1].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonRect();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[2].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonEllipse();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[3].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonRed();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[4].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonGreen();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[5].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonBlue();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[6].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonWhite();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[7].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonSizePlus();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[8].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonSizeMinus();
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[9].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonZoomIn(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[10].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonZoomOut(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[11].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonLeft(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[12].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonRight(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[13].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonUp(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[14].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonDown(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[15].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonInitVC(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[16].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonVCCommit(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[17].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonVCNextCommit(hwnd);
		return 1;
	}
	else if (CursorInsideArea(menu.buttons[18].area, cursorCoords))
	{
		if (skip_processing) return 2;
		ButtonVCPrevCommit(hwnd);
		return 1;
	}
	else
	{
		return 0;
	}
}
