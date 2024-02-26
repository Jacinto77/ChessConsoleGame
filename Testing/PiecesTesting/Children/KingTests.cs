using ChessMac.Board;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;
using static ChessMac.Pieces.Base.Piece;
namespace TestProject2.PiecesTesting.Children;

public class KingTests
{
    [Test]
    public void Clone_CastlePositionsAreProperlyCloned()
    {
        var king = new King(PieceColor.Black, (4, 4));
        king.PrintAttributes();
        var newKing = king.Clone();
        newKing.PrintAttributes();
    }

    [Test]
    public void GenerateValidMoves_CastlingWorks()
    {
        var chessboard = new ChessBoard();
        
        var king = new King(PieceColor.White, (7, 4));
        var queenRook = new Rook(PieceColor.White, (7, 0));
        var kingRook = new Rook(PieceColor.White, (7, 7));
        
        chessboard.ActivePieces.Add(king);
        chessboard.ActivePieces.Add(queenRook);
        chessboard.ActivePieces.Add(kingRook);
        
        chessboard.UpdateBoardAndPieces();
        
        king.PrintAttributes();
        queenRook.PrintAttributes();
        kingRook.PrintAttributes();

        king.Move((7, 4));
        
        chessboard.UpdateBoardAndPieces();
        king.PrintAttributes();
    }
}