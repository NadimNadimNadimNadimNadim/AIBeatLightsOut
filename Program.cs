using System;
using System.Collections.Generic;
using System.Drawing;

namespace AIBeatLightsOut
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            GameState start = new GameState(5,
            new Point[]{
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),
                new Point(3,2),
                new Point(4,2),
                new Point(2,4),
                new Point(4,4),
            });
            // var startingPoints = new List<Point>();
          
            // int x;
            // int y;
            // string input = Console.ReadLine();
            // do
            // {
                
            //     x = int.Parse(input);
            //     input = Console.ReadLine();
            //     y = int.Parse(input);
            //     startingPoints.Add(new Point(x-1,y-1));                      
            // } while ((input = Console.ReadLine()) != "X" );
            // GameState start = new GameState(5,startingPoints.ToArray());
            Search search = new Search(SearchType.Astar, start);
            var stack = search.Run();
            game.Play(start,stack);

        }
    }
}
