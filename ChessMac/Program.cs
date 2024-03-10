using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac;

using static Methods;

// TODO: validate move generation methods
// TODO: pawn promotion
// TODO: chess engine
// TODO: visualization and testing

internal static class Program
{
    private static void Main()
    {
        var moveCounter = 1;
        List<int> errorCodes = new List<int>();
        
        var board = new ChessBoard();
        board.InitializeActivePieces();
        board.UpdateBoardAndPieces();
        
        UpdateScreen(board, moveCounter, GetColorToMove(moveCounter), errorCodes);
        
        while (true)
        {
            ChessBoard tempBoard = board.DeepCopy();
            Piece.PieceColor colorToMove = GetColorToMove(moveCounter);

            string[] playerMove = GetPlayerMove();
            
            (int row, int col) startPiecePos = ConvertPosToIndex(playerMove[0]);
            (int row, int col) destPiecePos = ConvertPosToIndex(playerMove[1]);

            Piece? activePiece = board.GetPieceByPosition(startPiecePos);
            if (activePiece is not null)
            {
                Piece? takenPiece;
                if (tempBoard.ValidateAndMovePiece(colorToMove, startPiecePos, destPiecePos, out errorCodes))
                {
                    board.MovePiece(startPiecePos, destPiecePos, out takenPiece);
                }
                else
                {
                    // Console.WriteLine("Move was not valid. Please try again.");
                    continue;
                }
                moveCounter++;
            
                board.UpdateBoardAndPieces();
                // throw new Exception("activePiece was null from: board.GetPieceByPosition(startPiecePos);");
            }
            else
            {
                errorCodes.Add(1);
            }
            
            UpdateScreen(board, moveCounter, GetColorToMove(moveCounter), errorCodes);
            errorCodes.Clear();

            if (moveCounter <= 100) continue;
            Console.WriteLine("Move limit reached");
            break;
        }
    }
}