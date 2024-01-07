namespace ChessMac;

using static Methods;

public enum PieceColor
{
    White,
    Black,
    Null
}

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King,
    Null
}

public class ChessBoard
{
    
    // initializes board with all spaces set to []
    public ChessBoard()
    {
        InitBoardPieces();
    }
    
    public Piece[,] BoardPieces = new Piece[8, 8];
    
    private readonly int[] rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };
    
    
    public ChessBoard DeepCopy()
    {
        var tempBoard = new ChessBoard();
        for (var row = 0; row < 8; row++)
        for (var col = 0; col < 8; col++)
        {
            var pieceToCopy = GetPieceByIndex((row, col));
            if (pieceToCopy.Type is PieceType.Null) continue;
            
            var tempPiece = pieceToCopy.DeepCopy();
            tempBoard.PlacePiece(tempPiece, (row, col));
        }

        return tempBoard;
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
        {
            for (var col = 0; col < 8; col++)
            {
                var activePiece = GetPieceByIndex((row, col));
                if (activePiece.Type == PieceType.Null) continue;
                activePiece.ClearValidMoves();
            }
        }
    }

    public void GeneratePieceMoves()
    {
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                var activePiece = GetPieceByIndex((row, col));
                if (activePiece.Type == PieceType.Null) continue;
                activePiece.GenerateValidMoves(this, row, col);
            }
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
    public (int row, int col) ConvertPosToIndices((char col, int row) position)
    {
        var colIndex = -1;
        var rowIndex = -1;
        colIndex = position.col switch
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

        rowIndex = position.row switch
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

        return new ValueTuple<int, int>(rowIndex, colIndex);
    }

    public static bool IsWithinBoard((int row, int col) position)
    {
        return position.row is <= 7 and >= 0 && position.col is <= 7 and >= 0;
    }

    public void PlacePiece(Piece inPiece, (int row, int col) position)
    {
        if (!IsWithinBoard(position)) throw new Exception("PlacePiece() arguments are not within bounds of ChessBoard");
        BoardPieces[position.row, position.col] = inPiece;
    }

    public void MovePiece((int row, int col) startPos, (int row, int col) destPos, out Piece takenPiece)
    {
        takenPiece = GetPieceByIndex(destPos);
        PlacePiece(GetPieceByIndex(startPos), destPos);
        PlacePiece(new Piece(), startPos);
    }

    // TODO: split up prompts for pieceToMove and destPiece input to simplify validation checking
    public bool InitialMoveValidation(Piece activePiece, PieceColor colorToMove, (int row, int col) destPos)
    {
        if (activePiece.Color != colorToMove)
        {
            Console.WriteLine("That ain't your piece");
            return false;
        }
        return true;
    }

    public bool DestinationValidation(Piece activePiece, PieceColor colorToMove, (int row, int col) destPos)
    {
        if (!activePiece.IsMoveValid(destPos))
        {
            Console.WriteLine("move is not valid");
            activePiece.PrintValidMoves();
            return false;
        }
        return true;
    }

    public bool ValidateAndMovePiece(PieceColor colorToMove, (int row, int col) startPos, (int row, int col) destPos)
    {
        Piece pieceBeingMoved = GetPieceByIndex(startPos);
        bool isMoveValid = InitialMoveValidation(pieceBeingMoved, colorToMove, destPos);
        if (isMoveValid == false)
        {
            Console.WriteLine("Destination is not a valid move");
            return false;
        }

        Piece takenPiece;
        MovePiece(startPos, destPos, out takenPiece);

        if (pieceBeingMoved.Type == PieceType.Pawn)
            CheckAndPromotePawn(pieceBeingMoved, this, destPos.row);

        ClearThreats();
        GeneratePieceMoves();
        AddThreats();

        //TODO king in check validation
        // if (IsKingInCheck(isMoveValid.Color))
        // {
        //     Console.WriteLine("Your king is in check!");
        //     return false;
        // }

        return true;
    }

    public void AddThreats()
    {
        foreach (var piece in BoardPieces)
        {
            if (piece.Type == PieceType.Null) continue;

            var validMoves = piece.GetValidMoveList();
            
            foreach (var move in validMoves) 
               GetPieceByIndex(move).SetThreat();
        }
    }

    public void ClearThreats()
    {
        foreach (var piece in BoardPieces) piece.ClearThreat();
    }

    // public bool IsKingInCheck(PieceColor inColor)
    // {
    //     if (inColor == PieceColor.Black)
    //         return BoardPieces[blackKingPos.row, blackKingPos.col]!.IsThreatened;
    //     return BoardPieces[whiteKingPos.row, whiteKingPos.col]!.IsThreatened;
    // }

    public Piece GetPieceByIndex((int row, int col) piece)
    {
        // if (BoardPieces[piece.row, piece.col]!.Type is PieceType.NULL)
        // {
        //     Console.WriteLine("Piece is null: Chessboard.GetPieceByIndex()");
        //     return new Piece();
        // }

        return BoardPieces[piece.row, piece.col];
    }
}