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

        // Increase and decrease the match's movemente quantity counter
        public void IncreaseMovement()
        {
            MovementQuantity++;
        }

        public void DecreaseMovement()
        {
            MovementQuantity--;
        }

        // Checks if there are possible movements for a piece
        public bool IsTherePossibleMoviment()
        {
            bool[,] matrix = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrix[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        // Check if there is a piece to move and if the it belongs to the player
        public virtual bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != Color;
        }

        // Check if a piece can move to a specific position

        public virtual bool CanMoveTo(Position pos) // Renamed to "movimentoPossivel" in class.
        {
            return PossibleMovements()[pos.Line,pos.Column];
        }

        // Abastract method to be used in each piece's class
        public abstract bool[,] PossibleMovements();
    }
}
