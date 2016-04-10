using System;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;

namespace GrovePiTest
{
		class Program
		{

			public static void Main (string[] args)
			{

				//var distance = DeviceFactory.Build.UltraSonicSensor(Pin.DigitalPin2).MeasureInCentimeters();
				Console.WriteLine("Hello, Linux");
				Console.WriteLine("Love from c#.");

				const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
				const ConnectorPin sclPin = ConnectorPin.P1Pin05;


			using (var driver = new I2cDriver(PinMapping.ToProcessor(sdaPin), PinMapping.ToProcessor(sclPin)))
				{
					var deviceConnection =  driver.Connect(0x04);
					
				/*try{
					Console.WriteLine("1:");
					deviceConnection.Write (new[] {(byte) 4, (byte) 1, (byte) 0});
					Thread.Sleep(500);
					deviceConnection.Write (new[] {(byte) 4, (byte) 0, (byte) 0});
				}catch (Exception e){
					Console.WriteLine(e);
				}

				Thread.Sleep(1000);*/

				try{
					Console.WriteLine("2:");
					deviceConnection.Write (new[] {(byte) 1, (byte) 2, (byte) 4, (byte) 1, (byte) 0});
					Thread.Sleep(2000);
					deviceConnection.Write (new[] {(byte) 1, (byte) 2, (byte) 4, (byte) 0, (byte) 0});
				}catch (Exception e){
					Console.WriteLine (e);
				}

				/*try{
					Console.WriteLine("3:");
					deviceConnection.Write (new[] {(byte) 1, (byte) 4, (byte) 1, (byte) 0});
					Thread.Sleep(500);
					deviceConnection.Write (new[] {(byte) 1, (byte) 4, (byte) 0, (byte) 0});
				}catch (Exception e){
					Console.WriteLine (e);
				}

				try{
					Console.WriteLine("4:");
					deviceConnection.Write (new[] {(byte) 2, (byte) 4, (byte) 1, (byte) 0});
					Thread.Sleep(500);
					deviceConnection.Write (new[] {(byte) 2, (byte) 4, (byte) 0, (byte) 0});
				}catch (Exception e){
					Console.WriteLine (e);
				}*/


					//Thread.Sleep(1000);
					
				}
			}
		}
}
