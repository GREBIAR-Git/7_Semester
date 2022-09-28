#include <avr/io.h>


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

