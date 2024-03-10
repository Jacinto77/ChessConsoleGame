using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;

namespace TestProject2.PiecesTesting.Children;

public class QueenTests
{
    [Test]
    public void GenerateValidMoves_MovesGeneratedAreAllHorizVertDiag()
    {
        var chessboard = new ChessBoard();
        var queen = PieceFactory.CreatePiece(Piece.PieceType.Queen, Piece.PieceColor.White);
        queen.SetPosition((4, 4));
        
        chessboard.ActivePieces.Add(queen);
        
        chessboard.UpdateBoardAndPieces();
        
        Methods.UpdateScreen(chessboard, 1, Piece.PieceColor.Black, new List<int>());
        queen.PrintValidMoveList();
        chessboard.VisualizePieceMoves(queen);
    }
}