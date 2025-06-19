#include <Arduino.h>

#define BTN1P1 22
#define BTN2P1 21
#define BTN1P2 2
#define BTN2P2 15
#define XP1 25
#define YP1 32
#define XP2 14
#define YP2 27


void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(BTN1P1,INPUT_PULLUP);
  pinMode(BTN2P1,INPUT_PULLUP);
  pinMode(BTN1P2,INPUT_PULLUP);
  pinMode(BTN2P2,INPUT_PULLUP);
}

void loop() {
  delay(50);
  int buttonState1P1 = !digitalRead(BTN1P1);
  int buttonState2P1 = !digitalRead(BTN2P1);
  int buttonState1P2 = !digitalRead(BTN1P2);
  int buttonState2P2 = !digitalRead(BTN2P2);

  int x1 = analogRead(XP1);
  int y1 = analogRead(YP1);
  int x2 = analogRead(XP2);
  int y2 = analogRead(YP2);

  Serial.print("BTN1P1:");
  Serial.print(buttonState1P1);
  Serial.print(" BTN2P1:");
  Serial.print(buttonState2P1);
  Serial.print(" XP1:");
  Serial.print(x1);
  Serial.print(" YP1:");
  Serial.print(y1);

  Serial.print(" BTN1P2:");
  Serial.print(buttonState1P2);
  Serial.print(" BTN2P2:");
  Serial.print(buttonState2P2);
  Serial.print(" XP2:");
  Serial.print(x2);
  Serial.print(" YP2:");
  Serial.println(y2);
}