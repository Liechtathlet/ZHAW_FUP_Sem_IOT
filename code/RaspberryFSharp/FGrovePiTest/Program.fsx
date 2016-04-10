// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#r "./libs/GrovePiTest.dll"

open GrovePiTest;

let blink = 
    printfn "Hello from F#"
    GrovePiTest.Program.blink()
    0// return an integer exit code
