using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using C5;

namespace AIBeatLightsOut
{
    public enum SearchType
    {
        Astar
    }

    abstract class BaseCompare : Comparer<Node>
    {
        public override int Compare(Node x, Node y)
        {
            return f(x).CompareTo(f(y));
        }
        public int f(Node x)
        {
            return x.Depth + Heuristic(x);
        }
        public abstract int Heuristic(Node x);

    }
    class NumberOfSquares : BaseCompare
    {
        public override int Heuristic(Node x)
        {
            var sum = 0;
            foreach (var item in x.State.Board)
            {
                if (item) sum++;
            }
            return sum / 5;
        }
    }
    class NodeComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            if (x.Depth != y.Depth) return false;
            return x.State.Equals(y.State);
        }
        public int GetHashCode(Node x)
        {
            return x.Depth;
        }
    }
    public class Search
    {
        Game Game = new Game();
        private Random rand = new Random();
        IntervalHeap<Node> Open;
        System.Collections.Generic.HashSet<Node> Closed = new System.Collections.Generic.HashSet<Node>(new NodeComparer());
        List<Point> ProductionSystem
        {
            get
            {
                var list = new List<Point>();
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        list.Add(new Point(i, j));
                    }
                }
                // Shuffle(list);
                return list;
            }
        }
        private int boardSize;
        public Search(SearchType type, GameState start)
        {
            boardSize = start.BoardSize;
            switch (type)
            {
                case SearchType.Astar:
                    Open = new IntervalHeap<Node>(new NumberOfSquares());
                    break;
            }
            var node = new Node(start, 0);
            Open.Add(node);
        }
        public static void Shuffle<T>(System.Collections.Generic.IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public Stack<Point> Run()
        {
            while (!Open.IsEmpty)
            {
                var node = Open.DeleteMin();
                // node.State.print();
                if (Game.IsFinishState(node.State))
                {
                    var stack = new Stack<Point>();
                    while (node != null)
                    {
                        stack.Push(node.Move);
                        node = node.Parent;
                    }
                    return stack;
                }
                foreach (var move in ProductionSystem)
                {
                    var newNode = new Node(node, Game.MakeMove(node.State, move), move);
                    if (!Closed.Contains(newNode)) Open.Add(newNode);
                }
                Closed.Add(node);
            }
            System.Console.WriteLine("No solution found.");
            return null;
        }


    }

    public class Node
    {
        public GameState State { get; set; }
        public Node Parent { get; }
        public Node Child { get; set; }
        public int Depth { get; set; }
        public Point Move { get; set; }

        public Node(GameState state, int depth)
        {
            State = state;
            Depth = depth;
        }
        public Node(Node parent, GameState state, Point move)
        {
            State = state;
            Parent = parent;
            Depth = parent.Depth + 1;
            Move = move;
        }

    }
}