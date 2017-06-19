using FoxAndChickens.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoxAndChickens
{
    class Program
    {
        static void Main(string[] args)
        {
            var fox = new Item { Name = "Fox" };
            var chicken = new Item { Name = "Chick" };
            var grain = new Item { Name = "Grain" };

            fox.Food = chicken;
            chicken.Food = grain;

            var game = new Game();
            game.LeftItems = new List<Item>
            {
                fox,
                chicken,
                grain
            };


            ShowStart(game);

            game.Play();

            ShowResult(game);

            Console.ReadLine();
        }

        private static void ShowResult(Game game)
        {
            foreach (var a in game.Actions)
            {
                var left = string.Join(",", a.LeftItems.Select(x => x.Name));
                var crossed = string.Join(",", a.CrossedItem.Select(x => x.Name));
                if (a.ActionName == RiverActionName.Cross) crossed += " and Farmer";
                else
                {
                    left += " and farmer";
                }

                Console.WriteLine("{0} -- {1} ", a.Item?.Name ?? "Nothing", a.ActionName);
                Console.WriteLine("left :{0} ", left);
                Console.WriteLine("crossed :{0} ", crossed);

                Console.WriteLine("---------------------------------------------");
            }
        }


        private static void ShowStart(Game game)
        {
            var leftFirst = string.Join(",", game.LeftItems.Select(x => x.Name));
            leftFirst += " and Farmer";
            var crossedFirst = string.Join(",", game.CrossedItems.Select(x => x.Name));


            Console.WriteLine("left :{0} ", leftFirst);
            Console.WriteLine("crossed :{0} ", crossedFirst);
            Console.WriteLine("---------------------------------------------");
        }


    }
}
