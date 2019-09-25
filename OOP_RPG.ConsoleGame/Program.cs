using Microsoft.Extensions.DependencyInjection;
using OOP_RPG.Models;
using OOP_RPG.Models.Interfaces;
using System;

namespace OOP_RPG.ConsoleGame
{
    public static class Program
    {
        public static IServiceProvider Provider { get; private set; }
        public static void Main(string[] args)
        {
            using ServiceProvider provider = BuildServiceProvider();
            Provider = provider;

            var newGame = new Game(
                console: provider.GetService<IConsole>(),
                hero: provider.GetService<IHero>(),
                shop: provider.GetService<IShop>(),
                achievementManager: provider.GetService<IAchievementManager>());
            newGame.Start();
        }

        public static ServiceProvider BuildServiceProvider() => new ServiceCollection()
            .AddSingleton<IConsole, Terminal>()
            .AddSingleton<IAchievementManager, AchievementManager>()
            .AddSingleton<IHero, Hero>()
            .AddSingleton<IShop, Shop>()
            .AddScoped<IMonster, Monster>()
            .BuildServiceProvider();
    }
}
