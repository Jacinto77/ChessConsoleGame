
namespace ChessMac;
using static Methods;

// TODO: turn all Tuple<int, int> into ValueTuples<int row, int col>
internal static class Program
{
    static void Main()
    {
        ChessBoard board = new ChessBoard();
        board.PlacePieces();
        board.OutputBoard();

        int moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }

            board.GeneratePieceMoves();

            //board.OutputBoard();
            var tempBoard = board.DeepCopy();
            var colorToMove = moveCounter % 2 != 0
                ? PieceColor.White
                : PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            Tuple<string, string> parsedInput = GetPlayerMove();

            var startPiece = ConvertPosToIndex(parsedInput.Item1);
            var destPos = ConvertPosToIndex(parsedInput.Item2);

            if (tempBoard.IsValidMove(colorToMove, startPiece, destPos))
            {
                board.MovePiece(startPiece, destPos);
                board.BoardPieces[destPos.row, destPos.col]!.HasMoved = true;
                board.BoardPieces[destPos.row, destPos.col]!.MoveCounter++;
            }
            else continue;
            
            moveCounter++;
            board.OutputBoard();
        }
        
    }
}