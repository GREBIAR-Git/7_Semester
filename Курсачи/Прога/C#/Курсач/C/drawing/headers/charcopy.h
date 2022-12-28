#ifndef CHARCOPY_H
#define CHARCOPY_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// example call: charcopy(&str1, &str2);  where str1 and str2 are both char*

void charCopy(char ** destination, char ** str);
void charCopyL(char ** destination, char ** str, int length);
void charConcat(char ** destination, char ** str);
void charConcatL(char ** destination, char ** str, int length);
void charConcat1(char ** destination, char ch);

#endif