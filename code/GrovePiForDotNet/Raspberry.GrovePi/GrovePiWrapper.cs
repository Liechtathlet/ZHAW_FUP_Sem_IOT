using System;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;

namespace Raspberry.GrovePi
{
	//http://www.dexterindustries.com/GrovePi/engineering/port-description/
	//http://www.dexterindustries.com/GrovePi/programming/grovepi-protocol-adding-custom-sensors/ 

	public class GrovePiWrapper
	{
		static readonly object _locker = new object();

		const ConnectorPin sdaPin = ConnectorPin.P1Pin03;
		const ConnectorPin sclPin = ConnectorPin.P1Pin05;

		private I2cDriver driver;
		private I2cDeviceConnection connection;

		public GrovePiWrapper ()
		{
			Console.WriteLine ("Initi GrovePi wrapper on Raspberry Pi");

			driver = new I2cDriver (PinMapping.ToProcessor (sdaPin), PinMapping.ToProcessor (sclPin));
			connection = driver.Connect (0x04);

		}

		~GrovePiWrapper() 
		{
				driver.Dispose();
		}

		public int readNoiseSensorData(){
			//Sensor-Pin: A0

			byte[] ret;
			lock (_locker) {
				//Dummy, Action, Pin, Data, Data
				connection.Write (new[] { (byte)1, (byte)3, (byte)0, (byte)0, (byte)0 });
				Thread.Sleep (10);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (3);
			}
			//Convert to int
			return ((ret[1]*256) + ret[2]);
		}

		public int readTemperatureAndHumiditySensorData(){
			//Sensor-Pin: A1

			byte[] ret;
			lock (_locker) {
				//Dummy, Action, Pin, Data, Data
				connection.Write (new[] { (byte)1, (byte)3, (byte)1, (byte)0, (byte)0 });
				Thread.Sleep (10);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (3);
			}
			//Convert to int
			return ((ret[1]*256) + ret[2]);
		}
			
		public int readLightSensorData(){
			//Sensor-Pin: A2

			byte[] ret;
			lock (_locker) {
				//Dummy, Action, Pin, Data, Data
				connection.Write (new[] { (byte)1, (byte)3, (byte)2, (byte)0, (byte)0 });
				Thread.Sleep (10);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (3);
			}
			//Convert to int
			return ((ret[1]*256) + ret[2]);
		}
			
		public void blink(int ms){
			//Digital Pin: D4

			//Dummy, Action, Pin, Data, Data
			connection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)1, (byte)0 });
			Thread.Sleep (ms);
			connection.Write (new[] { (byte)1, (byte)2, (byte)4, (byte)0, (byte)0 });

		}
	}
}

