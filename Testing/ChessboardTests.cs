using ChessMac;
using ChessMac.Board;
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
        
        var piece = PieceFactory.CreatePiece(Piece.PieceType.Pawn.ToString(), Piece.PieceColor.Black);
        (int row, int col) piecePos = (4, 4);
        
        chessboard.PlacePiece(piece, piecePos);
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.Type, Is.EqualTo(piece.Type));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.Icon, Is.EqualTo(piece.Icon));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.Color, Is.EqualTo(piece.Color));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.IsThreatened, Is.EqualTo(piece.IsThreatened));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.MoveCounter, Is.EqualTo(piece.MoveCounter));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.HasMoved, Is.EqualTo(piece.HasMoved));
        Assert.That(chessboard.BoardPieces[piecePos.row, piecePos.col]?.IsPinned, Is.EqualTo(piece.IsPinned));
        
        TestContext.WriteLine($"{chessboard.BoardPieces[piecePos.row, piecePos.col]?.GetType()} | {piece.GetType()}");

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
                var piece = chessboard.BoardPieces[row, col];
                var pieceCopy = chessboardCopy.BoardPieces[row, col];
                
                Assert.That(piece?.Type, Is.EqualTo(pieceCopy?.Type));
                Assert.That(piece?.Icon, Is.EqualTo(pieceCopy?.Icon));
                Assert.That(piece?.Color, Is.EqualTo(pieceCopy?.Color));
                Assert.That(piece?.IsThreatened, Is.EqualTo(pieceCopy?.IsThreatened));
                Assert.That(piece?.MoveCounter, Is.EqualTo(pieceCopy?.MoveCounter));
                Assert.That(piece?.HasMoved, Is.EqualTo(pieceCopy?.HasMoved));
                Assert.That(piece?.IsPinned, Is.EqualTo(pieceCopy?.IsPinned));
                
                TestContext.WriteLine($"Row: {row} | Col: {col} {chessboard.BoardPieces[row, col]?.GetType()} | " +
                                      $"{chessboardCopy.BoardPieces[row, col]?.GetType()}");

            }
        }
    }

    [Test]
    public void GeneratePieceMoves_PiecesHaveMovesInValidMoves()
    {
        var chessboard = new ChessBoard();
        chessboard.InitializeActivePieces();
        chessboard.PopulateBoardPieces();
        
        //TODO add a testing board with pieces in various other positions
        chessboard.GeneratePieceMoves();
        
        bool hasMoves = false;

        foreach (var piece in chessboard.ActivePieces)
        {
            if (piece.GetValidMoveList().Count > 0)
                hasMoves = true;
            TestContext.Write($"{Methods.ConvertIndexToPos(piece.Position)}: \t");
            TestContext.WriteLine($"{piece.GetType()} " +
                                  $"{piece.GetValidMoveList().Count}");
        }
        
        // for (var row = 0; row < 8; row++)
        // {
        //     for (var col = 0; col < 8; col++)
        //     {
        //         if (chessboard.BoardPieces[row, col]?.GetValidMoveList().Count > 0)
        //             hasMoves = true;
        //         TestContext.Write($"{row}, {col}: \t");
        //         TestContext.WriteLine($"{chessboard.BoardPieces[row, col]?.GetType()} " +
        //                               $"{chessboard.BoardPieces[row, col]?.GetValidMoveList()}");
        //     }
        // }
        Assert.That(hasMoves, Is.True);
        
    }
    
    [Test]
    public void ClearValidMoves_AllPiecesHaveNoValidMoves()
    {
        var chessboard = new ChessBoard();
        chessboard.InitializeActivePieces();
        chessboard.PopulateBoardPieces();
        chessboard.GeneratePieceMoves();
        
        
        bool hasMoves = false;
        chessboard.AddThreats();
        chessboard.ClearValidMoves();
        
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                if (chessboard.BoardPieces[row, col]?.GetValidMoveList().Count > 0)
                    hasMoves = true;
                TestContext.Write($"{row}, {col}: \t");
                TestContext.WriteLine($"{chessboard.BoardPieces[row, col]?.GetType()} " +
                                      $"{chessboard.BoardPieces[row, col]?.GetValidMoveList()}");
            }
        }
        Assert.That(hasMoves, Is.False);
    }

    [Test]
    public void ConvertPosToIndices_ProperConversion()
    {
        
        List<(char col, int row)> testInput = new List<(char col, int row)>
        {
            ('A', 1),
            ('B', 2),
            ('C', 3),
            ('D', 4),
            ('E', 5),
            ('F', 6),
            ('G', 7),
            ('H', 8)
        };
        
        Assert.That(ConvertPosToIndices(testInput[0]), Is.EqualTo((7, 0)));
        Assert.That(ConvertPosToIndices(testInput[1]), Is.EqualTo((6, 1)));
        Assert.That(ConvertPosToIndices(testInput[2]), Is.EqualTo((5, 2)));
        Assert.That(ConvertPosToIndices(testInput[3]), Is.EqualTo((4, 3)));
        Assert.That(ConvertPosToIndices(testInput[4]), Is.EqualTo((3, 4)));
        Assert.That(ConvertPosToIndices(testInput[5]), Is.EqualTo((2, 5)));
        Assert.That(ConvertPosToIndices(testInput[6]), Is.EqualTo((1, 6)));
        Assert.That(ConvertPosToIndices(testInput[7]), Is.EqualTo((0, 7)));
    }

    [Test]
    public void IsWithinBoard_RejectsOutOfBoundsAsFalse()
    {
        List<(int row, int col)> testInputs = new List<(int row, int col)>
        {
            // true
            (0, 0),
            (0, 7),
            (7, 0),
            (7, 7),
            // false
            (-1, 4),
            (8, 5),
            (0, 9),
            (-5, 7)
        };
        
        Assert.That(IsWithinBoard(testInputs[0]), Is.True);
        Assert.That(IsWithinBoard(testInputs[1]), Is.True);
        Assert.That(IsWithinBoard(testInputs[2]), Is.True);
        Assert.That(IsWithinBoard(testInputs[3]), Is.True);
        
        Assert.That(IsWithinBoard(testInputs[4]), Is.False);
        Assert.That(IsWithinBoard(testInputs[5]), Is.False);
        Assert.That(IsWithinBoard(testInputs[6]), Is.False);
        Assert.That(IsWithinBoard(testInputs[7]), Is.False);
    }

    [Test]
    public void PlacePiece_PieceIsPlacedAtPos()
    {
        var chessboard = new ChessBoard();
        var testPos = new List<(int row, int col)>
        {
            (0, 2),
            (3, 3),
            (5, 2)
        };
        var testPieces = new List<Piece>
        {
            PieceFactory.CreatePiece("Pawn", Piece.PieceColor.Black),
            PieceFactory.CreatePiece("Rook", Piece.PieceColor.White),
            PieceFactory.CreatePiece("Queen", Piece.PieceColor.Black)
        };
        for (int i = 0; i < testPos.Count; i++)
        {
            chessboard.PlacePiece(testPieces[i], testPos[i]);
        }

        int countPieces = 0;
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                try
                {
                    if (chessboard.BoardPieces[row, col] is not null)
                        countPieces++;
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }
        }
        
        Assert.That(countPieces, Is.EqualTo(testPos.Count));

        for (int i = 0; i < testPos.Count; i++)
        {
            Assert.That(chessboard.BoardPieces[testPos[i].row, testPos[i].col], Is.EqualTo(testPieces[i]));
        }
    }

    [Test]
    public void MovePiece_PieceIsMovedPriorSpaceIsNull()
    {
        const int limit = 50;
        var rand = new Random();
        var chessboard = new ChessBoard();
        chessboard.PlacePieces();

        int counter = 0;
        while (counter < limit)
        {
            (int row, int col) startPos = (rand.Next(0, 8), rand.Next(0, 8));
            (int row, int col) destPos = (rand.Next(0, 8), rand.Next(0, 8));

            if (startPos == destPos) continue;
            
            Piece? startPiece = chessboard.GetPieceByIndex(startPos);
            Piece? destPiece = chessboard.GetPieceByIndex(destPos);

            if (startPiece is null) { continue; }
            
            var destPosNullFlag = chessboard.GetPieceByIndex(destPos) is null;
            TestContext.WriteLine($"--Iteration--: {counter + 1}: ");
            TestContext.WriteLine($"Start piece: {startPiece.GetType()}");
            TestContext.WriteLine($"Dest is null: {destPiece?.GetType() is null}");
            
            chessboard.MovePiece(startPos, destPos, out var discardPiece);
            
            TestContext.WriteLine($"{startPos} : {destPos}");
            TestContext.WriteLine($"{chessboard.BoardPieces[startPos.row, startPos.col]?.GetType()}");
            TestContext.WriteLine($"{chessboard.BoardPieces[destPos.row, destPos.col]?.GetType()}");
            TestContext.WriteLine($"Taken Piece: {discardPiece?.GetType()}");
            TestContext.WriteLine("----");
            
            Assert.That(discardPiece, Is.EqualTo(destPiece));
            Assert.That(chessboard.BoardPieces[startPos.row, startPos.col], Is.Null);
            Assert.That(chessboard.BoardPieces[destPos.row, destPos.col], !Is.Null);
            counter++;
        }
    }
    
    [Test]
    public void ValidateAndMovePiece_AllChecksPassPieceIsMoved()
    {
        
    }
    
    [Test]
    public void AddThreats_AllPiecesHaveThreatsAssigned()
    {
        
    }

    [Test]
    public void InitializeActivePieces_AllPiecesAddedToList()
    {
        var chessboard = new ChessBoard();
        chessboard.InitializeActivePieces();
        
        Assert.That(chessboard.ActivePieces, Has.Count.EqualTo(32));
        //Assert.That(chessboard.BoardPieces[0, 0]!.Type, Is.EqualTo(Piece.PieceType.Rook));

        foreach (var piece in chessboard.ActivePieces)
        {
            Assert.That(piece, !Is.Null);
        }
    }
    
    [Test]
    public void PopulateBoardPieces_AllPiecesAreAdded()
    {
        var chessboard = new ChessBoard();
        chessboard.InitializeActivePieces();
        
        chessboard.PopulateBoardPieces();
        
        for (int row = 0; row < chessboard.BoardPieces.GetLength(0); row++)
        {
            for (int col = 0; col < chessboard.BoardPieces.GetLength(0); col++)
            {
                if (chessboard.BoardPieces[row, col] is null) continue;
                Assert.Contains(chessboard.BoardPieces[row, col], chessboard.ActivePieces);
                TestContext.WriteLine($"{chessboard.BoardPieces[row, col]}");
            }
        }
    }
    
    [Test]
    public void ClearThreats_ThreatsAreRemovedFromBoard()
    {
        //TODO
        var chessboard = new ChessBoard();
        
        chessboard.PlacePieces();
        chessboard.GeneratePieceMoves();
        chessboard.AddThreats();
        for(int row = 0; row < chessboard.BoardPieces.GetLength(0); row ++)
        for (int col = 0; col < chessboard.BoardPieces.GetLength(0); col++)
        {
            var piece = chessboard.BoardPieces[row, col];
            if (piece is null) continue;
            
            TestContext.Write($"{piece.IsThreatened}\t");
            TestContext.WriteLine($"{piece.Type}");
            foreach (var move in piece.GetValidMoveList())
            {
                TestContext.WriteLine($"{Methods.ConvertIndexToPos(move)}\n---");
            }
        }
        
        chessboard.ClearThreats();
        for(int row = 0; row < chessboard.BoardPieces.GetLength(0); row ++)
        for (int col = 0; col < chessboard.BoardPieces.GetLength(0); col++)
        {
            var piece = chessboard.BoardPieces[row, col];
            if (piece is null) continue;
            
            TestContext.Write($"{piece.IsThreatened}\t");
            TestContext.WriteLine($"{piece.Type}");
            foreach (var move in piece.GetValidMoveList())
            {
                TestContext.WriteLine($"{Methods.ConvertIndexToPos(move)}\n---");
            }
        }
    }

    [Test]
    public void GetPieceByIndex_GetsReferenceToPieceByIndex()
    {
        var chessboard = new ChessBoard();
        chessboard.PlacePieces();

        List<(int row, int col)> validTestInputs = new List<(int row, int col)>
        {
            (0, 0),
            (1, 2),
            (7, 7),
            (6, 6),
            (0, 4),
            (0, 7)
        };
        foreach (var index in validTestInputs)
        {
            Piece? testPiece = chessboard.GetPieceByIndex(index);
            Piece? validationPiece = chessboard.BoardPieces[index.row, index.col];
            TestContext.WriteLine($"Test Piece: \t\t{testPiece!.GetType()} {testPiece!.Color} {index}");
            TestContext.WriteLine($"Validation Piece: \t{validationPiece!.GetType()} {validationPiece!.Color} {index}\n");
            Assert.That(testPiece, Is.EqualTo(validationPiece));
        }
        
        List<(int row, int col)> invalidTestInputs = new List<(int row, int col)>
        {
            (-1, 0),
            (1, 9),
            (7, 8),
            (-1, 100),
            (9, 9),
            (-1, -1)
        };
        foreach (var index in invalidTestInputs)
        {
            Piece? testPiece = chessboard.GetPieceByIndex(index);
            Piece? validationPiece = null;
            TestContext.WriteLine($"Test Piece: \t\t{testPiece?.GetType()} {testPiece?.Color} {index}");
            TestContext.WriteLine($"Validation Piece: \t{validationPiece?.GetType()} {validationPiece?.Color} {index}\n");
            Assert.That(testPiece, Is.Null);
        }
    }
}