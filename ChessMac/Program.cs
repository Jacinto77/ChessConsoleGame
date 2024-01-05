namespace ChessMac;

using static Methods;

// TODO: turn all Tuple<int, int> into ValueTuples<int row, int col>
// TODO: test all methods
internal static class Program
{
    private static void Main()
    {
        var board = new ChessBoard();
        board.TestingPlacePieces();
        //board.PlacePieces();

        // debugging
        // board.PlacePiece(new Queen(PieceColor.White), (4, 4));

        board.OutputBoard();

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
                ? PieceColor.White
                : PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");

            var parsedInput = GetPlayerMove();
            
            var startPiece = ConvertPosToIndex(parsedInput.pieceToMove);
            var destPos = ConvertPosToIndex(parsedInput.moveDestination);

            board.GetPieceByIndex(startPiece).PrintValidMoves();
            
            if (tempBoard.IsValidMove(colorToMove, startPiece, destPos))
            {
                board.MovePiece(startPiece, destPos);
                var activePiece = board.GetPieceByIndex(destPos);
                activePiece.SetHasMoved();
                activePiece.IncrementMoveCounter();
            }
            else
            {
                continue;
            }

            moveCounter++;
            board.OutputBoard();
        }
    }
}