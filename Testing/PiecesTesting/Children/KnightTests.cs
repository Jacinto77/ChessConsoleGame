using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace TestProject2.PiecesTesting.Children;


public class KnightTests
{
    [Test]
    public void KnightConstructor_ValidMovesArePopulated()
    {
        Piece knight = new Knight( Piece.PieceColor.White, (-1, -1), Piece.PieceType.Knight, null, Piece.WhiteIcons[Piece.PieceType.Knight]);
        
        List<(int row, int col)> validMoveInitialization = new List<(int row, int col)>
        {
            (0, 0),
            (1, 1),
            (7, 7),
            (4, 5)
        };

        knight.SetValidMoveList(validMoveInitialization);
        
        Assert.That(knight.GetValidMoveList(), Has.Count.EqualTo(validMoveInitialization.Count));
        Assert.That(knight.GetValidMoveList()[0], Is.EqualTo(validMoveInitialization[0]));
        
        foreach (var move in knight.GetValidMoveList())
        {
            TestContext.WriteLine(move);
        }
    }
    [Test]
    public void GenerateValidMoves_MovesGeneratedAreCorrectlyLShaped()
    {
        var chessboard = new ChessBoard();
        var knight = PieceFactory.CreatePiece(Piece.PieceType.Knight, Piece.PieceColor.White);
        knight.SetPosition((4, 4));
        
        chessboard.ActivePieces.Add(knight);
        
        chessboard.UpdateBoardAndPieces();
        
        Methods.UpdateScreen(chessboard, 1, Piece.PieceColor.Black, new List<int>());
        knight.PrintValidMoveList();
        chessboard.VisualizePieceMoves(knight);
    }
}