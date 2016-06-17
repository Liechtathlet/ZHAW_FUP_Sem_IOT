//open Gtk.DotNet
//open FSharp.Charting
//open Samples.Charting.DojoChar
open XPlot.GoogleCharts
open System
open System.Drawing
open System.Data

// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let Bolivia = seq [ 100; 100; 100; 100; 100 ]
    let Bolivia2 = seq [ seq [(100,100)] ]
    let Ecuador = seq [ 920; 1150; 1190; 1150; 700]
    let Madagascar = seq [ 500; 600; 550; 610; 630]
    let Average = seq [ 610; 630; 610; 600; 580 ]
    let series = [ "bars"; "bars"; "bars"; "lines" ]
    let series2 = [ "bars" ]
    let inputs = [ Bolivia; Ecuador; Madagascar; Average ]

    let x = Bolivia2
            |> Chart.Combo
            |> Chart.WithOptions 
                 (Options(title = "Coffee Production", series = 
                    [| for typ in series2 -> Series(typ) |]))
            |> Chart.WithLabels 
                 ["Bolivia"]
            |> Chart.WithLegend true
            |> Chart.WithSize (600, 250)
    printfn "%s" x.Html

    0 // return an integer exit code
