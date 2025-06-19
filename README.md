```
___________.__       .__     __                           __                         __   
\_   _____/|__| ____ |  |___/  |_  ___________    _______/  |________   ____   _____/  |_ 
 |    __)  |  |/ ___\|  |  \   __\/ __ \_  __ \  /  ___/\   __\_  __ \_/ __ \_/ __ \   __\
 |     \   |  / /_/  >   Y  \  | \  ___/|  | \/  \___ \  |  |  |  | \/\  ___/\  ___/|  |  
 \___  /   |__\___  /|___|  /__|  \___  >__|    /____  > |__|  |__|    \___  >\___  >__|  
     \/      /_____/      \/          \/             \/                    \/     \/      
```
_Ascii Art generated with [Ascii Art Generator](http://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20)_

## About The Project

Fighter street is a parody of street fighter made on Unity, with the use of an esp32 to simulate an arcade machine.

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites
You will need to 
 - install [unity](https://unity.com/download) (version 6000.1.3f1 was used for this project.)

 - Have a [c++ compiler](https://visualstudio.microsoft.com/fr/vs/features/cplusplus/)

 - install [platformIO](https://platformio.org/)

### Installation

Clone the repo.
```
git clone https://github.com/Cheonmaa/Arcade
```

In unity hub, select "Add project from disk" open the project from the FighterStreet folder.

Setup an esp32 with two joystick and 4 buttons like the following:

![esp32 setup image accessible in repo file if link is broken](https://github.com/Cheonmaa/Arcade/image/setup.png "esp32 setup")

## Usage

Run the main.cpp on the esp32 (Port detection is automatic, if you're having trouble with the port, use COM4.)

Start the game from the unity Editor.