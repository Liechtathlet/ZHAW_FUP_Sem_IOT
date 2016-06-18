
#I "/usr/lib/cli/gtk-sharp-2.0/"
#I "/usr/lib/cli/gdk-sharp-2.0/"
#I "/usr/lib/cli/atk-sharp-2.0/"
#I "/usr/lib/cli/glib-sharp-2.0/"
#I "bin/Debug/"
#I "../packages/Rx-Linq.2.2.5/lib/net40/"

#r "FSharp.Charting.dll"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"
#r "FSharp.Control.Reactive.dll"
#r "System.Reactive.Linq.dll"

//#INCOMPLETE_API=true
#load "../packages/FSharp.Charting.Gtk.0.90.14/FSharp.Charting.Gtk.fsx"
//#load "../packages/FSharp.Charting.0.90.14/FSharp.Charting.fsx"
//#load "../packages/FSharp.Data.2.3.0/lib/portable-net45+netcore45/FSharp.Data.dll"

open FSharp.Charting
open System
open System.Threading
open FSharp.Data

type SensorData = CsvProvider<"SensorData-L.csv", ";">

let chunk size list =
    let chunkedValues = list |> Seq.chunkBySize size
    //chunkedValues |> Seq.map (fun r -> [r |> Seq.head |> fst, r |> Seq.averageBy (fun e -> e |> snd |> float)])
    chunkedValues |> Seq.map (fun r -> r |> Seq.averageBy (fun e -> e |> float))

let format (sensorData:SensorData) =
    //let values = sensorData.Rows |> Seq.map (fun r -> (r.MyIndex, r.SensorData))
    let values = sensorData.Rows |> Seq.map (fun r -> r.SensorData)
    chunk 1000 values

let runApp = 

    //let noise = SensorData.Load("SensorData-N.csv")
    //let firstRow = noise.Rows |> Seq.head
    //let value = firstRow.Date

    //let t = firstRow.SensorData
    //printfn "%i" t


    printfn "Load Data"
    let light = SensorData.Load("SensorData-L.csv")
    //let updater = fun x -> [(fst x, snd x)]
    printfn "Preparing data"
    let lightValues = format light
    //let observableLightValues = FSharp.Control.Reactive.Observable.toObservable lightValues
    //observableLightValues.Add updater

    //let noiseValues = format noise
    printfn "Setup chart"
    let chart = Chart.Line lightValues
    //Chart.Combine(
    //    [ Chart.Line(lightValues, Name="Light")
    //      Chart.Line(noiseValues, Name="Noise")]).ShowChart()
    Gtk.Application.Run()
    printfn "Chart displayed"

    //System.Threading.Thread.Sleep 3000
    //let lightValue2 = Seq.append lightValues [ (121000, float 700) ]

    ignore 0


printfn "Starting application"
runApp
printfn "Press any key to exit..."
let n = Console.ReadKey()
Gtk.Application.Quit()
printfn "Bye..."

