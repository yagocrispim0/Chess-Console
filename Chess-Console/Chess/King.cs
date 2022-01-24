using Chessboard;
namespace Chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }
        
        // Check all the possible movements of a king
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Columns];
            Position auxpos = new Position(0,0);
            
            //North
            auxpos.SetValue(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //North East
            auxpos.SetValue(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //East
            auxpos.SetValue(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //South East
            auxpos.SetValue(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //South
            auxpos.SetValue(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //South West
            auxpos.SetValue(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
            }
            //West
            auxpos.SetValue(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(auxpos) && CanMove(auxpos))
            {
                movements[auxpos.Line, auxpos.Column] = true;
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
