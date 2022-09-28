#include <avr/io.h>

int main(void)
{
	DDRB = 0x00;
	PORTD = 0b00000000;
	PORTB = 0b00000001;
	while (1)
	{
		if (PINB & 0b00000001)
		{
			PORTD = 0b00000000;
		}
		else
		{
			PORTD = 0b00000001;
		}
	}
}

