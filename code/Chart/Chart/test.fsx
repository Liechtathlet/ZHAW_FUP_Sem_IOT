//#I "../packages/gtk-sharp.Linux.3.14.3.14.7/lib/net40"
//#I "../packages/FSharp.Charting.Gtk.0.90.14"
//#load "FSharp.Charting.Gtk.fsx"
#I "../packages/FSharp.Charting.0.90.14"
#load "FSharp.Charting.fsx"

open FSharp.Charting
open System
open System.Drawing

// Drawing graph of a 'square' function
Chart.Line [ for x in 1.0 .. 10.0 -> (x, x ** 2.0) ]
