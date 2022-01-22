using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessboard
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        private Piece[,] Pieces;

        public Board()
        {
        }
        public Board(int lines, int columns, Piece[,] pieces)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines,columns];
        }
    }
}
