#I "/usr/lib/cli/gtk-sharp-2.0/"
#I "/usr/lib/cli/gdk-sharp-2.0/"
#I "/usr/lib/cli/atk-sharp-2.0/"
#I "/usr/lib/cli/glib-sharp-2.0/"
#I "bin/Debug/"

#r "gtk-sharp.dll"
#r "gdk-sharp.dll"
#r "atk-sharp.dll"
#r "glib-sharp.dll"
#r "FSharp.Charting.Gtk.dll"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"
#r "FSharp.Control.Reactive.dll"

//#INCOMPLETE_API=true
#load "../packages/FSharp.Charting.Gtk.0.90.14/FSharp.Charting.Gtk.fsx"
#load "EventEx-0.1.fsx"
//#load "../packages/FSharp.Charting.0.90.14/FSharp.Charting.fsx"
//#load "../packages/FSharp.Data.2.3.0/lib/portable-net45+netcore45/FSharp.Data.dll"

open System
open System.Drawing
open FSharp.Charting
open FSharp.Control


let runApp = async{
    let timeSeriesData = 
      [ for x in 0 .. 99 -> 
          DateTime.Now.AddDays (float x),sin(float x / 10.0) ]
    let rnd = new System.Random()
    let rand() = rnd.NextDouble()

    let data = [ for x in 0 .. 99 -> (x,x*x) ]
    let data2 = [ for x in 0 .. 99 -> (x,sin(float x / 10.0)) ]
    let data3 = [ for x in 0 .. 99 -> (x,cos(float x / 10.0)) ]
    let incData = Event.clock 10 |> Event.map (fun x -> (x, x.Millisecond))
    (**
    You can use `Event.cycle` to create a simple live data source that 
    iterates over in-memory sequence, or you can use an event as follows:
    *)
    // Cycle through two data sets of the same type
    LiveChart.LineIncremental(incData,Name="MouseMove").ShowChart()
    Gtk.Application.Run()

    ignore 0
    }

let runMain  = async{

    printfn "Starting application"
    let! app = Async.StartChild runApp

    printfn "Press any key to exit..."
    let n = Console.ReadKey()
    Gtk.Application.Quit()
    printfn "Bye..."
    }

// run the whole workflow
Async.RunSynchronously runMain  

//1. xhost +localhost
//2. fsharpi Program.fsx
    