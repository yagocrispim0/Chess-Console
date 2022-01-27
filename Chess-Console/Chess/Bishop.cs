using System;
using Chessboard;

namespace Chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }

        // Check all the possible movements of a Bishop
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position(0, 0);
             
            // North East
            auxpos.SetValue(Position.Line -1, Position.Column +1);
            while(Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line - 1;
                auxpos.Column = auxpos.Column + 1;
            }

            // South East
            auxpos.SetValue(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line + 1;
                auxpos.Column = auxpos.Column + 1;
            }

            // South West
            auxpos.SetValue(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line + 1;
                auxpos.Column = auxpos.Column - 1;
            }

            // North West
            auxpos.SetValue(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line - 1;
                auxpos.Column = auxpos.Column - 1;
            }

            return movements;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
