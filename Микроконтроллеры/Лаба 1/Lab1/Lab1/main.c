#include <avr/io.h>
#include <util/delay.h>


int main(void)
{
    DDRB=0xFF;
	while(1)
	{
		PORTB=0b11111110;
		_delay_ms(1000);
		PORTB=0b11111111;
		_delay_ms(1000);
	}
}

