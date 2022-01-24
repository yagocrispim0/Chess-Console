using System;


namespace Chessboard
{
    internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        // Constructors
        public Piece()
        {
        }
        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            MovementQuantity = 0;
        }

        public void IncreaseMovement()
        {
            MovementQuantity++;
        }

        public void DecreaseMovement()
        {
            MovementQuantity--;
        }
    }
}
