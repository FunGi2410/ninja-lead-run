int strWheel_Pin = 34;
int accelerate_Pin = 33;

float strWheel_ADC;
float accelerate_ADC;

unsigned long int Time, preTime = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  strWheel_ADC = analogRead(strWheel_Pin);
  accelerate_ADC = analogRead (accelerate_Pin);

  Time = millis();
  if((Time - preTime) > 100){
    // int strWheel_Value = (unsigned long)(strWheel_ADC * 100 / 4095);
    // int accelerate_Value = 100 - (unsigned long)(accelerate_ADC * 100 / 4095);
    strWheel_ADC = MapFloat (strWheel_ADC, 0, 4095, -1, 1);
    accelerate_ADC = MapFloat (accelerate_ADC, 0, 4095, 0, 1);
    
    //Serial.print("Steering Wheel Angle: ");
    Serial.print(strWheel_ADC);
    Serial.print(",");
    Serial.println(accelerate_ADC);
    // Serial.print("Accelerate: ");
    // Serial.println(accelerate_Value);

    preTime = Time;
  }
}

float MapFloat(long x, long inMin, long inMax, long outMin, long outMax){
  return (float) (x - inMin) * (outMax - outMin) / (float) (inMax - inMin) + outMin;
}
