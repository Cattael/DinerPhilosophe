using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using WS_Thread.BreakFast;
using WS_Thread.DinerPhilo;

namespace WS_Thread
{
    class Program
    {
        delegate int AddNumber(int v1, int v2);
        delegate void Q4A();
        static void Main(string[] args)
        {/*
            Action<string> ThreadSayHiWithLambdaExpression = msg => Console.WriteLine(msg);

            Action<object> Q5Pool = obj =>
            {
                
            };

            CLpara cLpara = new CLpara();
            Q4A Q4AThread = new Q4A(cLpara.methode_para);
            Thread thread = new Thread(Q4AThread.Invoke);
            thread.Start();

            Func<int, int> square = nbr => nbr * nbr; 
            Thread threadLambda = new Thread(() => { 
                for(int i = 0; i < 10; i ++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("i : " + i);
                }
            });
            threadLambda.Start();

            Thread threadLambdaArgument = new Thread(() => ThreadSayHiWithLambdaExpression("Hello la metea"));
            threadLambdaArgument.Start(); 

            AddNumber function = new AddNumber(add);
            int i = function.Invoke(2, 30);

            var anonyme = new { Text = "Anonyme Text ", Nbr = 2 }; 

            Console.WriteLine("Delegate : " + i); 
            Console.WriteLine("Lambda exp : " + square(3));
            Console.WriteLine("Anonyme Type : " + anonyme.Text + anonyme.Nbr);
            Console.ReadLine(); 
            */
            //Breakfast breakfast = new Breakfast();
            //RunBreakFast(breakfast);

            Diner diner = new Diner();
            diner.StartDinner(); 

        }

        public static async Task RunBreakFast(Breakfast b)
        {
            await b.MakeBreakFast();
        }
        static void sayhi()
        {
            Console.WriteLine("ello");
        }
        static int add (int v1, int v2)
        {
            return (v1 + v2); 
        }
    }
}
