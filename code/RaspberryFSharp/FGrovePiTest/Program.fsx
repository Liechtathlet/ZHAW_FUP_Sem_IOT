// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#r "./libs/GrovePiTest.dll"


open GrovePiTest
open FSharp.Data
open System.IO
open System
open System.Threading

let blink = 
    printfn "Hello from F#"
    //GrovePiTest.Program.blink()
    0// return an integer exit code

type DataEntryJson = JsonProvider<"./data/base.structure.json">

let newData = DataEntryJson.Root("Mein Date",
                                 "Meine Zeit",
                                 12999)

let outFile = new StreamWriter("./Test.json")
outFile.Write(newData.JsonValue)
outFile.Flush()
outFile.Close()

let writeJsonFile

let readJsonFile
//let tryJSON =
 //   type RaspiData = JsonProvider<"./data/Raspi.Temp.Sensor.json">


let createTimerAndObservable timerInterval =
    // setup a timer
    let timer = new System.Timers.Timer(float timerInterval)
    timer.AutoReset <- true

    // events are automatically IObservable
    let observable = timer.Elapsed  

    // return an async task
    let task = async {
        timer.Start()
        do! Async.Sleep 5000
        timer.Stop()
        }

    // return a async task and the observable
    (task,observable)