using System;
using Chessboard.Exceptions;

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
        // Checking if there is a piece in a specific position

        public bool ExistsPiece(Position pos)
        {
            ValidatePosition(pos);
            return piece(pos) != null;
        }


        // This method inserts a piece into the board.
        public void InsertPiece(Piece p, Position pos)
        {
            if (ExistsPiece((pos)))
            {
                throw new ChessboardException("There a piece in this position already!");
            }
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        // This method removes a piece from the board.

        public Piece RemovePiece(Position pos)
        {
            if(piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null;
            return aux;

        }

        // Testing if position is valid

        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line > 7 || pos.Column < 0 || pos.Column > 7)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new ChessboardException("Invalid position");
            }
        }
    }
}
