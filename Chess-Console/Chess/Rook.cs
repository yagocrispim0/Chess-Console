using Chessboard;
namespace Chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        // Check all possible movements of a Rook.
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position();
            
            //North
            auxpos.SetValue(Position.Line -1, Position.Column);
            while(Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line - 1;
            }

            //East
            auxpos.SetValue(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Column = auxpos.Column + 1;
            }

            //South
            auxpos.SetValue(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Line = auxpos.Line + 1;
            }


            //West
            auxpos.SetValue(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
                if (Board.Piece(auxpos) != null && Board.Piece(auxpos).Color != Color)
                {
                    break;
                }
                auxpos.Column = auxpos.Column - 1;
            }

            return movements;
        }


        public override string ToString()
        {
            return "R";
        }

    }
}
