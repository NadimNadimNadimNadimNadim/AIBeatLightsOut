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
                new Point(1,0),
                new Point(4,4),
                new Point(3,4),
                new Point(4,3),
            });
            Search search = new Search(SearchType.Astar, start);
            var stack = search.Run();
            game.Play(start,stack);

        }
    }
}
