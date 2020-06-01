using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using System.Threading;

namespace WS_Thread.DinerPhilo
{
    public class Philosophe
    {
        public Fork[] forks { get; private set; }
        public ConsoleColor color { get; private set; }
        private bool AsSayIsHungry; 
        private double timer;
        public bool ShowLog { get; set; }
        public EtatPhilo etatPhilo { get; private set; }
        public string name { get; set; } 
        private TimeSpan timeSpendThinkingWithoutEating;
        private TimeSpan TotaltimeSpendThinking;
        public bool isStarving { get { return (this.timeSpendThinkingWithoutEating > TimeSpan.FromMilliseconds(timer)); }  }
        DateTime previousDateTime; 
        public Philosophe(Fork[] forks, string name, ConsoleColor color)
        {
            AsSayIsHungry = false; 
            timer = new Random().Next(1000, 10000); 
            if (forks.Length != 2)
                throw new Exception("Un philosophe doit avoir 2 fourchettes associées");
            this.forks = forks;
            this.name = name;
            this.color = color; 
            previousDateTime = DateTime.Now; 
        }
        public void Update()
        {
            if (isStarving)
            {
                while (this.isStarving) Eat(); 
            }
            else
            {
                this.TotaltimeSpendThinking +=  DateTime.Now - this.previousDateTime;
                this.timeSpendThinkingWithoutEating += DateTime.Now - this.previousDateTime;
                this.previousDateTime = DateTime.Now;
            }
        }

        private void Eat()
        {
            if (!getAccessFork()) { etatPhilo = EtatPhilo.Starve; return; }
            log(this.name + " a pris ses fourchettes " + ForkToString() + " pour manger.");
            etatPhilo = EtatPhilo.Eat; 
            Thread.Sleep(TimeSpan.FromSeconds(5));
            freeAccessFork();
            log(this.name + " a bien mangé et libère ses fourchettes " + ForkToString());
            etatPhilo = EtatPhilo.Think;
            this.AsSayIsHungry = false;
            this.previousDateTime = DateTime.Now;
            this.timeSpendThinkingWithoutEating = TimeSpan.Zero;
        }

        private void freeAccessFork()
        {
            Monitor.Exit(forks[0]); 
            Monitor.Exit(forks[1]);
        }

        private bool getAccessFork()
        {
            if (!Monitor.TryEnter(forks[0]))
            {
                if (!AsSayIsHungry)
                {
                    log(this.name + " a faim et ne peux pas manger. " + ForkToString() + " sont utilisées.");
                    this.AsSayIsHungry = true;
                }
                return false;
            }
            if (!Monitor.TryEnter(forks[1], TimeSpan.FromMilliseconds(200)))
            {
                Monitor.Exit(forks[0]);
                if (!AsSayIsHungry)
                {
                    log(this.name + " a faim et ne peut pas manger. " + ForkToString() + " sont utilisées.");
                    this.AsSayIsHungry = true;
                }
                return false;
            }
            return true; 
        }

        public string ForkToString()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < this.forks.Length; i++)
            {
                sb.Append("[" + this.forks[i].Id + "]");
                if (i == 0)
                    sb.Append(" ; ");
            }

            return sb.ToString(); 
        }

        public void log(string input)
        {
            if (ShowLog)
            {
                setConsoleColor();
                Console.WriteLine(input);
            }
        }

        private void setConsoleColor()
        {
            Console.ForegroundColor = color;
        }
    }
}
