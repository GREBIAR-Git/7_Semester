#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>

void but1(bool* flag,bool* on);
void nextMode(int* mode);
void nextStep(int* step);
void result(int* mode, int* step);
void onORoff(bool* flag, bool* on);

int main(void)
{
	DDRD = 1; // 0x
	DDRB = 0; // 0x
	PORTB = 0; // 0b
	PORTD = 0b00000111; // 0b
	bool on=false;
	bool flag = false;
	int mode = 1;
	int step = 1;
	while (1)
	{
		but1(&flag,&on);
	}
}

void but1(bool* flag, bool* on)
{
	onORoff(flag, on);
	
	if (*on)
	{
		PORTD = 1;
	}
	else
	{
		PORTD = 0;
	}
}

void onORoff(bool* flag, bool* on)
{
	if(PINB & 1)
	{
		if ((*flag)==false)
		{
			(*flag) = true;
			(*on) = !(*on);
		}
	}
	else
	{
		(*flag) = false;
	}
}



void nextMode(int* mode)
{
	(*mode) = (*mode) + 1;
	if ((*mode) == 4)
	{
		(*mode) = 1;
	}
}

void nextStep(int* step)
{
	(*step) = (*step) + 1;
	if ((*step) == 4)
	{
		(*step) = 1;
	}
}

void result(int* mode, int* step)
{
	if ((*mode) == 1)
	{
		if ((*step) == 1)
		{
			PORTD = 0b00000001;
		}
		else if ((*step) == 2)
		{
			PORTD = 0b00000010;
		}
		else if ((*step) == 3)
		{
			PORTD = 0b00000100;
		}
	}
	else if ((*mode) == 2)
	{
		if ((*step) == 1)
		{
			PORTD = 0b00000111;
		}
		else if ((*step) == 2)
		{
			PORTD = 0b00000000;
		}
		else if ((*step) == 3)
		{
			PORTD = 0b00000010;
		}
	}
	else if ((*mode) == 3)
	{
		if ((*step) == 1)
		{
			PORTD = 0b00000111;
		}
		else if ((*step) == 2)
		{
			PORTD = 0b00000111;
		}
		else if ((*step) == 3)
		{
			PORTD = 0b00000111;
		}
	}
}