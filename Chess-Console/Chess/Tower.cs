﻿using Chessboard;
namespace Chess_Console.Chess
{
    internal class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "T";
        }



    }
}