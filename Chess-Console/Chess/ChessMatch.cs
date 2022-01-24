using System;
using Chess;
using Chessboard;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        // Initialize board
        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PutPieces();

        }

        // Executes a movement in the game;
        public void ExecuteMovement(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.InsertPiece(p, destination);
        }

        private void PutPieces()
        {
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());

        }
    }
}
