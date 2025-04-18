using System.Diagnostics;
using System.Threading.Tasks;

namespace TestAsyncAwait
{
    internal class Program
    {
        static async Task<long> OP1()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.Delay(2000);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
        static async Task<long> OP2()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.Delay(500);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
        static async Task<long> OP3()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await Task.Delay(3000);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
        static async Task Main(string[] args)
        {
            await RealAsync();
            await AnotherOneRealAsync();
            await FakeAsync();
            await ManyParallelTasksAsync();
        }

        static async Task RealAsync()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var t1 = OP1();
            var t2 = OP2();
            var t3 = OP3();

            var result = await Task.WhenAll(t1, t2, t3);
            stopWatch.Stop();

            Console.WriteLine("t1: " + result[0]);
            Console.WriteLine("t2: " + result[1]);
            Console.WriteLine("t3: " + result[2]);
            Console.WriteLine("RealAsync: " + stopWatch.ElapsedMilliseconds);
        }
        static async Task AnotherOneRealAsync()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var t1 = OP1();
            var t2 = OP2();
            var t3 = OP3();
            await t1;
            await t2;
            await t3;
            stopWatch.Stop();
            Console.WriteLine("AnotherOneRealAsync: " + stopWatch.ElapsedMilliseconds);
        }
        static async Task FakeAsync()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var t1 = await OP1();
            var t2 = await OP2();
            var t3 = await OP3();
            stopWatch.Stop();

            Console.WriteLine("t1: " + t1);
            Console.WriteLine("t2: " + t2);
            Console.WriteLine("t3: " + t3);
            Console.WriteLine("FakeAsync: " + stopWatch.ElapsedMilliseconds);
        }
        static async Task ManyParallelTasksAsync()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var t1 = new SuperTask() { id = 1, task = OP1() };
            var t2 = new SuperTask() { id = 2, task = OP2() };
            var t3 = new SuperTask() { id = 3, task = OP3() };

            List<SuperTask> list = [t1, t2, t3];
            var list2 = new List<Task<long>>(list.Select(i => i.task!));
            while (list2.Count > 0)
            {
                var result = await Task.WhenAny(list2);
                Console.WriteLine($"t{list.FirstOrDefault(i => i.task == result)!.id} {await result}");
                list2.Remove(result);
            }
            stopWatch.Stop();
            Console.WriteLine("ManyParallelTasksAsync: " + stopWatch.ElapsedMilliseconds);
        }
        class SuperTask
        {
            public int id;
            public Task<long>? task;
        }
    }
}
