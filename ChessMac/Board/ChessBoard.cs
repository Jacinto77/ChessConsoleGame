using System.ComponentModel.Design;
using System.Runtime.InteropServices.JavaScript;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace ChessMac.Board;

using static Methods;
using static Piece;

public class ChessBoard
{

    public ChessBoard(bool initialize = false)
    {
        if (!initialize) return;
        
        InitializeActivePieces();
        UpdateBoardAndPieces();
    }
    
    public Piece?[,] BoardSpaces = new Piece[8, 8];
    public List<Piece> ActivePieces = new List<Piece>();
    public List<Piece> InactivePieces = new List<Piece>();

    public Dictionary<(int row, int col), Piece> PiecePositions = new();
    public Dictionary<(int row, int col), List<Piece>> ThreatenedSpaces = new();

    private (int row, int col) blackKingPosition;
    private (int row, int col) whiteKingPosition;

    public (int row, int col) GetKingPosition(PieceColor inColor)
    {
        foreach (var piece in ActivePieces)
        {
            if (piece.Color == inColor && piece.Type == PieceType.King)
                return piece.Position;
        }

        throw new Exception($"GetKingPosition() could not find a king of color: {inColor}");
    }
    
    public ChessBoard DeepCopy()
    {
        var tempBoard = new ChessBoard();
        foreach (var piece in ActivePieces)
        {
            tempBoard.ActivePieces.Add(piece.Clone());
        }

        foreach (var piece in InactivePieces)
        {
            tempBoard.InactivePieces.Add(piece.Clone());
        }

        // UpdatePiecePositions();
        // PopulateBoard();
        // ClearValidMoves();
        // GeneratePieceMoves();
        
        tempBoard.UpdatePiecePositions();
        tempBoard.AddPositionThreats();
        tempBoard.PopulateBoard();
        return tempBoard;
    }
    
    public void MovePiece((int row, int col) startPos, (int row, int col) destPos, out Piece? takenPiece)
    {
        var activePiece = GetPieceByPosition(startPos);
        if (activePiece == null)
        {
            takenPiece = null;
            return;
        }

        takenPiece = GetPieceByPosition(destPos);
        if (takenPiece != null)
        {
            ActivePieces.Remove(takenPiece);
            InactivePieces.Add(takenPiece);
        }
        
        activePiece.Move(destPos);
        
        // uncomment if testing alone
        // UpdatePiecePositions();
        // PopulateBoard();
    }
    
    public void MovePieceNoTake((int row, int col) startPos, (int row, int col) destPos)
    {
        var activePiece = GetPieceByPosition(startPos);
        if (activePiece == null)
        {
            return;
        }

        activePiece.Move(destPos);
        
        // uncomment if testing alone
        // UpdatePiecePositions();
        // PopulateBoard();
    }
    
    public Piece? GetPieceByPosition((int row, int col) inIndex)
    {
        // PrintPiecesByPositions();
        // PrintPieceLocationDictionary();
        
        try
        {
            // Console.WriteLine(PiecePositions[inIndex].Type); 
            return PiecePositions[inIndex];
        }
        catch (Exception error)
        {
            
            // Console.WriteLine(error);
            // throw;
            return null;
        }
    }
    
    public Piece? GetPieceByIndex((int row, int col) inIndex)
    {
        try
        { 
            return BoardSpaces[inIndex.row, inIndex.col];
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Value provided is not within the bounds of the chessboard or the value is null. Returning null.");
            return null;
        }
    }
    
    // Dictionary of positions and the piece that is at that position
    public void UpdatePiecePositions()
    {
        PiecePositions.Clear();
        
        foreach (var piece in ActivePieces)
        {
            PiecePositions[piece.Position] = piece;
        }
        // foreach (var kvp in ActivePieces)
        // {
        //     //Console.WriteLine($"{ChessBoard.ConvertIndexToPos(kvp.Key)}\t{kvp.Value.Type}\t{kvp.Value.Icon}\t{kvp.Value.Color}");
        //     Console.WriteLine($"{kvp.Type}");
        // }
    }
    
    public void AddPositionThreats()
    {
        ClearPositionThreats();
        
        foreach (var piece in ActivePieces)
        {
            foreach (var validMove in piece.GetValidMoveList())
            {
                if (ThreatenedSpaces.TryGetValue(validMove, out var pieces))
                {
                    pieces.Add(piece);
                }
                else
                {
                    ThreatenedSpaces[validMove] = new List<Piece> { piece };
                }
            }
        }
    }

    // public void AddColorSpecificThreats()
    // {
    //     WhiteThreats.Clear();
    //     BlackThreats.Clear();
    //     foreach (var kvp in ThreatenedSpaces)
    //     {
    //         foreach (var piece in kvp.Value)
    //         {
    //             if (piece.Color == Piece.PieceColor.Black)
    //                 WhiteThreats.Add(kvp.Key, piece);
    //         }
    //     }
    // }
    
    public void ClearPositionThreats()
    {
        ThreatenedSpaces.Clear();
    }

    public void RemovePieceByPosition((int row, int col) inPosition)
    {
        PiecePositions.Remove(inPosition);
    }

    public void AddPieceByPosition((int row, int col) inPosition, Piece piece)
    {
        PiecePositions.Add(inPosition, piece);
    }
    
    private readonly int[] _rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };

    public void InitializeActivePieces()
    {
        ActivePieces.Clear();
        
        // black pieces
        ActivePieces.Add( new Rook  (PieceColor.Black, (0, 0)));
        ActivePieces.Add( new Knight(PieceColor.Black, (0, 1)));
        ActivePieces.Add( new Bishop(PieceColor.Black, (0, 2)));
        ActivePieces.Add( new Queen (PieceColor.Black, (0, 3)));
        ActivePieces.Add( new King  (PieceColor.Black, (0, 4)));
        ActivePieces.Add( new Bishop(PieceColor.Black, (0, 5)));
        ActivePieces.Add( new Knight(PieceColor.Black, (0, 6)));
        ActivePieces.Add( new Rook  (PieceColor.Black, (0, 7)));
        
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 0)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 1)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 2)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 3)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 4)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 5)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 6)));
        ActivePieces.Add( new Pawn(PieceColor.Black, (1, 7)));

        // white pieces
        ActivePieces.Add( new Rook  (PieceColor.White, (7, 0)));
        ActivePieces.Add( new Knight(PieceColor.White, (7, 1)));
        ActivePieces.Add( new Bishop(PieceColor.White, (7, 2)));
        ActivePieces.Add( new Queen (PieceColor.White, (7, 3)));
        ActivePieces.Add( new King  (PieceColor.White, (7, 4)));
        ActivePieces.Add( new Bishop(PieceColor.White, (7, 5)));
        ActivePieces.Add( new Knight(PieceColor.White, (7, 6)));
        ActivePieces.Add( new Rook  (PieceColor.White, (7, 7)));
        
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 0)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 1)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 2)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 3)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 4)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 5)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 6)));
        ActivePieces.Add( new Pawn(PieceColor.White, (6, 7)));
        
        
    }

    public void PopulateBoard()
    {
        ClearBoardPieces();
        
        foreach (var piece in ActivePieces)
        {
            var position = piece.Position;
            BoardSpaces[position.row, position.col] = piece;
        }
    }

    private void ClearBoardPieces()
    {
        for (var row = 0; row < BoardSpaces.GetLength(0); row++)
        for (var col = 0; col < BoardSpaces.GetLength(0); col++)
        {
            BoardSpaces[row, col] = null;
        }
    }

    private void InactivatePiece(Piece? inPiece)
    {
        if (inPiece is null) return;
        if (ActivePieces.Contains(inPiece))
        {
            ActivePieces.Remove(inPiece);
            InactivePieces.Add(inPiece);
        }
    }

    public void ClearValidMoves()
    {
        foreach (var piece in ActivePieces)
        {
            piece.ClearValidMoves();
        }
    }

    public void GeneratePieceMoves()
    {
        foreach (var piece in ActivePieces)
        {
            piece.GenerateValidMoves(this);
        }
    }

    // TODO: change output logic to be based on a larger 2x2 array
    //  will simplify output and allow for easier modifications later
    public void OutputBoard(int inMoveCounter = 0, Dictionary<(int row, int col), char>? spacesIcons = null)
    {
        Console.WriteLine("\t\t\t\t BLACK\n");
        
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");

        for (var row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(_rowNums[row] + @"     |" + "\t");

            for (var col = 0; col < 8; col++)
            {
                if (spacesIcons is not null && spacesIcons.TryGetValue((row, col), out char charValue))
                {
                    Console.Write(charValue);   
                }
                else if (PiecePositions.TryGetValue((row, col), out Piece? pieceValue))
                {
                    Console.Write(pieceValue.Icon);
                }
                else Console.Write(EmptySpaceIcon);

                if (col < 7) Console.Write("\t");
                // skip to next line after reaching last item in row
                if (col == 7)
                {
                    // row number labels
                    Console.Write(@"  |" + "\t");
                    Console.Write(_rowNums[row]);
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
        Console.WriteLine($"Move Count: {inMoveCounter}");
    }
    
    // // Output Board Display in ASCII to console
    // public void OutputBoard(int inMoveCounter, Piece? visualizedMoves = null)
    // {
    //     Console.WriteLine("\t\t\t\t BLACK\n");
    //     
    //     Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
    //     Console.WriteLine(@"      ______________________________________________________________");
    //
    //     for (var row = 0; row < 8; row++)
    //     {
    //         // row number labels
    //         Console.Write(_rowNums[row] + @"     |" + "\t");
    //
    //         for (var col = 0; col < 8; col++)
    //         {
    //             if (PiecePositions.ContainsKey((row, col)))
    //             {
    //                 Console.Write(PiecePositions[(row, col)].Icon);
    //             }
    //             else
    //             {
    //                 Console.Write(EmptySpaceIcon);
    //             }
    //             
    //             if (col < 7) Console.Write("\t");
    //             // skip to next line after reaching last item in row
    //             if (col == 7)
    //             {
    //                 // row number labels
    //                 Console.Write(@"  |" + "\t");
    //                 Console.Write(_rowNums[row]);
    //                 Console.WriteLine();
    //                 if (row < 7)
    //                 {
    //                     Console.Write(@"      |" + "\t\t\t\t\t\t\t\t" + @"   |");
    //                     Console.WriteLine();
    //                 }
    //                 else
    //                 {
    //                     Console.WriteLine(@"      ______________________________________________________________");
    //                 }
    //             }
    //         }
    //     }
    //
    //     // column letter labels
    //     Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH\n");
    //     // White side label
    //     Console.WriteLine("\t\t\t\t WHITE");
    //     Console.WriteLine($"Move Count: {inMoveCounter}");
    // }

    public static bool IsWithinBoard((int row, int col) position)
    {
        return position.row is <= 7 and >= 0 && position.col is <= 7 and >= 0;
    }

    // public void PlacePiece(Piece? inPiece, (int row, int col) position)
    // {
    //     if (!IsWithinBoard(position)) throw new Exception("PlacePiece() arguments are not within bounds of ChessBoard");
    //     BoardPieces[position.row, position.col] = inPiece;
    // }

    
    
    // should receive non-null starting position
    public bool ValidateAndMovePiece(PieceColor colorToMove, (int row, int col) startIndex, (int row, int col) destIndex, out List<int> errorCodes)
    {
        Piece? activePiece = GetPieceByPosition(startIndex);
        errorCodes = new List<int>();
        
        if (activePiece is null)
        {
            Console.WriteLine("activePiece was null");
            errorCodes.Add(1);
            return false;
        }

        if (!activePiece.IsColorToMove(colorToMove))
        {
            Console.WriteLine($"Invalid Color: Only {colorToMove} pieces can be moved.");
            errorCodes.Add(2);
            return false;
        }

        if (!activePiece.HasMove(destIndex))
        {
            Console.WriteLine($"Move entered ({ConvertIndexToPos(destIndex)}) was not found as a valid move for " +
                              $"{activePiece.Color} {activePiece.Type} at {ConvertIndexToPos(startIndex)} ");
            errorCodes.Add(3);
            return false;
        }
        
        Piece? takenPiece;
        MovePiece(startIndex, destIndex, out takenPiece);

        if (activePiece?.Type == PieceType.Pawn)
            CheckAndPromotePawn(activePiece, this, destIndex.row);

        ClearAllPositionThreats();
        GeneratePieceMoves();
        AddAllPositionThreats();

        // TODO king in check validation
        if (IsKingInCheck())
        {
            Console.WriteLine("Your king is in check!");
            return false;
        }

        if (errorCodes.Count > 0)
        {
            return false;
        }
        return true;
    }

    public void AddAllPositionThreats()
    {
        for (var row = 0; row < BoardSpaces.GetLength(0); row++)
        {
            for (var col = 0; col < BoardSpaces.GetLength(0); col++)
            {
                var piece = BoardSpaces[row, col];
                if (piece is null) continue;
                foreach (var move in piece.GetValidMoveList())
                    BoardSpaces[move.row, move.col]?.SetThreat();
            }
        }
    }

    public void ClearAllPositionThreats()
    {
        for (var row = 0; row < BoardSpaces.GetLength(0); row++)
        {
            for (var col = 0; col < BoardSpaces.GetLength(0); col++)
            {
                var piece = BoardSpaces[row, col];
                if (piece is null) continue;
                piece.ClearThreat();
            }
        }
    }

    private bool IsKingInCheck()
    {
        // get location of kings
        // how to get their location? each time a king moves, update a tracker
        // variable?
        foreach (var kvp in ThreatenedSpaces)
        {
            //if (kvp.Key == )
            foreach (var piece in kvp.Value)
            {
                
            }
        }
        return false;
    }
    
    public void PrintPiecesByPositions()
    {
        foreach (var piece in ActivePieces)
        {
            Console.WriteLine($"{piece.Type} {piece.Position}");
        }
    }

    public void PrintPieceLocationDictionary()
    {
        foreach (var kvpPiecePosition in PiecePositions)
        {
            Console.WriteLine($"{kvpPiecePosition.Key} {kvpPiecePosition.Value}");
        }
    }
    
    public void ExecuteCastleMove((int row, int col) startSpace, (int row, int col) destSpace)
    {
        if (!PiecePositions.TryGetValue(startSpace, out var king)) return;
       
        Piece? rook;
        if (destSpace.col > king.Position.col)
            rook = GetPieceByPosition(king.GetRookPos(PieceType.King));
        else if (destSpace.col < king.Position.col)
            rook = GetPieceByPosition(king.GetRookPos(PieceType.Queen));
        else
        {
            throw new Exception(
                "ExecuteCastleMoves() was passed an invalid value. Value of destSpace.col was " +
                "equal to king.Position.col");
        }
        if (rook is null) throw new Exception("ExecuteCastleMoves() rook is null");
        
        MovePieceNoTake(king.Position, destSpace);
        MovePieceNoTake(rook.Position, rook.GetCastlePos());
    }

    public void UpdateBoardAndPieces()
    {
        UpdatePiecePositions();
        PopulateBoard();
        ClearValidMoves();
        GeneratePieceMoves();
        // Add Threats
        // Calculate Pins
        
        // FOR TESTING
        //OutputBoard();
    }
    
    
    
    //    
    // -- Not used below --
    //
    
    public void PlacePieces()
    {
        PieceColor[] colors = {
            PieceColor.Black, 
            PieceColor.White
        };
        Type[] pieceOrder = { 
            typeof(Rook), 
            typeof(Knight), 
            typeof(Bishop), 
            typeof(Queen), 
            typeof(King), 
            typeof(Bishop), 
            typeof(Knight), 
            typeof(Rook) 
        };
    
        foreach (var color in colors)
        {
            int pawnRow = (color == PieceColor.Black) ? 1 : 6;
            int pieceRow = (color == PieceColor.Black) ? 0 : 7;
    
            // Place pawns
            for (int col = 0; col < 8; col++)
            {
                BoardSpaces[pawnRow, col] = new Pawn(color);
            }
    
            // Place other pieces based on the pieceOrder array
            for (int col = 0; col < 8; col++)
            {
                BoardSpaces[pieceRow, col] = (Piece?)Activator.CreateInstance(pieceOrder[col], color);
            }
        }
    }
    
    // not used switch method to convert H3 input to [2, 7] output
    public static (int row, int col) ConvertPosToIndices((char col, int row) position)
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
    
    public static string ConvertIndexToPos((int row, int col) inIndex)
    {
        var row = "";
        var column = "";
        row = inIndex.row switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => row
        };
        column = inIndex.Item2 switch
        {
            0 => "A",
            1 => "B",
            2 => "C",
            3 => "D",
            4 => "E",
            5 => "F",
            6 => "G",
            7 => "H",
            _ => column
        };
        return column + row;
    }

    public void VisualizePieceMoves(Piece inPiece)
    {
        var tempDict = new Dictionary<(int row, int col), char>();
        foreach (var validMove in inPiece.GetValidMoveList())
        {
            tempDict.Add(validMove, ThreatenedSpaceIcon);
        }
        OutputBoard(0, tempDict);
    }

    public Dictionary<(int row, int col), char> CreatePositionIconDict(List<(int row, int col)> inPositions, List<char> inIcons)
    {
        if (inPositions.Count != inIcons.Count)
        {
            throw new Exception("Arguments must be of equal length!");
        }
        Dictionary<(int row, int col), char> newDict = new();

        for (int i = 0; i < inPositions.Count; i++)
        {
            newDict.Add(inPositions[i], inIcons[i]);
        }

        return newDict;
    }
}