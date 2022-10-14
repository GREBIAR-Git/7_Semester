#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>

void but1(bool*,bool*);

int main(void)
{
	DDRD = 1; // 0x
	DDRB = 0; // 0x
	PORTB = 0; // 0b
	PORTD = 1; // 0b
	bool on=false;
	bool flag = false;
	while (1)
	{
		but1(&flag,&on);
	}
}

void but1(bool* flag, bool* on)
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
	
	if (*on)
	{
		PORTD = 1;
	}
	else
	{
		PORTD = 0;
	}
}

