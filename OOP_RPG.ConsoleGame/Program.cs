﻿using Microsoft.Extensions.DependencyInjection;

namespace OOP_RPG.ConsoleGame
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (ServiceProvider provider = BuildServiceProvider())
            {
            }
        }

        public static ServiceProvider BuildServiceProvider() =>
            new ServiceCollection()
            .BuildServiceProvider();
    }
}
