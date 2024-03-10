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
    public void IsPieceCorrectColor_CorrectlyReturnsBoolOnColorToMove()
    {
        var counter = 0;
        const int limit = 10;
        
        while (counter < limit)
        {
            Piece activePiece; 
            PieceColor colorToMove;
            
            var rand = new Random();
            var flip = rand.Next(0, 2);
            
            PieceColor colorOfPiece = flip == 0 ? PieceColor.Black : PieceColor.White;
            activePiece = CreatePiece(PieceType.Pawn, colorOfPiece);
            
            colorToMove = PieceColor.Black;
            if (colorOfPiece == colorToMove)
            {
                Assert.IsTrue(IsPieceCorrectColor(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            else
            {
                Assert.IsFalse(IsPieceCorrectColor(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            
            colorToMove = PieceColor.White;
            if (colorOfPiece == colorToMove)
            {
                Assert.IsTrue(IsPieceCorrectColor(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            else
            {
                Assert.IsFalse(IsPieceCorrectColor(activePiece, colorToMove));
                TestContext.WriteLine($"Color to move: {colorToMove}");
                TestContext.WriteLine($"Color of Piece: {activePiece.Color}");
            }
            counter++;
        }
    }

    [Test]
    public void IsInputValid_CorrectlyValidatesStringInput()
    {
        string[] moves = { "a2 a4", "b1 c3", "h7 h8", "e2 e4", "a9 Z9" };
        
        foreach (string move in moves)
        {
            if (CheckIsInputValidRegex(move))
            {
                Console.WriteLine($"'{move}' is a valid chess move notation.");
            }
            else
            {
                Console.WriteLine($"'{move}' is NOT a valid chess move notation.");
            }
        }
    }
    
    
}