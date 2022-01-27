using System;
using Chessboard;

namespace Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        private bool IsThereOpponentPiece(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool EmptySpace(Position pos)
        {
            return Board.Piece(pos) == null;
        }


        // Check all the possible movements of a Pawn
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position(0, 0);

            //White Pieces
            if (Color == Color.White)
            {
                auxpos.SetValue(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(auxpos) && EmptySpace(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }
                if (MovementQuantity == 0)
                {
                    auxpos.SetValue(Position.Line - 2, Position.Column);
                    if (Board.ValidPosition(auxpos) && EmptySpace(auxpos))
                    {
                        movements[auxpos.Line, auxpos.Column] = true;
                    }
                }

                // Attacking North East
                auxpos.SetValue(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(auxpos) && IsThereOpponentPiece(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }

                // Attacking North West
                auxpos.SetValue(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(auxpos) && IsThereOpponentPiece(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }
                return movements;
            }
            else
            {
                auxpos.SetValue(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(auxpos) && EmptySpace(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }
                if (MovementQuantity == 0)
                {
                    auxpos.SetValue(Position.Line + 2, Position.Column);
                    if (Board.ValidPosition(auxpos) && EmptySpace(auxpos))
                    {
                        movements[auxpos.Line, auxpos.Column] = true;
                    }
                }

                // Attacking South East
                auxpos.SetValue(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(auxpos) && IsThereOpponentPiece(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }

                // Attacking South West
                auxpos.SetValue(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(auxpos) && IsThereOpponentPiece(auxpos))
                {
                    movements[auxpos.Line, auxpos.Column] = true;
                }
                return movements;
            }
        }


        /*auxpos.SetValue(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line + 1;
                auxpos.Column = auxpos.Column + 1;
            }*/

        public override string ToString()
        {
            return "P";
        }
    }
}
