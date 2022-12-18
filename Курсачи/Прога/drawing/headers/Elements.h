#ifndef ELEMENTS_H
#define ELEMENTS_H

#include <windows.h>
#include "g_var.h"

typedef struct POINTD
{
	double x;
	double y;
}PointD;

typedef struct DISPLAY
{
	PointD zoom;
	PointD center;
}Display;

typedef struct COORDINATESS
{
	PointD point1;
	PointD point2;
}Coords;

typedef struct TYPEELEMENT
{
	int shape;
	COLORREF colour;
	int size;
}TypeElement;

typedef struct ELEMENT
{
	Coords coords;
	TypeElement typeElement;
}Element;

#endif