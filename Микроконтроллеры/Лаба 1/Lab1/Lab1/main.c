#include <avr/io.h>

int main(void)
{
	//DDRD = 0; // 0x
	//PORTB = 0; // 0b
	//PORTD = 1; // 0b
	while (1)
	{
		if (PINB & 1)
		{
			PORTD = 0;
		}
		else
		{
			PORTD = 1;
		}
	}
}

