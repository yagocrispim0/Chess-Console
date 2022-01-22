using Chessboard;
namespace Chess_Console.Chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "K";
        }



    }
}
