#load "../packages/FSharp.Charting.Gtk.0.90.14/FSharp.Charting.Gtk.fsx"

#r "/usr/lib/cli/gtk-sharp-2.0/gtk-sharp.dll"
#r "./FSharp.Charting.Gtk.dll"

open FSharp.Charting

Chart.Line([ for x in 0 .. 10 -> x, x*x ]).ShowChart()


//1. xhost +localhost
//2.  fsharpi --lib:./bin/Debug,/usr/lib/cli/gtk-sharp-2.0/,/usr/lib/cli/gdk-sharp-2.0/,/usr/lib/cli/atk-sharp-2.0/,/usr/lib/cli/glib-sharp-2.0/ 
//3. #load "Program.fsx";;
