
namespace ChessMac;
using static Methods;



internal static class Program
{
    static void Main()
    {
        ChessBoard board = new ChessBoard();
        board.OutputBoard();

        const int numberOfPieces = 16;
        
        Piece[] whitePieces = new Piece[numberOfPieces];
        Piece[] blackPieces = new Piece[numberOfPieces];
        //Piece pawnA = whitePieces[0];
        //Piece pawnB = whitePieces[1];
        //...
        
        
        InitPieces(whitePieces, Piece.PieceColor.White);
        InitPieces(blackPieces, Piece.PieceColor.Black);
        
        PlacePieces(whitePieces, board);
        PlacePieces(blackPieces, board);

        // testing
        // Piece? testPiece = board.GetPiece(new Space.Position(6, 5));
        // board.GetSpace(5, 4).PlacePiece(new Pawn(Piece.PieceColor.Black, Piece.PieceType.Pawn));
        // testPiece.GenerateValidMoves(board);
        // testPiece.PrintValidMoves();
        // board.OutputBoard();
        
        
        int moveCounter = 1;
        while (true)
        {
            if(moveCounter > 100)
                return;
            GeneratePieceMoves(whitePieces, board);
            GeneratePieceMoves(blackPieces, board);
            board.OutputBoard();
        
            Piece.PieceColor colorToMove;
            if (moveCounter % 2 != 0)
                colorToMove = Piece.PieceColor.White;
            else
            {
                colorToMove = Piece.PieceColor.Black;
            }
            
            PlayerMove(board, colorToMove);
            moveCounter++;
        }
        
        
        // GeneratePieceMoves(whitePieces, board);
        // GeneratePieceMoves(blackPieces, board);
        // board.DisplayMoves(whitePieces[9]);
        // board.DisplayMoves(whitePieces[1]);
        
        
        // whitePieces[1].PrintValidMoves();
        // whitePieces[1].PrintValidMovesIndex();
    }
}



    