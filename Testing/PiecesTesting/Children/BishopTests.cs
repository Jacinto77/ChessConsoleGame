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
}