using System;
using Chessboard;

namespace Chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        // Check all the possible movements of a Knight
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position(0, 0);

            // North
            auxpos.SetValue(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            auxpos.SetValue(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            // East
            auxpos.SetValue(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            auxpos.SetValue(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            // South
            auxpos.SetValue(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            auxpos.SetValue(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            // West

            auxpos.SetValue(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            auxpos.SetValue(Position.Line +1, Position.Column - 2);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }

            return movements;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}
