using System;
using Chessboard;

namespace Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
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

                // En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereOpponentPiece(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        movements[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereOpponentPiece(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        movements[right.Line - 1, right.Column] = true;
                    }


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

                // En passant

                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereOpponentPiece(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        movements[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereOpponentPiece(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        movements[right.Line + 1, right.Column] = true;
                    }


                }


                return movements;
            }
        }


        public override string ToString()
        {
            return "P";
        }
    }
}
