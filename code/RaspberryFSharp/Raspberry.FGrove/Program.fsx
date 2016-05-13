// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#r "./libs/Raspberry.GrovePi.dll"


open Raspberry.GrovePi
open System.IO
open System
open System.Threading

// Init
let grovePiWrapper = new Raspberry.GrovePi.GrovePiWrapper()

printfn "Starting data collection..."
grovePiWrapper.blink(2000)

printfn "Please provide your location:"

// Location
let currentLocation = Console.ReadLine()


// Function definitions
// ------------------------------------------------------

// Timer functions
let startTimerAndCreateObservable timerInterval =
    // Setup timer
    let timer = new System.Timers.Timer(float timerInterval)

    // Autoreset and enable
    timer.AutoReset <- true
    timer.Enabled <- true

    // Return observable event
    (timer,timer.Elapsed)


// File functions
let writeData suffix (data:String) (date:DateTime) (location:String) = 
    let filepath = "./data/SensorData-" + suffix + ".csv"

    if not(File.Exists(filepath)) then
        printfn "Data-File doesn't exists, creating file"
        use streamWriter = new StreamWriter(filepath,false)
        streamWriter.WriteLine "Date;Time;SensorData;Location"
        streamWriter.Flush()
        streamWriter.Close()

    use streamWriter = new StreamWriter(filepath,true)
    [ date.ToString("dd.MM.yyyy");
      date.ToString("hh:mm:ss.fff");
      data;
      location]
    |> List.fold (fun r s -> r + s + ";") ""
    |> streamWriter.WriteLine

    streamWriter.Flush()
    streamWriter.Close()

// Read sensor values
let readTemperatureAndHumiditySensor() =
    grovePiWrapper.readTemperatureAndHumiditySensorData()

let readLightSensor() =
    grovePiWrapper.readLightSensorData()

let readNoiseSensor() =
    grovePiWrapper.readNoiseSensorData()

// Event processors
let processTemperatureAndHumidityEvent() =
    let sensorValue = readTemperatureAndHumiditySensor()
    writeData "TH" (sensorValue.ToString()) DateTime.Now currentLocation

let processNoiseEvent() =
    let sensorValue = readNoiseSensor()
    writeData "N" (sensorValue.ToString()) DateTime.Now currentLocation

let processLightEvent() =
    let sensorValue = readLightSensor()
    writeData "L" (sensorValue.ToString()) DateTime.Now currentLocation


// Main Program
// ------------------------------------------------------

//Create TemperatureAndHumidity timer
let thTimer, thEventStream = startTimerAndCreateObservable 100 

// Subscribe to event
thEventStream |> Observable.subscribe (fun _ -> processTemperatureAndHumidityEvent())

//Create Noise timer
let nTimer, nEventStream = startTimerAndCreateObservable 100 

// Subscribe to event
nEventStream |> Observable.subscribe (fun _ -> processNoiseEvent())

//Create Light timer
let lTimer, lEventStream = startTimerAndCreateObservable 100 

// Subscribe to event
lEventStream |> Observable.subscribe (fun _ -> processLightEvent())

printfn "Starting data collection"

//Wait for termination
Console.ReadLine()
printfn "Stopping data collection"
thTimer.Stop()
nTimer.Stop()
lTimer.Stop()
Thread.Sleep(200)
printfn "Goodbye"
grovePiWrapper.blink(300)
grovePiWrapper.blink(300)

