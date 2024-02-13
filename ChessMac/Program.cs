﻿using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac;

using static Methods;
// TODO: take care of input
// will handle the following input options
/*
 * A2 B3, conventional notation specifying space to space
 * knight to pawn, piece to piece notation, will need to check for ambiguity and confirm if found
 * knight g4, piece to space
 * a2-b3
 * a2 pawn
 *
 * for options/views:
 *  a2 --options
 *  a2 -o
 *  a2 --piece
 *  a2 -p
 *  a2 --threat
 *  a2 -t
 *  a2 --moves
 *  a2 -m
 * 
 * for stats:
 *  --stats
 *  -s
 * 
 *  for help:
 *  a2 /?
 *  a2?
 *  /?
 *  /help
 *  /h
 * 
 */
// TODO: visualization and debugging, testing methods to output board states and data

// TODO: validate all current working methods and functions
// TODO: refactor everything as needed
// TODO: turn all Tuple<int, int> into ValueTuples<int row, int col>
// TODO: test all methods
internal static class Program
{
    private static void Main()
    {
        var board = new Board.ChessBoard();
        //board.TestingPlacePieces();
        board.PlacePieces();

        // debugging
        // board.PlacePiece(new Queen(PieceColor.White), (4, 4));

        //board.OutputBoard();

        var moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }


            board.OutputBoard();

            board.ClearValidMoves();
            board.ClearThreats();

            board.GeneratePieceMoves();
            board.AddThreats();

            var tempBoard = board.DeepCopy();
            var colorToMove = moveCounter % 2 != 0
                ? Piece.PieceColor.White
                : Piece.PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            // TODO change user input logic and validation to handle different methods of piece choice
            // and move selection. will need to also handle help prompts among others
            
            var parsedInput = GetPlayerMove();
            // TODO add validation for destinationPiece separate from pieceChoice
            var startPiecePos = ConvertPosToIndex(parsedInput.pieceToMove);
            var destPiecePos = ConvertPosToIndex(parsedInput.moveDestination);

            var activePiece = board.GetPieceByIndex(startPiecePos);
            //activePiece.PrintValidMoves();
            if (activePiece is null) continue;
            
            Piece? takenPiece;
            if (tempBoard.ValidateAndMovePiece(colorToMove, startPiecePos, destPiecePos))
            {
                board.MovePiece(startPiecePos, destPiecePos, out takenPiece);
                activePiece?.SetHasMoved();
                activePiece?.IncrementMoveCounter();
            }
            else
            {
                continue;
            }
            activePiece?.GenerateValidMoves(board, destPiecePos.row, destPiecePos.col);
            activePiece?.PrintValidMoves();
            moveCounter++;
            //board.OutputBoard();
        }
    }
}