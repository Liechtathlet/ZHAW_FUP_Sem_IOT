#!/usr/bin/env bash

trap_with_arg() { # from http://stackoverflow.com/a/2183063/804678
  local func="$1"; shift
  for sig in "$@"; do
    trap "$func $sig" "$sig"
  done
}

stop() {
  trap - SIGINT EXIT
  printf '\n%s\n' "recieved $1, killing children"
  kill -s SIGINT 0
}

trap_with_arg 'stop' EXIT SIGINT SIGTERM SIGHUP

(python "/home/pi/GrovePi/Software/Python/grove_led_blink.py") & sleep 5 ; kill $!

cd "/home/pi/Development/FUP/Raspberry.FGrove/data"

python -m SimpleHTTPServer 8000 &
cd "/home/pi/Development/FUP/Raspberry.FGrove/"
fsharpi --lib:/home/pi/Development/FUP/Raspberry.FGrove/libs "/home/pi/Development/FUP/Raspberry.FGrove/Program.fsx"
