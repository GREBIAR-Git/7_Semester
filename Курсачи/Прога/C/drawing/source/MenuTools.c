#include "MenuTools.h"

struct menu menu;
char debugLine[500];

int DrawDebug(HDC hdc, RECT window)
{
	RECT debugArea;
    debugArea.left = 300;
    debugArea.right = 1000;
    debugArea.bottom = 500;
    debugArea.top = debugArea.bottom - 50;
	DrawLabel(hdc, window, debugLine);
}


int CursorInsideArea(struct area area, POINT cursorCoords)
{
	if (cursorCoords.x > area.topLeft.x && cursorCoords.x < area.botRight.x && cursorCoords.y > area.topLeft.y && cursorCoords.y < area.botRight.y)
	{
		return TRUE;
	}
	return FALSE;
}

int CursorInsideRect(RECT area, POINT cursorCoords)
{
	if (cursorCoords.x > area.left && cursorCoords.x < area.right && cursorCoords.y < area.bottom && cursorCoords.y > area.top)
	{
		return TRUE;
	}
	return FALSE;
}

int DrawArea(HDC hdc, RECT area)
{
	COLORREF colorBorder = RGB(0,0,0);
	COLORREF colorFill = RGB(100,100,100);

	HPEN hPen = CreatePen(PS_SOLID, 1, colorBorder);
	SelectObject(hdc, hPen);
	HBRUSH hBrush = CreateSolidBrush(colorFill);
	SelectObject(hdc, hBrush);
	Rectangle(hdc, area.left, area.top, area.right, area.bottom);
	DeleteObject(hBrush);
	DeleteObject(hPen);
}

int DrawMenu(HDC hdc, RECT menuArea, BOOL vertical, int buttonSize, char title[100])
{
	strcpy(menu.title, title);
	
	DrawArea(hdc, menuArea);
	menu.area = RectToArea(menuArea);
	menu.vertical = vertical;
	menu.buttonSize = buttonSize;

	DrawLabel(hdc, menuArea, title);
}

int DrawMenuButton(HDC hdc, char title[100])
{
	strcpy(menu.buttons[menu.buttonsLength].title,title);

	int margin = 5;
	int marginButtonsTop = 35;

	RECT buttonArea;
	if (menu.vertical)
	{
		buttonArea.left = menu.area.topLeft.x + margin;
		buttonArea.right = menu.area.botRight.x - margin;
		buttonArea.top = menu.buttonsLength * (menu.buttonSize + 2 * margin) + marginButtonsTop;
		buttonArea.bottom = buttonArea.top + menu.buttonSize;
	}
	else
	{
		buttonArea.top = menu.area.topLeft.y + margin;
		buttonArea.bottom = menu.area.botRight.y - margin;
		buttonArea.left = menu.buttonsLength * (menu.buttonSize + 2 * margin) + margin;
		buttonArea.right = buttonArea.left + menu.buttonSize;
	}
	DrawArea(hdc, buttonArea);
	menu.buttons[menu.buttonsLength].area = RectToArea(buttonArea);
	//printf("ButtonArea[%15d]: %15d%15d%15d%15d\n", menu.buttonsLength, menu.buttons[menu.buttonsLength].area.topLeft.x, menu.buttons[menu.buttonsLength].area.topLeft.y, menu.buttons[menu.buttonsLength].area.botRight.x, menu.buttons[menu.buttonsLength].area.botRight.y);
	menu.buttonsLength ++;

	DrawLabel(hdc, buttonArea, title);
}

struct area RectToArea(RECT rect)
{
	struct area area;
	area.topLeft.x = rect.left;
	area.topLeft.y = rect.top;
	area.botRight.x = rect.right;
	area.botRight.y = rect.bottom;

	return area;
}

int DrawLabel(HDC hdc, RECT area, char* text)
{
	int marginTop = 5;

	HPEN hPen = CreatePen(PS_SOLID, 1, RGB(0,0,0));
	SelectObject(hdc, hPen);
	TextOut(hdc, area.left, area.top + marginTop, text, strlen(text));
	DeleteObject(hPen);
}