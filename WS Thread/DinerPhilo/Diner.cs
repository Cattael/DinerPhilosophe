using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WS_Thread.DinerPhilo
{
    public class Diner
    {
        private int nbrEntities = 6;

        private Thread threadSec;
        private Fork[] forks;
        private Philosophe[] philosophes;
        private bool showLog;
        private bool showStatus;
        private Thread ShowStatusThread; 
        public Diner()
        {
           
            string[] names = new string[] { "Nicolas", "Alexis", "Rudy", "Cyril", "Gerald", "Clara" };
            ConsoleColor[] colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Magenta}; 
            //threadSec = new Thread(TakeFork);
            forks = new Fork[nbrEntities]; 
            philosophes = new Philosophe[nbrEntities];

            for (int i = 0; i < forks.Length; i++)
            {
                forks[i] = new Fork(i);
            }
            for (int i = 0; i < philosophes.Length; i++)
            {
                Fork[] philoFork = new Fork[2];
                for (int j = 0; j < philoFork.Length; j++)
                {
                    
                    if (i + j >= forks.Length)
                    {
                        philoFork[j] = forks[0];
                        continue;
                    }
                    philoFork[j] = forks[i + j];
                }

                philosophes[i] = new Philosophe(philoFork, names[i], colors[i]); 
            }
            showLog = false;
            showStatus = false; 
            this.ShowStatusThread = new Thread(PrintPhilosopheStatusThread);
            this.ShowStatusThread.Start();
            SetPhilosopheLogState();
            Console.WriteLine("Init done...");
            Console.WriteLine(this.ToString());
        }

        public void StartDinner()
        {
            //monitor.enter(forks);
            //threadsec.start();

            //while (Console.ReadLine() != "stop") ;
            //Monitor.Exit(forks);   
            Thread[] PhiloThread = new Thread[this.nbrEntities]; 
            for (int i = 0; i < philosophes.Length; i++)
            {
                PhiloThread[i] = new Thread(o => loopPhilosophe(o));
                PhiloThread[i].Start(philosophes[i]);
            }
            string input; 
            while((input = Console.ReadLine()) != "stop")
            {
                string[] cmd = input.Split(" "); 
                if (cmd[0].ToLower() == "showlog")
                {
                    if(cmd.Length < 2)
                    {
                        Console.WriteLine("Aucun argument donné.");
                    }
                    else if (cmd[1].ToLower() == "true")
                    {
                        this.showLog = true;
                        SetPhilosopheLogState();
                    }
                    else if (cmd[1].ToLower() == "false")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        this.showLog = false; 
                        SetPhilosopheLogState();
                    }
                }
                else if(cmd[0].ToLower() == "clear")
                {
                    Console.Clear(); 
                }
                else if (cmd[0].ToLower() == "showstatus")
                {
                    if (cmd.Length < 2)
                    {
                        Console.WriteLine("Aucun argument donné.");
                    }
                    else if (cmd[1].ToLower() == "true")
                    {
                        this.showLog = false;
                        
                        this.showStatus = true;
                        SetPhilosopheLogState();
                    }
                    else if (cmd[1].ToLower() == "false")
                    {
                        this.showStatus = false;
                        Console.ForegroundColor = ConsoleColor.White;
                        this.showLog = false;
                        SetPhilosopheLogState();
                    }
                }
                else
                {
                    Console.WriteLine("J'ai pas compris la...");
                }
            }
        }

        private void PrintPhilosopheStatusThread()
        {
            while (true)
            {
                Thread.Sleep(200);
                if (showStatus)
                    Console.Clear();
                foreach (Philosophe philosophe in philosophes)
                {
                    Console.ForegroundColor = philosophe.color;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(philosophe.name + " " + philosophe.ForkToString() +"est en train de ");
                    switch (philosophe.etatPhilo)
                    {
                        case EtatPhilo.Eat:
                            sb.Append("manger");
                            break;
                        case EtatPhilo.Starve:
                            sb.Append("mourir de faim");
                            break;
                        case EtatPhilo.Think:
                            sb.Append("penser");
                            break;
                        default:
                            Console.WriteLine("J'ai pas compris la");
                            break;
                    }

                    
                    if(showStatus)
                        Console.WriteLine(sb.ToString());
                }
            }
        }

        private void SetPhilosopheLogState()
        {
            foreach(Philosophe philosophe in philosophes)
            {
                philosophe.ShowLog = this.showLog; 
            }
        }

        private void loopPhilosophe(object philo)
        {
            //Console.WriteLine((philo as Philosophe).name);
            while (true)
            {

                (philo as Philosophe).Update();
                Thread.Sleep(500);
            }
        }

        private void TakeFork()
        {
            Console.WriteLine("Thread Lancé !");
            while(!Monitor.TryEnter(forks, TimeSpan.FromMilliseconds(2000)))
            {
                Console.WriteLine("Le thread n'a pas réussi a avoir l'objet");
            }
            Console.WriteLine("Le thread a réussi a avoir l'objet ");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Philosophe philosophe in philosophes)
            {
                sb.Append(philosophe.name + " utilise les fourchettes " + philosophe.ForkToString() + " et est en train de " + (philosophe.isStarving ? "manger" : "penser") + "\n");
            }
            return sb.ToString(); 
        }
    }
}
