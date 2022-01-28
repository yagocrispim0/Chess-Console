using Chessboard;
namespace Chess
{
    internal class King : Piece
    {
        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }
        
        private bool TestingRookCastling(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null & p is Rook && p.Color == Color && p.MovementQuantity == 0;
        }

        // Check all the possible movements of a king
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position(0,0);
             
            // North
            auxpos.SetValue(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            // North East
            auxpos.SetValue(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //  East
            auxpos.SetValue(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            // South East
            auxpos.SetValue(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //  South
            auxpos.SetValue(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            // South West
            auxpos.SetValue(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            // West
            auxpos.SetValue(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            // Castling (Special move)
            if (MovementQuantity==0 && !Match.Check)
            {
                // Castling short
                Position posRookS = new Position(Position.Line, Position.Column + 3);
                if (TestingRookCastling(posRookS))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        movements[Position.Line, Position.Column + 2] = true;
                    }
                }

                // Castling long
                Position posRookL = new Position(Position.Line, Position.Column - 4);
                if (TestingRookCastling(posRookL))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                    {
                        movements[Position.Line, Position.Column - 2] = true;
                    }

                }

            }

            return movements;

        }

        // Character printed on the console
        public override string ToString()
        {
            return "K";
        }



    }
}
