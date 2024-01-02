namespace ChessMac;

using static Methods;

public enum PieceColor
{
    White,
    Black,
    NULL
}

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King,
    NULL
}

public class ChessBoard
{
    
    // initializes board with all spaces set to []
    public ChessBoard()
    {
        InitBoardPieces();
    }
    
    public readonly char EmptySpaceIcon = '\u2610';

    private (int row, int col) blackKingPos;

    public Piece?[,] BoardPieces = new Piece[8, 8];

    // array of numbers 1-8 to be used by OutputBoard()
    private readonly int[] rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };

    private (int row, int col) whiteKingPos;
    
    // TODO copying is producing a tempBoard with all pieces set to Piece, 
    // and showing as Pawns
    public ChessBoard DeepCopy()
    {
        var tempBoard = new ChessBoard();
        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
        {
            if (BoardPieces[row, col] is null) continue;
            var tempPiece = BoardPieces[row, col]?.DeepCopy();
            tempBoard.BoardPieces[row, col] = tempPiece;
        }

        return tempBoard;
    }

    public void UpdateKingPositions(PieceColor inColor, (int row, int col) inPos)
    {
        if (inColor is PieceColor.Black)
            blackKingPos = inPos;
        else whiteKingPos = inPos;
    }

    public void InitBoardPieces()
    {
        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
            BoardPieces[row, col] = new Piece();
    }

    public void PlacePieces()
    {
        // black pieces
        BoardPieces[0, 0] = new Rook(PieceColor.Black);
        BoardPieces[0, 1] = new Knight(PieceColor.Black);
        BoardPieces[0, 2] = new Bishop(PieceColor.Black);
        BoardPieces[0, 3] = new Queen(PieceColor.Black);
        BoardPieces[0, 4] = new King(PieceColor.Black);
        BoardPieces[0, 5] = new Bishop(PieceColor.Black);
        BoardPieces[0, 6] = new Knight(PieceColor.Black);
        BoardPieces[0, 7] = new Rook(PieceColor.Black);

        BoardPieces[1, 0] = new Pawn(PieceColor.Black);
        BoardPieces[1, 1] = new Pawn(PieceColor.Black);
        BoardPieces[1, 2] = new Pawn(PieceColor.Black);
        BoardPieces[1, 3] = new Pawn(PieceColor.Black);
        BoardPieces[1, 4] = new Pawn(PieceColor.Black);
        BoardPieces[1, 5] = new Pawn(PieceColor.Black);
        BoardPieces[1, 6] = new Pawn(PieceColor.Black);
        BoardPieces[1, 7] = new Pawn(PieceColor.Black);

        // white pieces
        BoardPieces[7, 0] = new Rook(PieceColor.White);
        BoardPieces[7, 1] = new Knight(PieceColor.White);
        BoardPieces[7, 2] = new Bishop(PieceColor.White);
        BoardPieces[7, 3] = new Queen(PieceColor.White);
        BoardPieces[7, 4] = new King(PieceColor.White);
        BoardPieces[7, 5] = new Bishop(PieceColor.White);
        BoardPieces[7, 6] = new Knight(PieceColor.White);
        BoardPieces[7, 7] = new Rook(PieceColor.White);

        BoardPieces[6, 0] = new Pawn(PieceColor.White);
        BoardPieces[6, 1] = new Pawn(PieceColor.White);
        BoardPieces[6, 2] = new Pawn(PieceColor.White);
        BoardPieces[6, 3] = new Pawn(PieceColor.White);
        BoardPieces[6, 4] = new Pawn(PieceColor.White);
        BoardPieces[6, 5] = new Pawn(PieceColor.White);
        BoardPieces[6, 6] = new Pawn(PieceColor.White);
        BoardPieces[6, 7] = new Pawn(PieceColor.White);
    }

    public void TestingPlacePieces()
    {
        BoardPieces[7, 0] = new Rook(PieceColor.White);
        BoardPieces[7, 7] = new Rook(PieceColor.White);
        BoardPieces[7, 4] = new King(PieceColor.White);
    }

    public void ClearValidMoves()
    {
        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
        {
            if (BoardPieces[row, col]?.Icon == EmptySpaceIcon) continue;
            BoardPieces[row, col]?.ClearValidMoves();
        }
    }

    public void GeneratePieceMoves()
    {
        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
        {
            if (BoardPieces[row, col]?.Icon == EmptySpaceIcon) continue;
            BoardPieces[row, col]?.GenerateValidMoves(this, row, col);
        }
    }

    // Output Board Display in ASCII to console
    public void OutputBoard()
    {
        // Black side label
        Console.WriteLine("\t\t\t\t BLACK\n");
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");

        for (var row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(rowNums[row] + @"     |" + "\t");

            for (var col = 0; col < 8; col++)
            {
                Console.Write(BoardPieces[row, col]?.Icon);

                if (col < 7) Console.Write("\t");

                // skip to next line after reaching last item in row
                if (col == 7)
                {
                    // row number labels
                    Console.Write(@"  |" + "\t");
                    Console.Write(rowNums[row]);
                    Console.WriteLine();
                    if (row < 7)
                    {
                        Console.Write(@"      |" + "\t\t\t\t\t\t\t\t" + @"   |");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(@"      ______________________________________________________________");
                    }
                }
            }
        }

        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH\n");
        // White side label
        Console.WriteLine("\t\t\t\t WHITE");
    }

    // not used switch method to convert H3 input to [2, 7] output
    public Tuple<int, int> ConvertPosToIndices(Tuple<char, int> position)
    {
        var colIndex = -1;
        var rowIndex = -1;
        colIndex = position.Item1 switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => colIndex
        };

        rowIndex = position.Item2 switch
        {
            1 => 7,
            2 => 6,
            3 => 5,
            4 => 4,
            5 => 3,
            6 => 2,
            7 => 1,
            8 => 0,
            _ => rowIndex
        };

        return new Tuple<int, int>(colIndex, rowIndex);
    }

    public static bool IsWithinBoard(int currentRow, int currentCol)
    {
        return currentCol is <= 7 and >= 0 && currentRow is <= 7 and >= 0;
    }

    public void PlacePiece(Piece inPiece, (int row, int col) position)
    {
        BoardPieces[position.row, position.col] = inPiece;
    }

    public void MovePiece((int row, int col) startPos, (int row, int col) destPos)
    {
        BoardPieces[destPos.row, destPos.col] = BoardPieces[startPos.row, startPos.col];
        BoardPieces[startPos.row, startPos.col] = new Piece();
    }

    public static Piece? InitialMoveValidation(Piece? activePiece, PieceColor colorToMove, ValueTuple<int, int> destPos)
    {
        if (activePiece is null)
        {
            Console.WriteLine("Piece is null");
            return null;
        }

        if (activePiece.Color != colorToMove)
        {
            Console.WriteLine("That ain't your piece");
            return null;
        }

        if (activePiece.IsMoveValid(destPos) == false)
        {
            Console.WriteLine("move is not valid");
            activePiece.PrintValidMoves();
            return null;
        }

        return activePiece;
    }

    public bool IsValidMove(PieceColor colorToMove, (int row, int col) startPos, (int row, int col) destPos)
    {
        var pieceBeingMoved = BoardPieces[startPos.row, startPos.col];
        var destSpace = BoardPieces[destPos.row, destPos.col];

        var testedMove = InitialMoveValidation(pieceBeingMoved, colorToMove, destPos);
        if (testedMove is null)
        {
            Console.WriteLine();
            return false;
        }

        if (destSpace is not null)
        {
            // pieceBeingMoved.TakenPieces.Add(destSpace);
        }

        MovePiece((startPos.row, startPos.col), (destPos.row, destPos.col));

        if (testedMove.Type == PieceType.Pawn)
            CheckAndPromotePawn(testedMove, this, destPos.row);

        ClearThreats();
        GeneratePieceMoves();
        AddThreats();

        if (IsKingInCheck(testedMove.Color))
        {
            Console.WriteLine("Your king is in check!");
            return false;
        }

        return true;
    }

    public void AddThreats()
    {
        foreach (var piece in BoardPieces)
        {
            if (piece is null || piece.Type == PieceType.Pawn) continue;

            var validMoves = piece.GetValidMoveList();
            foreach (var move in validMoves) BoardPieces[move.row, move.col]?.SetThreat();
        }
    }

    public void ClearThreats()
    {
        foreach (var piece in BoardPieces) piece?.ClearThreat();
    }

    public bool IsKingInCheck(PieceColor inColor)
    {
        if (inColor == PieceColor.Black)
            return BoardPieces[blackKingPos.row, blackKingPos.col]!.IsThreatened;
        return BoardPieces[whiteKingPos.row, whiteKingPos.col]!.IsThreatened;
    }

    public Piece GetPieceByIndex((int row, int col) piece)
    {
        if (BoardPieces[piece.row, piece.col] is null)
        {
            Console.WriteLine("Piece is null: Chessboard.GetPieceByIndex()");
            return new Piece();
        }

        return BoardPieces[piece.row, piece.col];
    }
}