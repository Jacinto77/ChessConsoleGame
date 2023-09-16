
namespace ChessMac;
using static Methods;


// TODO: make chessboard a static object

internal static class Program
{
    static void Main()
    {
        ChessBoard board = new ChessBoard();
        board.OutputBoard();

        const int numberOfPieces = 16;
        
        Piece?[] whitePieces = new Piece?[numberOfPieces];
        Piece?[] blackPieces = new Piece?[numberOfPieces];
        //Piece pawnA = whitePieces[0];
        //Piece pawnB = whitePieces[1];
        //...
        
        
        InitPieces(whitePieces, Piece.PieceColor.White);
        InitPieces(blackPieces, Piece.PieceColor.Black);
        
        PlacePieces(whitePieces, board);
        PlacePieces(blackPieces, board);

        // :testing:
        // Piece? testPiece = board.GetPiece(new Space.Position(6, 5));
        // board.GetSpace(5, 4).PlacePiece(new Pawn(Piece.PieceColor.Black, Piece.PieceType.Pawn));
        // testPiece.GenerateValidMoves(board);
        // testPiece.PrintValidMoves();
        // board.OutputBoard();
        
        
        int moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }

            GeneratePieceMoves(whitePieces, board);
            GeneratePieceMoves(blackPieces, board);
            board.OutputBoard();

            var colorToMove = moveCounter % 2 != 0 ? Piece.PieceColor.White 
                                                   : Piece.PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            
            PlayerMove(board, colorToMove);
            moveCounter++;
        }
    }
}



    