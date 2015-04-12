#define RED_STREAM 3
#define GREEN_STREAM 5
#define BLUE_STREAM 6

void setup() 
{
  pinMode(RED_STREAM,OUTPUT);
  pinMode(GREEN_STREAM,OUTPUT);
  pinMode(BLUE_STREAM,OUTPUT);
  Serial.begin(115200);
}

void loop() 
{
  if (Serial.available() == 4)
  {
    if (Serial.read() == '#')
    {
      analogWrite(RED_STREAM, Serial.read());
      analogWrite(GREEN_STREAM, Serial.read());
      analogWrite(BLUE_STREAM, Serial.read());
    }
  }
}
