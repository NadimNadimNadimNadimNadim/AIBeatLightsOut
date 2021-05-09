using System;
using System.Collections.Generic;
using System.Drawing;

namespace AIBeatLightsOut
{
    public class Game
    {
        public GameState MakeMove(GameState state, Point move)
        {
            var newState = new GameState(state);
            newState.Board[move.Y, move.X] = !newState.Board[move.Y, move.X];
            // -1 x
            if (move.Y - 1 >= 0) newState.Board[move.Y - 1, move.X] = !newState.Board[move.Y - 1, move.X];
            // +1 x
            if (move.Y + 1 < state.BoardSize) newState.Board[move.Y + 1, move.X] = !newState.Board[move.Y + 1, move.X];
            // -1 y
            if (move.X - 1 >= 0) newState.Board[move.Y, move.X - 1] = !newState.Board[move.Y, move.X - 1];
            // +1 y
            if (move.X + 1 < state.BoardSize) newState.Board[move.Y, move.X + 1] = !newState.Board[move.Y, move.X + 1];
            return newState;
        }
        public bool IsFinishState(GameState state) 
        {
            foreach (var item in state.Board)
            {
                if (item == true) return false;
            }
            return true;
        }
        public void Play(GameState start, Stack<Point> moves)
        {
            start.print();
            while(moves.Count > 0)
            {
                var move = moves.Pop();
                System.Console.WriteLine(move);
                start = MakeMove(start,move);
                start.print();
            }
        }

    }
    public class GameState
    {
        public int BoardSize { get; }
        public bool[,] Board { get; }
        public GameState(int boardSize, Point[] startingPositions)
        {
            BoardSize = boardSize;
            Board = new bool[boardSize, boardSize];
            foreach (var position in startingPositions)
            {
                Board[position.Y, position.X] = true;
            }
        }
        public GameState(GameState state)
        {
            BoardSize = state.BoardSize;
            Board = new bool[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Board[i, j] = state.Board[i, j];
                }
            }
        }
        public void print()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                Console.WriteLine(new String('-', BoardSize * 4));
                for (int j = 0; j < BoardSize; j++)
                {
                    Console.Write($"| {(Board[i, j] ? "X" : " ")} ");
                }
                Console.Write("|\n");
            }
            Console.WriteLine(new String('-', BoardSize * 4));
            System.Console.WriteLine();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is GameState)) return false;
            var other = obj as GameState;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (!Board[i,j]==other.Board[i,j]) return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        
    }
}
