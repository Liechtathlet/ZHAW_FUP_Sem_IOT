using System;
//using Windows.ApplicationModel.Background;

// Add using statements to the GrovePi libraries
using GrovePi;
using GrovePi.Sensors;
//using Raspberry.IO.GeneralPurpose;
//using Raspberry.IO.GeneralPurpose.Behaviors;

namespace GrovePiTest
{
		class Program
		{
			public void otherLib(){
				// Here we create a variable to address a specific pin for output
				// There are two different ways of numbering pins--the physical numbering, and the CPU number
				// "P1Pinxx" refers to the physical numbering, and ranges from P1Pin01-P1Pin40
				//var led1 = ConnectorPin.P1Pin07.Output();

				// Here we create a connection to the pin we instantiated above
				//var connection = new GpioConnection(led1);

				/*for (var i = 0; i<100; i++) {
					// Toggle() switches the high/low (on/off) status of the pin
					connection.Toggle(led1);
					System.Threading.Thread.Sleep(250);
				}

				connection.Close();*/
			}

			public static void Main (string[] args)
			{

				//var distance = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin2).MeasureInCentimeters();
				Console.WriteLine("Hello, Linux");
				Console.WriteLine("Love from CoreCLR.");

				DeviceFactory.Build.Led (Pin.AnalogPin2).ChangeState (SensorStatus.On);
			}
		}
}
