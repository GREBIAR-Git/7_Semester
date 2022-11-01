#define F_CPU 1000000UL

#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <util/delay.h>

void but1(bool* flagOnOff,bool* on);
void nextMode(int* mode);
void nextStep(int* step);
void result(int* mode, int* step);
void onORoff_Click(bool* flagOnOff, bool* on);
void but2(bool* flagMode, int* mode);
void mode_Click(bool* flagMode, int* mode);
void light(bool* on, int* mode, int* step, int* speed);
void but3(bool* flagSpeed, int* speed);
void nextSpeed(int* speed);
void speed_Click(bool* flagSpeed, int* speed);
void Delay(int* speed);

bool on=false;
bool flagOnOff = false;
bool flagMode = false;
bool flagSpeed = false;
int mode = 1;
int step = 1;
int speed = 1;

int main(void)
{
	DDRD = 0; // 0x
	DDRB = 0b00000111; // 0x
	

	sei();

	while (1)
	{
		light(&on, &mode, &step, &speed);
	}
	
}

ISR(INT0_vect)
{
	but1(&flagMode,&on);
}

ISR(INT1_vect)
{
	but2(&flagMode, &mode);
}

ISR(INT2_vect){
	
	but3(&flagSpeed,&speed);
}

void light(bool* on, int* mode, int* step, int* speed)
{
	if (*on)
	{
		result(mode, step);
		//Delay(speed);
		nextStep(step);
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

void but1(bool* flagOnOff, bool* on)
{
	onORoff_Click(flagOnOff, on);
}

void but2(bool* flagMode,int* mode)
{
	mode_Click(flagMode, mode);
}

void but3(bool* flagSpeed,int* speed)
{
	speed_Click(flagSpeed, speed);
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


void mode_Click(bool* flagMode, int* mode)
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

void speed_Click(bool* flagSpeed, int* speed)
{
	if(PINB & 0b00000100)
	{
		if ((*flagSpeed)==false)
		{
			(*flagSpeed) = true;
			nextMode(speed);
		}
	}
	else
	{
		(*flagSpeed) = false;
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

void nextSpeed(int* speed)
{
	(*speed) = (*speed) + 1;
	if ((*speed) == 4)
	{
		(*speed) = 1;
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