#ifndef VERSIONCONTROL_H
#define VERSIONCONTROL_H

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include "g_var.h"
#include "charcopy.h"

#define commitNameSize 40
#define branchNameSize 20

void initialize();
void commit();
char *rand_string(char *str, size_t size);
char* rand_string_alloc(size_t size);

#endif