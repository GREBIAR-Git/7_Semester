#define F_CPU 1000000UL

#include <avr/io.h>
#include <stdbool.h>
#include <stdio.h>
#include <avr/interrupt.h>
#include <util/delay.h>

void nextValue(int* mode);
void result(int* mode, int* step);
void light(bool* on, int* mode, int* step, int* speed);
void Delay(int* speed);

bool on = false;

int mode = 1;
int step = 1;
int speed = 1;
bool tactMode = false;

int main(void)
{
	
	cli();
	
	DDRD = 0;
	DDRB = 0b11111111;
	EICRA = 0b11111111;
	EIMSK = 0b00001111;


	TIMSK0 = 0b00000111;
	OCR0A = 0b1010101;
	OCR0B = 0b10101010;
	TCCR0B = 0b00000101;
	TCCR0A = 0b10000011;
	
	UCSR0B = 0b10010000;
	UBRR0L = 6;
	sei();
	
	while (1)
	{
		
	}
	
}

ISR(USART0_RX_vect)
{
	int f = UDR0;
	
	if(f=='A')
	{
		on = true;	
	}
	else if(f=='B')
	{
		on = false;	
	}
	else if(f=='C')
	{
		nextValue(&mode);
	}
	else if(f=='D')
	{
		nextValue(&speed);
	}
}

ISR(TIMER0_OVF_vect)
{
	if(speed==1||speed==2)
	{
		light(&on, &mode, &step, &speed);
	}
}

ISR(TIMER0_COMPA_vect)
{
	if(tactMode)
	{
		PORTB = PORTB << 1;
		if(PORTB == 0b00001000)
		{
			PORTB = 1;
		}
	}
	else
	{
		if(speed==1)
		{
			light(&on, &mode, &step, &speed);
		}	
	}
}

ISR(TIMER0_COMPB_vect)
{
	if(speed==1||speed==2||speed==3)
	{
		light(&on, &mode, &step, &speed);
	}
}

ISR(INT0_vect)
{
	on = !on;
}

ISR(INT1_vect)
{
	nextValue(&mode);
}

ISR(INT2_vect)
{
	nextValue(&speed);
}

ISR(INT3_vect)
{
	tactMode = !tactMode;
	if(tactMode)
	{
		PORTB = 1;
		OCR0A = 0b11111110;
		TCCR0A = 0b00000010;
		OCR0B = 0b11111111;
	}
	else
	{
		PORTB = 0;
		OCR0A = 0b1010101;
		OCR0B = 0b10101010;
		TCCR0A = 0b00000000;
	}
}

void light(bool* on, int* mode, int* step, int* speed)
{
	if (*on&&!tactMode)
	{
		result(mode, step);
		nextValue(step);	
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


void nextValue(int* mode)
{
	(*mode) = (*mode) + 1;
	if ((*mode) == 4)
	{
		(*mode) = 1;
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