using System.Runtime.CompilerServices;
using static ChessMac.ChessBoard;
using static ChessMac.Methods;
namespace ChessMac;

/*
 * TODO: add debugging functions
 *  allow to query possible moves when prompted to move
 *  show threatened pieces
 *  show threatened squares
 * 
 * TODO: decompose functions further
 * 
 * TODO: create move generation functionality work to identify and tag pieces as being threatened, as well as what piece is threatening
 * TODO: implement pinned piece recognition, for when king would be threatened if a piece wasn't in the way
 * TODO: implement piece blocking checking for when King is in check
 * TODO: add secondary move generation method that scans all possible movements as if there were no other pieces, tags them and assigns a score based on how many pieces are in the way
 * TODO: add move counter
 */

public class Piece
{
    public PieceType Type { get; protected init; }
    public PieceColor Color { get; set; }
    public char? Icon { get; set; }

    public bool HasMoved { get; set; } = false;
    public bool IsPinned { get; set; } = false;
    public int MoveCounter { get; set; } = 0;
    public bool IsThreatened { get; set; } = false;
    private List<(int row, int col)> validMoves = new();
  
    public readonly char EmptySpaceIcon = '\u2610';
    
    protected Piece(PieceColor inColor)
    {
        Color = inColor;
    }

    public Piece(PieceColor inColor, PieceType inType)
    {
        Color = inColor;
        Type = inType;
    }

    public Piece()
    {
        Icon = EmptySpaceIcon;
    }

    public void SetThreat()
    {
        IsThreatened = true;
    }

    public void ClearThreat()
    {
        IsThreatened = false;
    }

    public List<(int row, int col)> GetValidMoveList()
    {
        return validMoves;
    }

    public static Piece CreatePiece(PieceColor inColor, PieceType inType)
    {
        Piece? tempPiece;
        switch (inType)
        {
            case PieceType.Pawn: tempPiece = new Pawn(inColor);
                break;
            case PieceType.Knight: tempPiece = new Knight(inColor);
                break;
            case PieceType.Bishop: tempPiece = new Bishop(inColor);
                break;
            case PieceType.Rook: tempPiece = new Rook(inColor);
                break;
            case PieceType.Queen: tempPiece = new Queen(inColor);
                break;
            case PieceType.King: tempPiece = new King(inColor);
                break;
            default:
            {
                Console.WriteLine("PieceGenError");
                throw new Exception("Piece generation error: CreatePiece()");
            }
        }
        return tempPiece;
    }
    
    protected static List<T> CreateList<T>(params T[] values)
    {
        return new List<T>(values);
    }

    public virtual Piece DeepCopy()
    {
        Piece pieceCopy = CreatePiece(this.Color, this.Type);
        pieceCopy.HasMoved = HasMoved;
        pieceCopy.IsPinned = IsPinned;
        pieceCopy.MoveCounter = MoveCounter;
        pieceCopy.validMoves = new List<(int row, int col)>();

        foreach (var move in validMoves)
        {
            pieceCopy.AddValidMove(move);
        }
        return pieceCopy;
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

    public char GetColorPieceIcon(PieceColor inColor)
    {
        if (inColor == PieceColor.Black)
        {
            return BlackIcons[this.Type];
        }
        else
        {
            return WhiteIcons[this.Type];
        }
    }

    protected void AddValidMove((int row, int col) validMove)
    {
        validMoves.Add(validMove);
    }

    public void ClearValidMoves()
    {
        validMoves.Clear();
    }

    public void PrintValidMoves()
    {
        int counter = 1;
        Console.Write("Valid Moves are:\n");
        foreach ((int row, int col) move in validMoves)
        {
            string validMove = ConvertIndexToPos(new Tuple<int, int>(move.row, move.col));

            Console.WriteLine($"{counter}: {validMove}");
            counter++;
        }
    }
    
    // generates all valid moves
    public virtual void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
    }
    
    public void GenerateRookMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        //ClearValidMoves();

        // Define the possible directions for movement as (rowChange, colChange)
        Tuple<int, int>[] directions = {
            new Tuple<int, int>(-1, 0),  // Up
            new Tuple<int, int>(1, 0),   // Down
            new Tuple<int, int>(0, -1),  // Left
            new Tuple<int, int>(0, 1)    // Right
        };
    
        foreach (var (rowChange, colChange) in directions)
        {
            for (int i = 1; i < 8; i++) 
            {
                int newRow = currentRow + i * rowChange;
                int newCol = currentCol + i * colChange;
    
                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) 
                    break;
    
                Piece? tempPiece = inBoard.BoardPieces[newRow, newCol];
    
                if (tempPiece.Icon != EmptySpaceIcon && tempPiece.Color == Color)
                    break;
            
                AddValidMove(new ValueTuple<int, int>(newRow, newCol));
            
                if (tempPiece.Icon != EmptySpaceIcon)
                    break;
            }
        }
    }
    
    public void GenerateBishopMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
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
                
                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) // Check boundary conditions
                    break;
    
                Piece? tempSpace = inBoard.BoardPieces[newRow, newCol];
    
                if (tempSpace?.Icon != EmptySpaceIcon && tempSpace?.Color == Color)
                    break;
            
                AddValidMove(new ValueTuple<int, int>(newRow, newCol));
            
                if (tempSpace?.Icon != EmptySpaceIcon)
                    break;
            }
        }
    }

    public bool IsMoveValid((int row, int col) inMove)
    {
        foreach (var validSpace in validMoves)
        {
            if (validSpace.row == inMove.row && validSpace.col == inMove.col)
                return true;
        }

        return false;
    }
    
    public void PromotePawn(ChessBoard inBoard)
    {
        PromptForPromotion();
        var pieceType = ConvertIntToPieceType(GetPlayerTypeInt());
        // CreatePromotionPiece(this, pieceType, inBoard);
    }

    public bool IsPawnPromotionSpace(int inRowIndex)
    {
        return inRowIndex is 7 or 0;
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

    // public Piece? ScanSpacesHorizVert(int inRowOffset, int inColOffset, int range, 
    //     PieceType threatType1, PieceType threatType2, ChessBoard inBoard)
    // {
    //     Piece? pieceToReturn = null;
    //     
    //     int currentCol = ColIndex;
    //     int currentRow = RowIndex;
    //     int timeToLive = 2;
    //     
    //     for (int i = 1; i < range; i++)
    //     {
    //         int tempRow = currentRow + (i * inRowOffset); 
    //         int tempCol = currentCol + (i * inColOffset);
    //         Tuple<int, int> tempLocation = new Tuple<int, int>(tempRow, tempCol);
    //         Space? tempSpace = inBoard.GetSpace(tempLocation);
    //
    //         if (!tempSpace.HasPiece) continue;
    //         
    //         timeToLive--;
    //         if (timeToLive == 0 && (tempSpace.Piece!.Type == threatType1
    //                                 || tempSpace.Piece.Type == threatType2))
    //             return pieceToReturn;
    //         pieceToReturn = tempSpace.Piece;
    //     }
    //     return null;
    // }
    //
    // public Piece? ScanSpacesDiagonal(int inRowOffset, int inColOffset, int range1, int range2, 
    //     PieceType threatType1, PieceType threatType2, ChessBoard inBoard)
    // {
    //     Piece? pieceToReturn = null;
    //
    //     int currentCol = ColIndex;
    //     int currentRow = RowIndex;
    //     int timeToLive = 2;
    //     for (int i = 1; i < range1 && i < range2; i++)
    //     {
    //         int tempRow = currentRow + (i * inRowOffset); 
    //         int tempCol = currentCol + (i * inColOffset);
    //         Tuple<int, int> tempLocation = new Tuple<int, int>(tempRow, tempCol);
    //         Space? tempSpace = inBoard.GetSpace(tempLocation);
    //
    //         if (!tempSpace.HasPiece) continue;
    //         
    //         timeToLive--;
    //         if (timeToLive == 0 && (tempSpace.Piece!.Type == threatType1
    //                                 || tempSpace.Piece.Type == threatType2))
    //             return pieceToReturn;
    //         pieceToReturn = tempSpace.Piece;
    //     }
    //     return null;
    // }
    //
    // public void ScanForPinnedPiece(ChessBoard inBoard)
    // {
    //     int rangeUp = RowIndex + 1;
    //     int rangeDown = 8 - RowIndex;
    //     int rangeNeg = ColIndex + 1;
    //     int rangePos = 8 - ColIndex;
    //
    //     List<Piece?> pinnedPieces = new List<Piece?>();
    //     // scan horiz and vert
    //     pinnedPieces.Add(ScanSpacesHorizVert(-1, 0, rangeUp,
    //         PieceType.Queen, PieceType.Rook, inBoard));
    //     pinnedPieces.Add(ScanSpacesHorizVert(1, 0, rangeDown,
    //         PieceType.Queen, PieceType.Rook, inBoard));
    //     pinnedPieces.Add(ScanSpacesHorizVert(0, 1, rangePos,
    //         PieceType.Queen, PieceType.Rook, inBoard));
    //     pinnedPieces.Add(ScanSpacesHorizVert(0, -1, rangeNeg,
    //         PieceType.Queen, PieceType.Rook, inBoard));
    //     
    //     // scan diagonally
    //     pinnedPieces.Add(ScanSpacesDiagonal(-1, 1, rangeUp, rangePos,
    //         PieceType.Queen, PieceType.Bishop, inBoard));
    //     pinnedPieces.Add(ScanSpacesDiagonal(1, 1, rangeDown, rangePos,
    //         PieceType.Queen, PieceType.Bishop, inBoard));
    //     pinnedPieces.Add(ScanSpacesDiagonal(-1, -1, rangeUp, rangeNeg,
    //         PieceType.Queen, PieceType.Bishop, inBoard));
    //     pinnedPieces.Add(ScanSpacesDiagonal(1, -1, rangeDown, rangeNeg,
    //         PieceType.Queen, PieceType.Bishop, inBoard));
}
