#ifndef MENUTOOLS_H
#define MENUTOOLS_H

#include <windows.h>

struct area
{
	POINT topLeft;
	POINT botRight;
};

struct button
{
	char title[100];
	struct area area;
};

struct menu
{
	char title[100];
	struct area area;
	struct button buttons[100];
	BOOL vertical;
	int buttonSize;
	int buttonsLength;
};

int CursorInsideArea(struct area area, POINT cursorCoords);
int CursorInsideRect(RECT area, POINT cursorCoords);
int DrawArea(HDC hdc, RECT area);
int DrawMenu(HDC hdc, RECT menuArea, BOOL vertical, int buttonSize, char title[100]);
int DrawMenuButton(HDC hdc, char title[100]);
struct area RectToArea(RECT rect);
int DrawLabel(HDC hdc, RECT area, char* text);
int DrawDebug(HDC hdc, RECT window);

#endif