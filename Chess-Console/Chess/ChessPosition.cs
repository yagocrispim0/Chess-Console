
// Class designed to handle the conversion between a a matrix index and the real-life chess positions. 
using Chessboard;

namespace Chess
{
    internal class ChessPosition
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }

        // This method coverts the matrix index position to a real Chess board position
        public Position toPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Line + Column;
        }
    }
}
