namespace TestProject2;
using ChessMac;
using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;
using static ChessMac.Board.ChessBoard;

[TestFixture]
public class PieceTests
{
    [Test]
    public void DeepCopy_PieceIsCopied()
    {
        
    }

    [Test]
    public void Clone_PieceIsCopiedAllPropertiesInitialized()
    {
        
    }

    [Test]
    public void HasMove_ReturnsTrueForValidMoves()
    {
        Piece testPiece = new King(Piece.PieceColor.Black);
        List<(int row, int col)> validTestMoves = new List<(int row, int col)>
        {
            (0, 2),
            (4, 4),
            (9, 9)
        };
        
        testPiece.SetValidMoveList(validTestMoves);
        for (int i = 0; i < validTestMoves.Count; i++)
        {
            TestContext.WriteLine($"{validTestMoves[i]} : {testPiece.GetValidMoveList()[i]}");
            Assert.That(validTestMoves[i], Is.EqualTo(testPiece.GetValidMoveList()[i]));
            Assert.Contains(validTestMoves[i], testPiece.GetValidMoveList());
            Assert.That(testPiece.HasMove(validTestMoves[i]), Is.True);
        }
    }
    
    [Test]
    public void HasMove_ReturnsFalseForInvalidMoves()
    {
        Piece testPiece = new King(Piece.PieceColor.Black);
        
        List<(int row, int col)> validTestMoves = new List<(int row, int col)>
        {
            (0, 2),
            (4, 4),
            (9, 9)
        };
        testPiece.SetValidMoveList(validTestMoves);
        
        List<(int row, int col)> invalidTestMoves = new List<(int row, int col)>
        {
            (0, 1),
            (4, 3),
            (9, 0)
        };
        
        for (int i = 0; i < invalidTestMoves.Count; i++)
        {
            TestContext.WriteLine($"{invalidTestMoves[i]} : {testPiece.GetValidMoveList()[i]}");
            Assert.That(invalidTestMoves[i], !Is.EqualTo(testPiece.GetValidMoveList()[i]));
            Assert.That(testPiece.HasMove(invalidTestMoves[i]), Is.False);
        }
    }

    [Test]
    public void Piece_ConstructorCreatesProperDefaultValues()
    {
        var chessboard = new ChessBoard();
        
        var piece = PieceFactory.CreatePiece("King", Piece.PieceColor.Black);
        piece.Move((4, 4));
        chessboard.ActivePieces.Add(piece);
        chessboard.UpdatePiecePositions();
        chessboard.PopulateBoard();
        chessboard.GeneratePieceMoves();
        chessboard.ActivePieces.Clear();
        piece.PrintAttributes();
        
        piece = new Pawn(Piece.PieceColor.Black, (-1, -1), default, new List<(int row, int col)>(), null, true, true, 4,
            false);
        piece.PrintAttributes();
        piece = new Pawn(Piece.PieceColor.White, (4, 4));
        chessboard.ActivePieces.Add(piece);
        chessboard.UpdatePiecePositions();
        chessboard.PopulateBoard();
        chessboard.GeneratePieceMoves();
        chessboard.ActivePieces.Clear();
        piece.PrintAttributes();

        piece = new Rook(Piece.PieceColor.Black, (0, 0));
        chessboard.ActivePieces.Add(piece);
        chessboard.UpdatePiecePositions();
        chessboard.PopulateBoard();
        chessboard.GeneratePieceMoves();
        chessboard.ActivePieces.Clear();
        piece.PrintAttributes();

    }
}