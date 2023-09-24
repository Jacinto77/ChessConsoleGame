using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static ChessMac.Program;
using static ChessMac.Space;
using static ChessMac.Methods;
namespace ChessMac;

/*
 * TODO: decompose functions further
 * 
 * TODO: create move generation functionality work to identify and tag pieces as being threatened, as well as what piece is threatening
 * TODO: implement pinned piece recognition, for when king would be threatened if a piece wasn't in the way
 * TODO: implement piece blocking checking for when King is in check
 * TODO: add secondary move generation method that scans all possible movements as if there were no other pieces, tags them and assigns a score based on how many pieces are in the way
 * TODO: add move counter
 */

public abstract class Piece
{
    public bool IsActive { get; set; }
    // human readable position of piece
    public string? Pos { get; set; }
    
    public int RowIndex { get; set; }
    public int ColIndex { get; set; }
    
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
    public string? Name { get; set; }
    public char? Icon { get; set; }
    
    public bool HasMoved { get; set; }
    public bool IsPinned { get; set; }
    
    public int MoveCounter = 0;
    // all valid moves for the piece
    public List<Tuple<int, int>> ValidMoves = new();
    
    public Piece(PieceColor color, string name, char icon)
    {
        Color = color;
        Name = name;
        Icon = icon;
        HasMoved = false;
        
    }

    public Piece(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
        SetupIconAndName(color, type);
    }

    public virtual Piece DeepCopy()
    {
        Piece pieceCopy;
        switch (Type)
        {
            case PieceType.Pawn: pieceCopy = new Pawn(this.Color, this.Type);
                break;
            case PieceType.Knight: pieceCopy = new Knight(this.Color, this.Type);
                break;
            case PieceType.Bishop: pieceCopy = new Bishop(this.Color, this.Type);
                break;
            case PieceType.Rook: pieceCopy = new Rook(this.Color, this.Type);
                break;
            case PieceType.Queen: pieceCopy = new Queen(this.Color, this.Type);
                break;
            case PieceType.King: pieceCopy = new King(this.Color, this.Type);
                break;
                
            default: throw new Exception("Piece.DeepCopy() switch error");
        }

        pieceCopy.IsActive = IsActive;
        
        pieceCopy.Type = Type;
        pieceCopy.Color = Color;
        pieceCopy.Name = Name;
        pieceCopy.Icon = Icon;
        
        pieceCopy.HasMoved = HasMoved;
        pieceCopy.IsPinned = IsPinned;
        
        pieceCopy.Pos = Pos;
        pieceCopy.RowIndex = RowIndex;
        pieceCopy.ColIndex = ColIndex; 
        
        foreach (var validMove in ValidMoves)
        {
            pieceCopy.ValidMoves.Add(new Tuple<int, int>(validMove.Item1, validMove.Item2));
        }
        
        return pieceCopy;
    }
    
    public enum PieceColor
    {
        White, 
        Black
    }

    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public static PieceType ConvertIntToPieceType(int? input)
    {
        PieceType pieceType = input switch
        {
            1 => PieceType.Knight,
            2 => PieceType.Bishop,
            3 => PieceType.Rook,
            4 => PieceType.Queen,
            _ => PieceType.Pawn
        };

        return pieceType;
    }
    
    private static int PieceCounter = 16;
    
    public static Piece CreatePiece(PieceColor color, PieceType type)
    {
        Piece? tempPiece;
        switch (type)
        {
            case PieceType.Pawn: tempPiece = new Pawn(color, type);
                break;
            case PieceType.Knight: tempPiece = new Knight(color, type);
                break;
            case PieceType.Bishop: tempPiece = new Bishop(color, type);
                break;
            case PieceType.Rook: tempPiece = new Rook(color, type);
                break;
            case PieceType.Queen: tempPiece = new Queen(color, type);
                break;
            case PieceType.King: tempPiece = new King(color, type);
                break;

            default:
            {
                Console.WriteLine("PieceGenError");
                throw new Exception("Piece generation error: CreatePiece()");
            }
        }
        PieceCounter -= 1;
        return tempPiece;
    }

    public static void ResetPieceCounter()
    {
        PieceCounter = 16;
    }
    
    public static readonly Dictionary<PieceType, char> BlackIcons = new()
    {
        { PieceType.Pawn, '\u2659' },
        { PieceType.Rook, '\u2656' },
        { PieceType.Knight, '\u2658' },
        { PieceType.Bishop,'\u2657' },
        { PieceType.Queen, '\u2655' },
        { PieceType.King, '\u2654' },
    };
    public static readonly Dictionary<PieceType, char> WhiteIcons = new()
    {
        { PieceType.Pawn, '\u265F' },
        { PieceType.Rook, '\u265C' },
        { PieceType.Knight, '\u265E' },
        { PieceType.Bishop, '\u265D' },
        { PieceType.Queen, '\u265B' },
        { PieceType.King, '\u265A' },
    };

    // generates all valid moves
    public abstract void GenerateValidMoves(ChessBoard inBoard);
    
    public void SetupIconAndName(PieceColor color, PieceType type)
    {
        SetIcon(color, type);
        SetName(color, type);
    }

    public void SetIcon(PieceColor inColor, PieceType inType)
    {
        Icon = inColor switch
        {
            PieceColor.White => WhiteIcons[inType],
            PieceColor.Black => BlackIcons[inType],
            _ => Icon
        };
    }

    public void SetPosition(Tuple<int, int> inPosition)
    {
        RowIndex = inPosition.Item1;
        ColIndex = inPosition.Item2;
    }
    
    public void SetName(PieceColor color, PieceType type)
    {
        Name = color.ToString() + type.ToString() + "_" + PieceCounter.ToString();
    }
    
    public void PrintValidMoves()
    {
        foreach (var position in ValidMoves)
        {
            Console.Write($"Row: {position.Item1} Col: {position.Item2}\n");
            Console.WriteLine(ConvertIndexToPos(position.Item1, position.Item2));
        }
    }
    
    public void GenerateRookMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();

        int currentCol = ColIndex;
        int currentRow = RowIndex;

        // Define the possible directions for movement as (rowChange, colChange)
        Tuple<int, int>[] directions = {
            new Tuple<int, int>(-1, 0),  // Up
            new Tuple<int, int>(1, 0),   // Down
            new Tuple<int, int>(0, -1),  // Left
            new Tuple<int, int>(0, 1)    // Right
        };

        foreach (var dir in directions)
        {
            int rowChange = dir.Item1;
            int colChange = dir.Item2;

            for (int i = 1; i < 8; i++) 
            {
                int newRow = currentRow + i * rowChange;
                int newCol = currentCol + i * colChange;

                Tuple<int, int> newLocation = new Tuple<int, int>(newRow, newCol);
                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) 
                    break;

                Space? tempSpace = inBoard.GetSpace(newLocation);

                if (tempSpace.HasPiece && tempSpace.Piece?.Color == Color)
                    break;
            
                ValidMoves.Add(new Tuple<int, int>(newRow, newCol));
            
                if (tempSpace.HasPiece)
                    break;
            }
        }
    }
    
    // //TODO: create extension methods for these for better encapsulation
    // public void GenerateRookMoves(ChessBoard inBoard)
    // {
    //     ValidMoves.Clear();
    //     // scan spaces between startSpace and possible destinations
    //     // walk spaces from start to edges of board
    //     // if space.HasPiece == True, stop
    //
    //     int currentCol = ColIndex;
    //     int currentRow = RowIndex;
    //
    //     // subtract current position from 7 (last index of board array) to find boundaries
    //     int rangeLeft = currentCol + 1;
    //     int rangeRight = 8 - currentCol;
    //     int rangeUp = currentRow + 1;
    //     int rangeDown = 8 - currentRow;
    //
    //     //scan up
    //     for (int i = 1; i < rangeUp; i++)
    //     {
    //         Space tempSpace = inBoard.GetSpace(currentRow - i, currentCol);                                          
    //         if (tempSpace.HasPiece && tempSpace.Piece?.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(new Tuple<int, int>(currentRow - i, currentCol));
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(new Tuple<int, int>(currentRow - i, currentCol));
    //     }
    //     
    //     //scan down
    //     for (int i = 1; i < rangeDown; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //         {  
    //             break;
    //         }
    //
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //     
    //     //scan left
    //     for (int i = 1; i < rangeLeft; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol - i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //     //scan right
    //     for (int i = 1; i < rangeRight; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol + i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //
    //     // int counter = 1;
    //     // foreach (var move in ValidMoves)
    //     // {
    //     //     Console.WriteLine(counter);
    //     //     Console.WriteLine(move.Col + move.Row.ToString());
    //     //     counter++;
    //     // }
    // }

    public void GenerateBishopMoves(ChessBoard inBoard)
    {
        int currentCol = ColIndex;
        int currentRow = RowIndex;

        // Define the possible diagonal directions for movement as (rowChange, colChange)
        Tuple<int, int>[] directions = {
            new Tuple<int, int>(-1, -1),  // Up-Left
            new Tuple<int, int>(-1, 1),   // Up-Right
            new Tuple<int, int>(1, 1),    // Down-Right
            new Tuple<int, int>(1, -1)    // Down-Left
        };

        foreach (var dir in directions)
        {
            int rowChange = dir.Item1;
            int colChange = dir.Item2;

            for (int i = 1; i < 8; i++)  // Max possible steps is 7 in any direction
            {
                int newRow = currentRow + i * rowChange;
                int newCol = currentCol + i * colChange;

                Tuple<int, int> newLocation = new Tuple<int, int>(newRow, newCol);
                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) // Check boundary conditions
                    break;

                Space? tempSpace = inBoard.GetSpace(newLocation);

                if (tempSpace.HasPiece && tempSpace.Piece?.Color == Color)
                    break;
            
                ValidMoves.Add(new Tuple<int, int>(newRow, newCol));
            
                if (tempSpace.HasPiece)
                    break;
            }
        }
    }
    
    // public void GenerateBishopMoves(ChessBoard inBoard)
    // {
    //     int currentCol = ColIndex;
    //     int currentRow = RowIndex;
    //
    //     int rangeUp = currentRow + 1;
    //     int rangeDown = 8 - currentRow;
    //     int rangeLeft = currentCol + 1;
    //     int rangeRight = 8 - currentCol;
    //
    //     // scan up-left
    //     for (int i = 1; i < rangeUp && i < rangeLeft; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol - i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //     
    //     // scan up-right
    //     for (int i = 1; i < rangeUp && i < rangeRight; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol + i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //     
    //     // scan down-right
    //     for (int i = 1; i < rangeDown && i < rangeRight; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol + i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    //     
    //     // scan down-left
    //     for (int i = 1; i < rangeDown && i < rangeLeft; i++)
    //     {
    //         Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol - i];
    //         if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
    //             break;
    //         if (tempSpace.HasPiece == true)
    //         {
    //             ValidMoves.Add(tempSpace);
    //             break;
    //         }
    //         if (tempSpace.HasPiece == false)
    //             ValidMoves.Add(tempSpace);
    //     }
    // }

    public void PlacePiece(ChessBoard inBoard, Tuple<int, int> destLocation)
    {
        SetPosition(destLocation);
        inBoard.GetSpace(destLocation).SetPieceInfo(this);
        IsActive = true;
    }

    public void MovePiece(ChessBoard inBoard, Tuple<int, int> inMove)
    {
        SetPosition(inMove);
        inBoard.GetSpace(inMove).SetPieceInfo(this);
        HasMoved = true;
    }
    
    public bool IsMoveValid(Tuple<int, int> inMove)
    {
        foreach (var validSpace in ValidMoves)
        {
            if (validSpace.Equals(inMove))
                return true;
        }

        return false;
    }


    public void PromotePawn(ChessBoard inBoard)
    {
        PromptForPromotion();
        var pieceType = ConvertIntToPieceType(GetPlayerTypeInt());
        CreatePromotionPiece(this, pieceType, inBoard);
    }
    
    private void CreatePromotionPiece(Piece? inPiece, PieceType inPieceType, ChessBoard inBoard)
    {
        inPiece?.SetType(inPieceType);
    }

    public bool IsPawnPromotionSpace()
    {
        return RowIndex is 7 or 0;
    }

    private void PromptForPromotion()
    {
        Console.WriteLine("Pawn is promoted! Choose a piece to promote to:");
        Console.WriteLine("1)\tKnight");
        Console.WriteLine("2)\tBishop");
        Console.WriteLine("3)\tRook");
        Console.WriteLine("4)\tQueen");
        Console.Write(">");
    }

    private void SetType(PieceType inPieceType)
    {
        this.Type = inPieceType;
        this.Icon = GetIcon(inPieceType);
    }

    private char GetIcon(PieceType inPieceType)
    {
        var tempDict = Color == PieceColor.White ? WhiteIcons : BlackIcons;
        
        var tempIcon = inPieceType switch
        {
            PieceType.Pawn => tempDict[inPieceType],
            PieceType.Knight => tempDict[inPieceType],
            PieceType.Bishop => tempDict[inPieceType],
            PieceType.Rook => tempDict[inPieceType],
            PieceType.Queen => tempDict[inPieceType],
            PieceType.King => tempDict[inPieceType],
            _ => 'x'
        };

        return tempIcon;
    }
    
    // public void PrintThreats()
    // {
    //     //
    // }
    //
    // public void AddPieceTake(Piece? inPiece)
    // {
    //     if (inPiece is not null)
    //         PieceTakes.Add(inPiece);
    // }
    //
    // public void PrintPieceTakes()
    // {
    //     foreach (var take in PieceTakes)
    //     {
    //         
    //     }
    // }

    // public void ScanAllDirections()
    // {
    //     
    //     //for (int i = 1; i < )
    // }

    public Piece? ScanSpacesHorizVert(int inRowOffset, int inColOffset, int range, 
        PieceType threatType1, PieceType threatType2, ChessBoard inBoard)
    {
        Piece? pieceToReturn = null;
        
        int currentCol = ColIndex;
        int currentRow = RowIndex;
        int timeToLive = 2;
        
        for (int i = 1; i < range; i++)
        {
            int tempRow = currentRow + (i * inRowOffset); 
            int tempCol = currentCol + (i * inColOffset);
            Tuple<int, int> tempLocation = new Tuple<int, int>(tempRow, tempCol);
            Space? tempSpace = inBoard.GetSpace(tempLocation);

            if (!tempSpace.HasPiece) continue;
            
            timeToLive--;
            if (timeToLive == 0 && (tempSpace.Piece!.Type == threatType1
                                    || tempSpace.Piece.Type == threatType2))
                return pieceToReturn;
            pieceToReturn = tempSpace.Piece;
        }
        return null;
    }
    
    public Piece? ScanSpacesDiagonal(int inRowOffset, int inColOffset, int range1, int range2, 
        PieceType threatType1, PieceType threatType2, ChessBoard inBoard)
    {
        Piece? pieceToReturn = null;

        int currentCol = ColIndex;
        int currentRow = RowIndex;
        int timeToLive = 2;
        for (int i = 1; i < range1 && i < range2; i++)
        {
            int tempRow = currentRow + (i * inRowOffset); 
            int tempCol = currentCol + (i * inColOffset);
            Tuple<int, int> tempLocation = new Tuple<int, int>(tempRow, tempCol);
            Space? tempSpace = inBoard.GetSpace(tempLocation);

            if (!tempSpace.HasPiece) continue;
            
            timeToLive--;
            if (timeToLive == 0 && (tempSpace.Piece!.Type == threatType1
                                    || tempSpace.Piece.Type == threatType2))
                return pieceToReturn;
            pieceToReturn = tempSpace.Piece;
        }
        return null;
    }
    
    public void ScanForPinnedPiece(ChessBoard inBoard)
    {
        int rangeUp = RowIndex + 1;
        int rangeDown = 8 - RowIndex;
        int rangeNeg = ColIndex + 1;
        int rangePos = 8 - ColIndex;

        List<Piece?> pinnedPieces = new List<Piece?>();
        // scan horiz and vert
        pinnedPieces.Add(ScanSpacesHorizVert(-1, 0, rangeUp,
            PieceType.Queen, PieceType.Rook, inBoard));
        pinnedPieces.Add(ScanSpacesHorizVert(1, 0, rangeDown,
            PieceType.Queen, PieceType.Rook, inBoard));
        pinnedPieces.Add(ScanSpacesHorizVert(0, 1, rangePos,
            PieceType.Queen, PieceType.Rook, inBoard));
        pinnedPieces.Add(ScanSpacesHorizVert(0, -1, rangeNeg,
            PieceType.Queen, PieceType.Rook, inBoard));
        
        // scan diagonally
        pinnedPieces.Add(ScanSpacesDiagonal(-1, 1, rangeUp, rangePos,
            PieceType.Queen, PieceType.Bishop, inBoard));
        pinnedPieces.Add(ScanSpacesDiagonal(1, 1, rangeDown, rangePos,
            PieceType.Queen, PieceType.Bishop, inBoard));
        pinnedPieces.Add(ScanSpacesDiagonal(-1, -1, rangeUp, rangeNeg,
            PieceType.Queen, PieceType.Bishop, inBoard));
        pinnedPieces.Add(ScanSpacesDiagonal(1, -1, rangeDown, rangeNeg,
            PieceType.Queen, PieceType.Bishop, inBoard));

        // foreach (var piece in pinnedPieces)
        // {
        //     if (piece is null)
        //         continue;
        //     PinnedPieces.Add(piece);
        //     piece.IsPinned = true;
        // }
    }
}