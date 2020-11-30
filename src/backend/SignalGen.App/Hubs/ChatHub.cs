using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalGen.App.Hubs
{
    public class ChatHub: Hub
    {
        public async Task Method(string arg1, int arg2)
        {
            await Task.Delay(500);
            Console.WriteLine($"{arg1}, {arg2}");
        }

        public async Task AnotherMethod(string name, int amount, string name2, Guid id, float num)
        {
            await Task.Delay(500);
        }

        public async Task ThirdMethod(TestClass test, List<string> strings)
        {
            await Task.Delay(500);
        }
    }

    public class TestClass
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public InnerClass1 InnerClass1 { get; set; }
        public InnerClass2 InnerClass2 { get; set; }
    }

    public class InnerClass1
    {
        public string Title { get; set; }
        public string LastName { get; set; }
        public List<string> StringList { get; set; }
    }

    public class InnerClass2
    {
        public string Address { get; set; }
        public int Amount { get; set; }
    }
}
