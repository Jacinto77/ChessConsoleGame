using ChessMac;
using ChessMac.Board;
using ChessMac.ChessBoard;
using ChessMac.Pieces;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;
using static ChessMac.Board.ChessBoard;

namespace TestProject2;

[TestFixture]
public class ChessboardTests
{
    // [Test]
    // public void InitBoardPieces_AllSlotsAreInitializedWithPieceObjects()
    // {
    //     var chessboard = new ChessBoard();
    //     chessboard.InitBoardPieces();
    //
    //     for (var row = 0; row < 8; row++)
    //     {
    //         for (var col = 0; col < 8; col++)
    //         {
    //             Assert.IsNotNull(chessboard.BoardPieces[row, col], $"BoardPieces[{row},{col}] is null.");
    //             Assert.IsInstanceOf<Piece>(chessboard.BoardPieces[row, col], $"BoardPieces[{row},{col}] is not an instance of Piece.");
    //             TestContext.WriteLine($"Row: {row} | Col: {col} {chessboard.BoardPieces[row, col].GetType()} {chessboard.BoardPieces[row, col].Type}");
    //         }
    //     }
    // }

    [Test]
    public void PlacePieces_AllPiecesAreCorrect()
    {
        var chessboard = new ChessBoard();
        //chessboard.InitBoardPieces();
        chessboard.PlacePieces();
        
        var boardPieces = new Piece?[8, 8];

        // black pieces
        boardPieces[0, 0] = new Rook(Piece.PieceColor.Black);
        boardPieces[0, 1] = new Knight(Piece.PieceColor.Black);
        boardPieces[0, 2] = new Bishop(Piece.PieceColor.Black);
        boardPieces[0, 3] = new Queen(Piece.PieceColor.Black);
        boardPieces[0, 4] = new King(Piece.PieceColor.Black);
        boardPieces[0, 5] = new Bishop(Piece.PieceColor.Black);
        boardPieces[0, 6] = new Knight(Piece.PieceColor.Black);
        boardPieces[0, 7] = new Rook(Piece.PieceColor.Black);

        boardPieces[1, 0] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 1] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 2] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 3] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 4] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 5] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 6] = new Pawn(Piece.PieceColor.Black);
        boardPieces[1, 7] = new Pawn(Piece.PieceColor.Black);

        // white pieces
        boardPieces[7, 0] = new Rook(Piece.PieceColor.White);
        boardPieces[7, 1] = new Knight(Piece.PieceColor.White);
        boardPieces[7, 2] = new Bishop(Piece.PieceColor.White);
        boardPieces[7, 3] = new Queen(Piece.PieceColor.White);
        boardPieces[7, 4] = new King(Piece.PieceColor.White);
        boardPieces[7, 5] = new Bishop(Piece.PieceColor.White);
        boardPieces[7, 6] = new Knight(Piece.PieceColor.White);
        boardPieces[7, 7] = new Rook(Piece.PieceColor.White);

        boardPieces[6, 0] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 1] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 2] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 3] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 4] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 5] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 6] = new Pawn(Piece.PieceColor.White);
        boardPieces[6, 7] = new Pawn(Piece.PieceColor.White);

        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                Assert.That(boardPieces[row, col]?.Type, Is.EqualTo(chessboard.BoardPieces[row, col]?.Type));
                Assert.That(boardPieces[row, col]?.Icon, Is.EqualTo(chessboard.BoardPieces[row, col]?.Icon));
                Assert.That(boardPieces[row, col]?.Color, Is.EqualTo(chessboard.BoardPieces[row, col]?.Color));
                Assert.That(boardPieces[row, col]?.IsThreatened, Is.EqualTo(chessboard.BoardPieces[row, col]?.IsThreatened));
                Assert.That(boardPieces[row, col]?.MoveCounter, Is.EqualTo(chessboard.BoardPieces[row, col]?.MoveCounter));
                Assert.That(boardPieces[row, col]?.HasMoved, Is.EqualTo(chessboard.BoardPieces[row, col]?.HasMoved));
                Assert.That(boardPieces[row, col]?.IsPinned, Is.EqualTo(chessboard.BoardPieces[row, col]?.IsPinned));
                
                TestContext.WriteLine($"Row: {row} | Col: {col} {chessboard.BoardPieces[row, col]?.GetType()}");

            }
        }
    }

    [Test]
    public void PlacePiece_PieceIsPlacedAndSaved()
    {
        var chessboard = new ChessBoard();
        chessboard.PlacePieces();
        
        var pieceToPlace = PieceFactory.CreatePiece(Piece.PieceType.Pawn.ToString(), Piece.PieceColor.Black);
        (int row, int col) locationToPlace = (4, 4);
        
        chessboard.PlacePiece(pieceToPlace, locationToPlace);
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.Type, Is.EqualTo(pieceToPlace.Type));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.Icon, Is.EqualTo(pieceToPlace.Icon));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.Color, Is.EqualTo(pieceToPlace.Color));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.IsThreatened, Is.EqualTo(pieceToPlace.IsThreatened));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.MoveCounter, Is.EqualTo(pieceToPlace.MoveCounter));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.HasMoved, Is.EqualTo(pieceToPlace.HasMoved));
        Assert.That(chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.IsPinned, Is.EqualTo(pieceToPlace.IsPinned));
        
        TestContext.WriteLine($"{chessboard.BoardPieces[locationToPlace.row, locationToPlace.col]?.GetType()} | {pieceToPlace.GetType()}");

    }
    
    [Test]
    public void DeepCopy_AllPiecesAreCopied()
    {
        var chessboard = new ChessBoard();
        chessboard.PlacePieces();
        
        var chessboardCopy = chessboard.DeepCopy();
        
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                Assert.That(chessboard.BoardPieces[row, col]?.Type, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.Type));
                Assert.That(chessboard.BoardPieces[row, col]?.Icon, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.Icon));
                Assert.That(chessboard.BoardPieces[row, col]?.Color, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.Color));
                Assert.That(chessboard.BoardPieces[row, col]?.IsThreatened, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.IsThreatened));
                Assert.That(chessboard.BoardPieces[row, col]?.MoveCounter, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.MoveCounter));
                Assert.That(chessboard.BoardPieces[row, col]?.HasMoved, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.HasMoved));
                Assert.That(chessboard.BoardPieces[row, col]?.IsPinned, Is.EqualTo(chessboardCopy.BoardPieces[row, col]?.IsPinned));
                
                TestContext.WriteLine($"Row: {row} | Col: {col} {chessboard.BoardPieces[row, col]?.GetType()} | {chessboardCopy.BoardPieces[row, col]?.GetType()}");

            }
        }
    }
}