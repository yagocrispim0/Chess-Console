using System;


namespace Chessboard
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece()
        {
        }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            Board = board;
            MovementQuantity = 0;
        }
    }
}
