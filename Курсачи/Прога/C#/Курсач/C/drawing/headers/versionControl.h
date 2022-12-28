#ifndef VERSIONCONTROL_H
#define VERSIONCONTROL_H

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include <stdarg.h>
#include "g_var.h"
#include "charcopy.h"

#define commitNameSize 6
#define branchNameSize 4

void initialize();
void commit();
void nextCommit();
void prevCommit();
void writeInit(FILE *f);
void writeDiff(FILE *f);

char *rand_string(char *str, size_t size);
char* rand_string_alloc(size_t size);
BOOL startsWith(const char *str, const char *starts_with);
char* escape(char* buffer);
void str_concat(char *result, int count, ...);
double toDouble(const char* s, int start, int stop);
void calculateDiff(Element * result);

#endif