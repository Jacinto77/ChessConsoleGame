using ChessMac.Board;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;

namespace TestProject2;

using static Piece;
using static PieceFactory;
using static Methods;

public class MethodsTests
{
    [Test]
    public void HasPlayerSelectedCorrectColorPiece_CorrectlyReturnsBoolOnColorToMove()
    {
        Piece activePiece; 
        PieceColor colorToMove;
        
        var counter = 0;
        var limit = 10;
        while (counter < limit)
        {
            var rand = new Random();
            var flip = rand.Next(0, 2);
            PieceColor colorOfPiece = flip == 0 ? PieceColor.Black : PieceColor.White;
            activePiece = CreatePiece("Pawn", colorOfPiece);
            
            colorToMove = PieceColor.Black;
            if (colorOfPiece == colorToMove)
            {
                Assert.IsTrue(HasPlayerSelectedCorrectColorPiece(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            else
            {
                Assert.IsFalse(HasPlayerSelectedCorrectColorPiece(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            
            colorToMove = PieceColor.White;
            if (colorOfPiece == colorToMove)
            {
                Assert.IsTrue(HasPlayerSelectedCorrectColorPiece(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            else
            {
                Assert.IsFalse(HasPlayerSelectedCorrectColorPiece(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            counter++;
        }
    }
}