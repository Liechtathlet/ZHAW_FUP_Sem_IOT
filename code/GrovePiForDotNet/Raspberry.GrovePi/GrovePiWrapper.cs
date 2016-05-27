using System;
using System.Threading;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;

namespace Raspberry.GrovePi
{
	//http://www.dexterindustries.com/GrovePi/engineering/port-description/
	//http://www.dexterindustries.com/GrovePi/programming/grovepi-protocol-adding-custom-sensors/ 

	//Firmware: https://github.com/DexterInd/GrovePi/blob/bfcaa57bb6ce2b5c4cb0057569ea38f3574f24cf/Firmware/Source/v1.2/grove_pi_v1_2_6/README.md
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

		/// <summary>
		/// Reads the noise sensor data.
		/// </summary>
		/// <returns>The noise sensor data.</returns>
		public int readNoiseSensorData(){
			// Spec: http://www.seeedstudio.com/wiki/Grove_-_Sound_Sensor
			// Git: https://github.com/DexterInd/GrovePi/blob/57042c1e512dd973a10c07b2510beafccaf88f3f/Software/CSharp/GrovePi/Sensors/SoundSensor.cs

			// Sensor-Pin: A0

			byte[] ret;
			lock (_locker) {
				//Dummy, Action: Read, Pin: 0, Unused, Unused
				connection.Write (new[] { (byte)1, (byte)3, (byte)0, (byte)0, (byte)0 });
				Thread.Sleep (10);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (3);
			}
			//Convert to int
			return ((ret[1]*256) + ret[2]);
		}

		/// <summary>
		/// Reads the temperature and humidity sensor data.
		/// </summary>
		/// <returns>The temperature and humidity sensor data. (Temperature, Humidity)</returns>
		public Tuple<double,double> readTemperatureAndHumiditySensorData(){
			// GIT: https://github.com/DexterInd/GrovePi/blob/1c16948fc3d747eada2ac326280e7d60bd0ebb25/Software/CSharp/GrovePi/Sensors/DHTTemperatureAndHumiditySensor%20.cs
			// Spec: http://www.seeedstudio.com/wiki/Grove_-_Temperature_and_Humidity_Sensor

			// Sensor-Pin: D3

			byte[] ret;
			lock (_locker) {
				//Dummy, Action:40, Pin: 3, DHT-Model: 0, Unused
				connection.Write (new[] { (byte)1, (byte)40, (byte)3, (byte)0, (byte)0 });
				Thread.Sleep (600);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (9);
			}
			//Convert to int
			//return ((ret[1]*256) + ret[2]);
			double t = (double)BitConverter.ToSingle (ret, 1);
			double h = (double)BitConverter.ToSingle (ret, 5);
			return Tuple.Create (t, h);
		}
			/// <summary>
			/// Reads the light sensor data.
			/// </summary>
			/// <returns>The light sensor data as raw value and light intensity / resistance. (sensorValue, resistance)</returns>
		public Tuple<int,float> readLightSensorData(){
			// GIT: https://github.com/DexterInd/GrovePi/blob/bfcaa57bb6ce2b5c4cb0057569ea38f3574f24cf/Software/CSharp/GrovePi/Sensors/LightSensor.cs
			// Spec: http://www.seeedstudio.com/wiki/Grove_-_Light_Sensor

			// Sensor-Pin: A2
			byte[] ret;
			lock (_locker) {
				//Dummy, Action:Read, Pin: 2, Unused, Unused
				connection.Write (new[] { (byte)1, (byte)3, (byte)2, (byte)0, (byte)0 });
				Thread.Sleep (10);

				//Read sensor value (3 bytes, use the last 2)
				ret = connection.Read (3);
			}
			//Convert to int
			//((ret[1]*256) + ret[2]);

			int sensorValue =  ((ret[1]*256) + ret[2]);
			float lightIntensity = (float)(1023-sensorValue)*10/sensorValue;

			return Tuple.Create(sensorValue,lightIntensity);
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

