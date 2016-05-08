open Gtk.DotNet
open FSharp.Charting
//open Samples.Charting.DojoChar
open System
open System.Drawing

// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    //0 // return an integer exit code
    // Drawing graph of a 'square' function 
    let data = [ for x in 0 .. 10 -> x, x*x ]
    let chart = Chart.Line(data, Name="Test")
    Chart.Combine [chart]

    let countryData = 
        [ "Africa", 1033043; 
          "Asia", 4166741; 
          "Europe", 732759; 
          "South America", 588649; 
          "North America", 351659; 
          "Oceania", 35838  ]
   // let chart = Chart.Bar countryData

    chart.ShowChart()
    0