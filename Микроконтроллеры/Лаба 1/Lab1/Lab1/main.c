#include <avr/io.h>
#include <util/delay.h>


int main(void)
{
    DDRB=0xFF;
	while(1)
	{
		if (PORTD == 0b11111110)
		{
			PORTB=0b11111110;
		}
		else if (PORTD == 0b11111111)
		{
			PORTB=0b11111111;
		}
	}
}

