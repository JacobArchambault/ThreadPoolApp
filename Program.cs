using System.Threading;
using static System.Console;
using static System.Threading.Thread;
using static System.Threading.ThreadPool;
namespace ThreadPoolApp
{
    public class Printer
    {
        private readonly object lockToken = new object();

        public void PrintNumbers()
        {
            lock (lockToken)
            {
                // Display Thread info.
                WriteLine($"-> {CurrentThread.ManagedThreadId} is executing PrintNumbers()");

                // Print out numbers.
                Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    Write($"{i}, ");
                    Sleep(1000);
                }
                WriteLine();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            WriteLine("***** Fun with the CLR Thread Pool *****\n");
            WriteLine($"Main thread started. ThreadID = {CurrentThread.ManagedThreadId}");

            Printer p = new Printer();

            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            // Queue the method ten times.
            for (int i = 0; i < 10; i++)
            {
                QueueUserWorkItem(workItem, p);
            }
            WriteLine("All tasks queued");
            ReadLine();
        }
        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }
    }
}