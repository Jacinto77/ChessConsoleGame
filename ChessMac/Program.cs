using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac;

using static Methods;
// TODO: verify all piece generation moves before finishing castling and pawn promotion
// TODO: then finish pawn promotion and verify
// TODO: then finish castling and verify
// TODO: finally, finish unit testing and begin improving and expanding
// TODO: "simple" chess engine

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

// The data structures that need to be updated when a piece is moved are:
// ChessBoard.ActivePieces, ChessBoard.InactivePieces, ChessBoard.PiecePositions, Piece.Position
/*
 * Start of program
 *  Prompt for choice at main menu
 * For game start
 * -- begin
 *  create chessboard
 *  initialize pieces (place pieces in ActivePieces list)
 * -- each game loop iteration
 *  update piece positions dictionary
 *  populate board based on piece positions
 *  generate valid moves for each piece
 *  calculate threats and pins
 *  output board to console
 * User Input
 *  prompt for user input
 *      "A2 A3" or "pawn H3" or "PxH3"
 *  process input, determine format and parse for usable values
 *  pass to move piece functions
 * Move Validating
 *  move is temporarily given to a copy of the board to test if move is valid
 *  checks that:
 *      piece is correct color
 *      destination is a valid move in the piece's list of valid moves
 *      king is not in check after the move
 *      destination does not have a friendly piece
 *  if all checks pass, perform the same move on the "real" chessboard
 *  otherwise, print reason why move wasn't valid
 *  retry loop iteration
 *
 * color to move is controlled by the move counter, which is incremented at the end of
 * the loop, continuing prior to that statement skips the color change from white to black, or
 * vice versa
 * 
 */
//

internal static class Program
{
    private static void Main()
    {
        var board = new ChessBoard();
        board.InitializeActivePieces();

        var moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }
            board.UpdatePiecePositions();
            board.PopulateBoard();
            board.OutputBoard();
            
            board.ClearValidMoves();
            board.GeneratePieceMoves();

            // foreach (var piece in board.ActivePieces)
            // {
            //     piece.PrintValidMoveList();
            // }
            
            var tempBoard = board.DeepCopy();
            var colorToMove = moveCounter % 2 != 0 ? Piece.PieceColor.White : Piece.PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            // TODO change user input logic and validation to handle different methods of piece choice and move selection. will need to also handle help prompts among others
            
            var parsedInput = GetPlayerMove();
            // TODO add validation for destinationPiece separate from pieceChoice when going to promote a piece
            var startPiecePos = ConvertPosToIndex(parsedInput.pieceToMove);
            var destPiecePos = ConvertPosToIndex(parsedInput.moveDestination);

            var activePiece = board.GetPieceByPosition(startPiecePos);
            
            if (activePiece is null) continue;
            
            Piece? takenPiece;
            if (tempBoard.ValidateAndMovePiece(colorToMove, startPiecePos, destPiecePos))
            {
                board.MovePiece(startPiecePos, destPiecePos, out takenPiece);
                // activePiece?.SetHasMoved();
                // activePiece?.IncrementMoveCounter();
            }
            else
            {
                // TODO error checking here
                continue;
            }
            moveCounter++;
        }
    }
}