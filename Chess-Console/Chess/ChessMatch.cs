using System;
using Chess;
using Chessboard;
using Chessboard.Exceptions;

namespace Chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        // Initialize board
        public ChessMatch()
        {
            Board = new Board(8, 8);
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

        // Perform a full play, including movement.
        public void PerformPlay(Position origin, Position destination)
        {
            ExecuteMovement(origin, destination);
            Turn++;
            SwitchPlayer();
        }

        // Validates origin position
        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new ChessboardException("The square is empty.");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new ChessboardException("The choosen piece is not yours.");
            }
            if (!Board.Piece(pos).IsTherePossibleMoviment())
            {
                throw new ChessboardException("There are no moves available for the chosen piece.");
            }
        }

        // Validates destination position
        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new ChessboardException("Invalid destination position.");
            }

        }


        // Switches players
        private void SwitchPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        // Aux method to insert pieces
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
