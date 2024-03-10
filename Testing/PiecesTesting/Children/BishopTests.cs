using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Children;
using static ChessMac.Pieces.Base.Piece;

namespace TestProject2.PiecesTesting.Children;

public class BishopTests
{
    [Test]
    public void BishopConstructor_ReturnsProperBishopObject_OneParameter_Color()
    {
        var piece = new Bishop(PieceColor.Black);
        
    }

    [Test]
    public void GenerateValidMoves_MovesGeneratedAreAllDiagonal()
    {
        var chessboard = new ChessBoard();
        var bishop = PieceFactory.CreatePiece(PieceType.Bishop, PieceColor.White);
        bishop.SetPosition((4, 4));
        
        chessboard.ActivePieces.Add(bishop);
        
        chessboard.UpdateBoardAndPieces();
        
        Methods.UpdateScreen(chessboard, 1, PieceColor.Black, new List<int>());
        bishop.PrintValidMoveList();
        chessboard.VisualizePieceMoves(bishop);
    }
}