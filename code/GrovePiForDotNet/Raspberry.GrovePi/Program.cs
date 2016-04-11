using System;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;

namespace GrovePiTest
{
	public class Program
	{

		public static void blink()
		{
			//var distance = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin2).MeasureInCentimeters();
			Console.WriteLine ("Hello, Linux");
			Console.WriteLine ("Love from c#.");

			const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
			const ConnectorPin sclPin = ConnectorPin.P1Pin05;


			using (var driver = new I2cDriver (PinMapping.ToProcessor (sdaPin), PinMapping.ToProcessor (sclPin))) {
				var deviceConnection = driver.Connect (0x04);

				try {
					Console.WriteLine ("Blink:");
					deviceConnection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)1, (byte)0 });
					Thread.Sleep (2000);
					deviceConnection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)0, (byte)0 });
				} catch (Exception e) {
					Console.WriteLine (e);
				}

				try {
					Console.WriteLine ("Get Noise Level:");
					//Dummy, Action, Pin, Data, Data
					deviceConnection.Write (new[] { (byte)1, (byte)3, (byte)0, (byte)0, (byte)0 });
					Thread.Sleep (10);

					//Read sensor value (3 bytes, use the last 2)
					byte[] ret = deviceConnection.Read(3);

					//Convert to int
					int retInt = ((ret[1]*256) + ret[2]);
					Console.WriteLine("RetInt:" + retInt);

					//Sensitivity, v0 (-48dbv)
					//Calculated with: http://www.sengpielaudio.com/calculator-db-volt.htm
					float v0 = 0.003981072f;
					Console.WriteLine("v0 : " + v0);

					//voltage range: 4-12v), Sensor data range: 0 - 1024
					float singleRatio = (8F / 1024);
					float v1 = retInt*singleRatio;
					Console.WriteLine("v1: " + v1);

					float v = (retInt*5)/1024;
					float v2 = (retInt)/1024;
					float v3 = (retInt)/1024;
					Console.WriteLine("Volt: " + v);
					Console.WriteLine("Volt 2: " + (retInt*(2000f/1024)));

					//http://electronics.stackexchange.com/questions/96205/how-to-convert-volts-in-db-spl
					double resInDb = 20*Math.Log(v / v0);
					double resInDb2 = 20*Math.Log10(v2);
					double resInDb3 = 20*Math.Log10(v3);

					Console.WriteLine("Decibel 1: " + resInDb);
					Console.WriteLine("Decibel 2: " + resInDb2);
					Console.WriteLine("Decibel 3: " + resInDb3);


					//Console.WriteLine("Output: 3" + (BitConverter.ToInt32(ret,0)));
				} catch (Exception e) {
					Console.WriteLine (e);
				}

			}

		}

		public static void Main (string[] args)
		{

			//var distance = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin2).MeasureInCentimeters();
			Console.WriteLine ("Hello, Linux");
			Console.WriteLine ("Love from c#.");

			const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
			const ConnectorPin sclPin = ConnectorPin.P1Pin05;


			using (var driver = new I2cDriver (PinMapping.ToProcessor (sdaPin), PinMapping.ToProcessor (sclPin))) {
				var deviceConnection = driver.Connect (0x04);
					
				try {
					Console.WriteLine ("Blink LED:");
					deviceConnection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)1, (byte)0 });
					Thread.Sleep (2000);
					deviceConnection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)0, (byte)0 });
				} catch (Exception e) {
					Console.WriteLine (e);
				}

				try {
					Console.WriteLine ("Get Noise Level:");
					deviceConnection.Write (new[] { (byte)1, (byte)4, (byte)0, (byte)1, (byte)0 });
					Thread.Sleep (1);
					Console.WriteLine("Output: " + deviceConnection.ReadByte());
				} catch (Exception e) {
					Console.WriteLine (e);
				}

					
			}
		}
	}
}
