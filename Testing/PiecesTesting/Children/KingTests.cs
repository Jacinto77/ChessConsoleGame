using ChessMac.Board;
using ChessMac.Pieces;
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
    
    [Test]
    public void GenerateValidMoves_MovesGeneratedAreCorrectlyLShaped()
    {
        var chessboard = new ChessBoard();
        var king = PieceFactory.CreatePiece(PieceType.King, PieceColor.White);
        king.SetPosition((4, 4));
        
        chessboard.ActivePieces.Add(king);
        
        chessboard.UpdateBoardAndPieces();
        
        Methods.UpdateScreen(chessboard, 1, PieceColor.Black, new List<int>());
        king.PrintValidMoveList();
        chessboard.VisualizePieceMoves(king);
    }

    [Test]
    public void GenerateValidMoves_KingDoesNotAttackOwnPieces()
    {
        var chessboard = new ChessBoard();
        chessboard.InitializeActivePieces();
        chessboard.UpdateBoardAndPieces();
        
        chessboard.ActivePieces.Remove(chessboard.GetPieceByIndex((7, 1)));
        chessboard.ActivePieces.Remove(chessboard.GetPieceByIndex((7, 2)));
        chessboard.ActivePieces.Remove(chessboard.GetPieceByIndex((7, 3)));
        chessboard.ActivePieces.Remove(chessboard.GetPieceByIndex((7, 4)));
        
        chessboard.UpdateBoardAndPieces();
        chessboard.OutputBoard();
        
        var king = PieceFactory.CreatePiece(PieceType.King, PieceColor.White);
        king.SetPosition((7, 4));
        chessboard.ActivePieces.Add(king);
        
        chessboard.UpdateBoardAndPieces();
        // king.SetHasMoved();
        // chessboard.UpdateBoardAndPieces();
        // chessboard.OutputBoard();
        
        Methods.UpdateScreen(chessboard, 1, PieceColor.Black, new List<int>());
        king.PrintValidMoveList();
        chessboard.VisualizePieceMoves(king);
    }
}