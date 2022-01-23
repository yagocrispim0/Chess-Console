using System;
using Chessboard;


namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;

        // Initialize board
        public ChessMatch()
        {
            Board = new Board();
            Turn = 1;
            CurrentPlayer = Color.White;
            PutPiece();

        }

        // Executes a movement in the game;
        public void ExecuteMovement(Position origin, Position end)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece CapturedPiece = Board.RemovePiece(end); // To be implemented later. Check convention when done so.
        }

        private void PutPiece()
        {

        }
    }
}
