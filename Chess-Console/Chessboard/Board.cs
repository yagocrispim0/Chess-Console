using System;

namespace Chessboard
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }

        private Piece[,] Pieces;

        //Constructors
        public Board()
        {
        }
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines,columns];
        }
        
        //Receiving pieces in the board
        public Piece Piece(int line, int column)
        {
            return Pieces[line,column];
        }

        public Piece piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
            
        }

        // Inserting pieces in each position of the board
        public void InsertPiece(Piece p, Position pos)
        {
            Pieces[pos.Line, pos.Column] = p;
        }
    }
}
