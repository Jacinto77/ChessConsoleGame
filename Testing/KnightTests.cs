using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace TestProject2;

[TestFixture]
public class KnightTests
{
    [Test]
    public void KnightConstructor_ValidMovesArePopulated()
    {
        List<(int row, int col)> validMoveInitialization = new List<(int row, int col)>
        {
            (0, 0),
            (1, 1),
            (7, 7),
            (4, 5)
        };
        
        Piece knight = new Knight(Piece.PieceType.Knight, Piece.PieceColor.White, 
            new List<(int row, int col)>
            {
                validMoveInitialization[0], 
                validMoveInitialization[1],
                validMoveInitialization[2],
                validMoveInitialization[3],
            },
            Piece.WhiteIcons[Piece.PieceType.Knight], false, false, 0, false);
        
        Assert.That(knight.GetValidMoveList(), Has.Count.EqualTo(validMoveInitialization.Count));
        Assert.That(knight.GetValidMoveList()[0], Is.EqualTo(validMoveInitialization[0]));
        
        foreach (var move in knight.GetValidMoveList())
        {
            TestContext.WriteLine(move);
        }
    }
}