namespace ChessMac;

using static Methods;

// TODO: turn all Tuple<int, int> into ValueTuples<int row, int col>
// TODO: test all methods
internal static class Program
{
    private static void Main()
    {
        var board = new ChessBoard();
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
                ? PieceColor.White
                : PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            // TODO change user input logic and validation to handle different methods of piece choice
            // and move selection. will need to also handle help prompts among others
            
            var parsedInput = GetPlayerMove();
            // TODO add validation for destinationPiece separate from pieceChoice
            var startPiecePos = ConvertPosToIndex(parsedInput.pieceToMove);
            var destPiecePos = ConvertPosToIndex(parsedInput.moveDestination);

            var activePiece = board.GetPieceByIndex(startPiecePos);
            //activePiece.PrintValidMoves();

            Piece takenPiece;
            if (tempBoard.ValidateAndMovePiece(colorToMove, startPiecePos, destPiecePos))
            {
                board.MovePiece(startPiecePos, destPiecePos, out takenPiece);
                activePiece.SetHasMoved();
                activePiece.IncrementMoveCounter();
            }
            else
            {
                continue;
            }
            activePiece.GenerateValidMoves(board, destPiecePos.row, destPiecePos.col);
            activePiece.PrintValidMoves();
            moveCounter++;
            //board.OutputBoard();
        }
    }
}