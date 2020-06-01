using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WS_Thread.BreakFast
{
    public class Breakfast
    {
        public async Task MakeBreakFast()
        {
            var eggTask = FryEggs();
            var toastTask = ToastBread();
            var baconTask = CookBacon();

            await Task.WhenAll(eggTask, toastTask, baconTask);

            Console.WriteLine("Le petit dej est servi !"); 
        }

        private async Task<Egg> FryEggs()
        {
            Console.WriteLine("Starting to fry eggs...");
            await Task.Delay(3000);
            Console.WriteLine("Eggs are fried  !");
            return new Egg(); 
        }

        private async Task<Toast> ToastBread()
        {
            Console.WriteLine("Starting to toast bread...");
            await Task.Delay(5000);
            Console.WriteLine("Bread is toasted !");
            return new Toast(); 
        }

        private async Task<Bacon> CookBacon()
        {
            Console.WriteLine("Starting to cook bacon...");
            await Task.Delay(2000);
            Console.WriteLine("Bacon is cooked !");
            return new Bacon(); 
        }

    }
}
