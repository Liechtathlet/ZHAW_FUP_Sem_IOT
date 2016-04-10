# FUP_Sem_IOT
Functional Programming - Term Paper - IOT / Big Data

## Setup Raspbian on Micro SD Card

  0. Installation instructions: https://www.raspberrypi.org/documentation/installation/noobs.md
  1. Download NOOBS: https://www.raspberrypi.org/downloads
  2. Run
      ~~~
      sudo fdisk -l
      ~~~
  2. Insert Micro SD Card
  3. Find Device (Compare before and after insert)
     ~~~
     sudo fdisk -l
     ~~~
  4. Start fdisk
     ~~~
     sudo fdsik <PathToDevice>
     ~~~
  5. Delete existing partitions
     ~~~
     d
     ~~~
  6. Create new partition (use default values: p, 1, ...)
      ~~~
      n
      ~~~
  7. Verify partition
      ~~~
      p
      ~~~
  8.  Change partition type (Type: W95 FAT - b)
      ~~~
      t
      ~~~
  9.  Make partition 1 bootable
      ~~~
      a
      ~~~
  10. Write changes
      ~~~
      w
      ~~~
  11. Unmount device
  12. Format partition (Use path to device partition!)
      ~~~
      sudo mkfs.vfat <PathToDevicePartition>
      ~~~
  13. Remount device
  14. Unzip Noobs
  15. Copy contents of zip file to device
  16. Insert micro sd card into rapsberry pi and start

## Raspbian Setup
### WLAN
** Disable IPv6**
~~~
sudo leafpad /etc/sysctl.conf
net.ipv6.conf.all.disable_ipv6 = 1
~~~
### Groove PI
  - http://www.dexterindustries.com/GrovePi/get-started-with-the-grovepi/setting-software/
### Mono / FSharp
**Mono-Version >= 4.0 is required**
~~~
sudo apt-get update
sudo apt-get install mono-complete
sudo apt-get install mono-devel
sudo apt-get install mono-csharp-shell
sudo apt-get install fsharpi
~~~

### .NET Core
~~~
sudo apt-get install unzip curl
curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh

sudo apt-get install libunwind8 gettext libssl-dev libcurl4-openssl-dev zlib1g libicu-dev uuid-dev

mozroots --import --sync

dnvm upgrade -r coreclr
dnvm upgrade -r mono
dnvm install latest -r coreclr -u -p
dnvm install -r mono -arch x86 latest -p
dnvm upgrade -u

~~~

**Links:**
  - http://docs.asp.net/en/latest/getting-started/installing-on-linux.html
  - http://docs.asp.net/en/latest/dnx/projects.html
  - https://blogs.msdn.microsoft.com/bethmassi/2015/02/25/understanding-net-2015/
  - https://github.com/robsonj/GrovePi
  - http://oren.codes/2015/07/29/targeting-net-core/
  - http://www.paraesthesia.com/archive/2015/10/20/gotcha-with-dnx-dependency-resolution-dotnet-pcl/
  - http://davidfowl.com/diagnosing-dependency-issues-with-asp-net-5/
  - https://github.com/mrward/monodevelop-dnx-addin
  - http://dotnet.github.io/docs/getting-started/installing/installing-core-linux.html
  - https://www.dotnet.xyz/tutorials/net-core-unter-linux-raspberry-pi/
  - https://www.hackster.io/9381/grove-pi-windows-iot-getting-started-94bf38

**Hints:**
  - .NET Core aktuell nur für 64bit
  - Raspberry PI OS sind praktisch alle 32bit
  - GrovePi NuGet Library verwendet .NET Core native (Windows only)
  - https://github.com/raspberry-sharp/raspberry-sharp-io
## Vergleich Raspbian vs Windows 10
http://netmf-tutorial.de/reaktionszeit-raspberry-pi-windows-10-gegen-net-micro-framework-gegen-arduino/
