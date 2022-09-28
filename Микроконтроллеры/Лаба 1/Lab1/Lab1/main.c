#include <avr/io.h>

int main(void)
{
	DDRD = 0x00;
	PORTD = 0b00000001;
	PORTB = 0b00000000;
	while (1)
	{
		if (PIND & 0b00000001)
		{
			PORTB = 0b00000000;
		}
		else
		{
			PORTB = 0b00000001;
		}
	}
}

