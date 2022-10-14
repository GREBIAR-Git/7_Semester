#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>
#include <util/delay.h>

void but1(bool* flagOnOff,bool* on);
void nextMode(int* mode);
void nextStep(int* step);
void result(int* mode, int* step);
void onORoff_Click(bool* flagOnOff, bool* on);
void but2(bool* flagOnOff, bool* on, int* mode, int* step, bool* flagMode);
void mode_Click(bool* flagMode, int* mode, int* step);

int main(void)
{
	DDRD = 1; // 0x
	DDRB = 0; // 0x
	PORTB = 0; // 0b
	PORTD = 0b00000111; // 0b
	bool on=false;
	bool flagOnOff = false;
	bool flagMode = false;
	int mode = 1;
	int step = 1;
	while (1)
	{
		//but1(&flagOnOff,&on);
		but2(&flagOnOff, &on, &mode, &step, &flagMode);
	}
}

void but1(bool* flagOnOff, bool* on)
{
	onORoff_Click(flagOnOff, on);
	
	if (*on)
	{
		PORTD = 1;
	}
	else
	{
		PORTD = 0;
	}
}

void but2(bool* flagOnOff, bool* on, int* mode, int* step, bool* flagMode)
{
	onORoff_Click(flagOnOff, on);
	mode_Click(flagMode, mode, step);
	
	if (*on)
	{
		result(mode, step);
		_delay_ms(1000);
		nextStep(step);
	}
	else
	{
		PORTD = 0;
	}
}

void onORoff_Click(bool* flagOnOff, bool* on)
{
	if(PINB & 1)
	{
		if ((*flagOnOff)==false)
		{
			(*flagOnOff) = true;
			(*on) = !(*on);
		}
	}
	else
	{
		(*flagOnOff) = false;
	}
}


void mode_Click(bool* flagMode, int* mode, int* step)
{
	if(PINB & 0b00000010)
	{
		if ((*flagMode)==false)
		{
			(*flagMode) = true;
			nextMode(mode);
		}
	}
	else
	{
		(*flagMode) = false;
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