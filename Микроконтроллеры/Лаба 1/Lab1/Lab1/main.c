#define F_CPU 1000000UL
#include <avr/io.h>
#include <util/delay.h>


int main(void)
{
	while (1)
	{
		if (PINB & (1<<0))
		{
			PORTD = 0x1;
		}
		else
		{
			PORTD = 0;
		}
	}
}

