using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;

namespace TestProject2.PiecesTesting.Children;

public class RookTests
{
    [Test]
    public void GenerateValidMoves_MovesGeneratedAreAllHorizVert()
    {
        var chessboard = new ChessBoard();
        var rook = PieceFactory.CreatePiece(Piece.PieceType.Rook, Piece.PieceColor.White);
        rook.SetPosition((4, 4));
        
        chessboard.ActivePieces.Add(rook);
        
        chessboard.UpdateBoardAndPieces();
        
        Methods.UpdateScreen(chessboard, 1, Piece.PieceColor.Black, new List<int>());
        rook.PrintValidMoveList();
        chessboard.VisualizePieceMoves(rook);
    }
}