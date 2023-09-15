﻿
namespace ChessMac;
using static Methods;

internal static class Program
{
    static void Main()
    {
        // create chessboard
        ChessBoard board = new ChessBoard();

        board.OutputBoard();

        const int numberOfPieces = 16;
        
        // generate pieces and initialize them
        Piece[] whitePieces = new Piece[numberOfPieces];
        //Piece pawnA = whitePieces[0];
        //Piece pawnB = whitePieces[1];
        //...
        Piece[] blackPieces = new Piece[numberOfPieces];
        InitPieces(whitePieces, "white");
        InitPieces(blackPieces, "black");
        // foreach (Piece piece in whitePieces)
        // {
        //     Console.WriteLine(piece.Name);
        // }
        // foreach (Piece piece in blackPieces)
        // {
        //     Console.WriteLine(piece.Name);
        // }
        
        //InitializePieces(inPieces: whitePieces, color: "white", inIcons: WhiteIcons);
        //InitializePieces(inPieces: blackPieces, color: "black", inIcons: BlackIcons);
        
        //PlacePieces(whitePieces, blackPieces, board);
        PlacePieces(whitePieces, board);
        PlacePieces(blackPieces, board);
        GeneratePieceMoves(whitePieces, board);
        GeneratePieceMoves(blackPieces, board);
        board.DisplayMoves(whitePieces[9]);
        board.DisplayMoves(whitePieces[1]);
        
        board.OutputBoard();
        whitePieces[1].PrintValidMoves();
        whitePieces[1].PrintValidMovesIndex();
        
        
        // Piece? testPiece = Piece.CreatePiece("black", "bishop");
        // Piece? testPiece2 = Piece.CreatePiece("black", "rook");
        // testPiece?.PlacePiece(board.GetSpace("D5"));
        // testPiece2?.PlacePiece(board.GetSpace("F2"));
        // board.OutputBoard();
        //
        // testPiece?.GenerateValidMoves(board);
        // testPiece?.AddThreats();
        // testPiece?.PrintValidMoves();
        //
        // testPiece2?.GenerateValidMoves(board);
        // testPiece2?.AddThreats();
        // testPiece2?.PrintValidMoves();
        //
        // board.GetSpace("F3").PrintThreats();
        // board.DisplayMoves(testPiece2);
        // board.DisplayMoves(testPiece);
        // board.OutputBoard();
        
        
        /*
        while (true)
        {
            // prompt to start
            
            // generate board and pieces
            
            // create array of valid moves for each piece
                // methods for each piece with the rules for valid moves

                while (true)
            {
                // output board
                
                // prompt for <COLOR> move
                    // get move from console in format <char, int>
                // validate move
                    // check if input is valid (char: A-H int: 1-8)
                    // convert input to indices of board array
                    // check position of input
                        // if it has a piece, check if proper color
                        // if it is a valid piece and the proper color, check for destination
                    // validate that destination is within bounds of board, and
                            // complies with piece's allowed moves
                    // check that the move doesn't place self in check
                    // check that the move doesn't move through other pieces
                // if move is to empty space
                    // update previous space to be empty
                    // update new space with piece
                
                // if move takes a piece
                    // update previous space to be empty
                    // update new space with piece and color
                    // remove taken piece from list of active pieces
                        // to be used for calculating score
                        
                // if move places other king in check, set isInCheck = true
                    // restricts black to only moves that makes isInCheck = false
                        
                // check for check and checkmate
                    // King piece has property isInCheck
                    // has method to check for checkmate
                        // checkmate is any board position where the king is in the line of fire
                        // and no valid moves will take the king out of check
                            // block check: check path of attacking piece, if a piece has a valid
                                // move that == one of the spaces in the attacking pieces path
                                // checkmate = false
                            // take piece: check valid moves from pieces and if a piece has a valid
                                // move to take the attacking piece, checkmate = false
                            // move king: check valid moves from King, if king can move to position
                                // that is not in check, checkmate = false
                        // if blockCheck, takePiece, and moveKing all == false, then checkMate=true
                    
                // regenerate arrays of valid moves
                    // for any piece, first check current array vs current state of board
                        // if a piece's valid moves now has a piece occupying any of the previous
                            // spaces, regenerate moves
                    // if a piece's valid moves/path/spaces have not been changed, do not regenerate
                    
            }
            
        }
        */
    }
}



    