using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using WS_Thread.DinerPhilo;

namespace WS_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            Diner diner = new Diner();
            diner.StartDinner(); 
        }
    }
}
