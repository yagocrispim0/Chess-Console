using System;


namespace Chessboard
{
    internal abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        // Constructors
        public Piece()
        {
        }
        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            MovementQuantity = 0;
        }

        public abstract bool[,] PossibleMovements();

        // Increase and decrease the match's movemente quantity counter
        public void IncreaseMovement()
        {
            MovementQuantity++;
        }

        public void DecreaseMovement()
        {
            MovementQuantity--;
        }

        public virtual bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }
    }
}
