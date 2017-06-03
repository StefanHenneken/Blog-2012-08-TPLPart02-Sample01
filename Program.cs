using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            Console.WriteLine("Run Start");
            Task task1 = new Task((ct1) =>
                {
                    CancellationToken ctLocal = (CancellationToken)ct1;
                    Console.WriteLine("Task Start");
                    try
                    {
                        while (true)
                        {
                            Console.Write(".");
                            ctLocal.ThrowIfCancellationRequested();
                            Thread.Sleep(1000);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("OperationCanceledException");
                    }
                    Console.WriteLine("Task End");
                }, ct);
            task1.Start();
            Console.ReadLine();
            cts.Cancel();
            task1.Wait();
            Console.WriteLine("Run End");
            Console.ReadLine();
        }
    }
}
