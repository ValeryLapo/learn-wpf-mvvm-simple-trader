using System;
using System.Linq;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());
            Console.WriteLine(userService.Delete(1).Result);
        }
    }
}
