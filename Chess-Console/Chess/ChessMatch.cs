using System;
using System.Collections.Generic;
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
        private HashSet<Piece> PiecesCollection;
        private HashSet<Piece> CapturedCollection;

        // Initialize board
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PiecesCollection = new HashSet<Piece>();
            CapturedCollection = new HashSet<Piece>();
            PutPieces();

        }

        // Executes a movement in the game;
        public void ExecuteMovement(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMovement();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.InsertPiece(p, destination);
            if (capturedPiece != null)
            {
                CapturedCollection.Add(capturedPiece);
            }
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

        // Returns all the captured pieces filtered by color
        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in CapturedCollection)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        // Returns all the pieces in the game
        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in CapturedCollection)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        // Method that inserts a new piece into the game
        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            PiecesCollection.Add(piece);
        }

        // Aux method to insert pieces
        private void PutPieces()
        {
            InsertNewPiece('c', 1 , new Tower(Board, Color.White));
            InsertNewPiece('c', 2, new Tower(Board, Color.White));
            InsertNewPiece('d', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 1, new Tower(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));

            InsertNewPiece('c', 7, new Tower(Board, Color.Black));
            InsertNewPiece('c', 8, new Tower(Board, Color.Black));
            InsertNewPiece('d', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 8, new Tower(Board, Color.Black));
            InsertNewPiece('d', 8, new King(Board, Color.Black));

        }
    }
}
