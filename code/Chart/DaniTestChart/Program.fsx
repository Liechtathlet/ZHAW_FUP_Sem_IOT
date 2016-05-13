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

#load "../packages/FSharp.Charting.Gtk.0.90.14/FSharp.Charting.Gtk.fsx"

open FSharp.Charting
open System
open System.Threading


let runApp = async{
    Chart.Line([ for x in 0 .. 10 -> x, x*x ]).ShowChart()
    Gtk.Application.Run()
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
