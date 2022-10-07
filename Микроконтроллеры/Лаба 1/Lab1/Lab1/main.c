#include <avr/io.h>

int main(void)
{
	DDRD = 0; // 0x
	PORTD = 1; // 0b
	PORTB = 0; // 0b
	while (1)
	{
		if (PIND & 1)
		{
			PORTB = 0;
		}
		else
		{
			PORTB = 1;
		}
	}
}

