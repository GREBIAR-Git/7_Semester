#define F_CPU 1000000UL

#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <util/delay.h>

void nextValue(int* mode);
void result(int* mode, int* step);
void light(bool* on, int* mode, int* step, int* speed);
void Delay(int* speed);

bool on = false;

int mode = 1;
int step = 1;
int speed = 1;

int main(void)
{
	DDRD = 0;
	DDRB = 0b00000111;
	EICRA = 0b00111111;
	EIMSK = 0b00000111;
	
	sei();
	
	while (1)
	{
		light(&on, &mode, &step, &speed);
	}
	
}

ISR(INT0_vect)
{
	on = !on;
}

ISR(INT1_vect)
{
	nextValue(&mode);
}

ISR(INT2_vect)
{
	nextValue(&speed);
}

void light(bool* on, int* mode, int* step, int* speed)
{
	if (*on)
	{
		result(mode, step);
		Delay(speed);
		nextValue(step);
	}
	else
	{
		PORTB = 0;
	}
}

void Delay(int* speed)
{
	if((*speed) == 1)	
	{
		_delay_ms(400);
	}
	else if((*speed)==2)
	{
		_delay_ms(200);
	}
	else if((*speed)==3)
	{
		_delay_ms(100);
	}
}


void nextValue(int* mode)
{
	(*mode) = (*mode) + 1;
	if ((*mode) == 4)
	{
		(*mode) = 1;
	}
}

void result(int* mode, int* step)
{
	if ((*mode) == 1)
	{
		if ((*step) == 1)
		{
			PORTB = 0b00000001;
		}
		else if ((*step) == 2)
		{
			PORTB = 0b00000010;
		}
		else if ((*step) == 3)
		{
			PORTB = 0b00000100;
		}
	}
	else if ((*mode) == 2)
	{
		if ((*step) == 1)
		{
			PORTB = 0b00000111;
		}
		else if ((*step) == 2)
		{
			PORTB = 0b00000000;
		}
		else if ((*step) == 3)
		{
			PORTB = 0b00000010;
		}
	}
	else if ((*mode) == 3)
	{
		if ((*step) == 1)
		{
			PORTB = 0b00000111;
		}
		else if ((*step) == 2)
		{
			PORTB = 0b00000111;
		}
		else if ((*step) == 3)
		{
			PORTB = 0b00000111;
		}
	}
}