using System.Data;
using ChessMac.Board;
using static ChessMac.Board.Methods;
using static ChessMac.Board.ChessBoard;

namespace ChessMac.Pieces.Base;

/*
 * TODO: add debugging functions
 * TODO: remove input validation checking from each piece, should be done once in the beginning and every other function will trust that the indices provided are valid
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

public abstract class Piece
{
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
    
    // protected Piece(PieceColor inColor)
    // {
    //     Color = inColor;
    //     Position = new();
    //     
    //     HasMoved = false;
    //     IsPinned = false;
    //     MoveCounter = 0;
    //     IsThreatened = false;
    //     
    // }
    //
    // protected Piece(PieceColor inColor, (int row, int col) inPosition)
    // {
    //     Color = inColor;
    //     Position = inPosition;
    //     
    //     HasMoved = false;
    //     IsPinned = false;
    //     MoveCounter = 0;
    //     IsThreatened = false;
    // }
    //
    // public Piece(PieceColor inColor, PieceType inType)
    // {
    //     Color = inColor;
    //     Type = inType;
    //     Position = new ValueTuple<int, int>();
    //     
    //     HasMoved = false;
    //     IsPinned = false;
    //     MoveCounter = 0;
    //     IsThreatened = false;
    //     
    // }

    // public Piece()
    // {
    //     Type = PieceType.Null;
    //     Color = PieceColor.Null;
    //     Icon = EmptySpaceIcon;
    //     HasMoved = false;
    //     IsPinned = false;
    //     MoveCounter = 0;
    // }
    
    public Piece(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = default,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = false,
        bool inIsPinned = false,
        int inMoveCounter = 0,
        bool inIsThreatened = false)
    {
        Type = inType;
        Color = inColor;
        SetValidMoveList(inValidMoves);
        Icon = inIcon;
        if (Icon == null) SetIconByColorAndType(inColor);
        HasMoved = inHasMoved;
        IsPinned = inIsPinned;
        MoveCounter = inMoveCounter;
        IsThreatened = inIsThreatened;

        Position = inPosition;
    }

    public abstract Piece Clone();
    
    public static readonly Dictionary<PieceType, char> BlackIcons = new()
    {
        { PieceType.Pawn, '\u2659' },
        { PieceType.Rook, '\u2656' },
        { PieceType.Knight, '\u2658' },
        { PieceType.Bishop, '\u2657' },
        { PieceType.Queen, '\u2655' },
        { PieceType.King, '\u2654' }
    };

    public static readonly Dictionary<PieceType, char> WhiteIcons = new()
    {
        { PieceType.Pawn, '\u265F' },
        { PieceType.Rook, '\u265C' },
        { PieceType.Knight, '\u265E' },
        { PieceType.Bishop, '\u265D' },
        { PieceType.Queen, '\u265B' },
        { PieceType.King, '\u265A' }
    };

    public (int row, int col) Position { get; protected set; }
    
    public const char EmptySpaceIcon = '\u2610';
    public static char ThreatenedSpaceIcon = '\u2606';

    private List<(int row, int col)> _validMoves = new();
    
    public PieceType Type { get; protected init; }
    public PieceColor Color { get; protected set; }
    public char? Icon { get; protected set; }

    public bool HasMoved { get; protected set; }
    public bool IsPinned { get; protected set; }
    public int MoveCounter { get; protected set; }
    public bool IsThreatened { get; protected set; }

    public virtual void Move((int row, int col) inPosition)
    {
        SetPosition(inPosition);
        IncrementMoveCounter();
        HasMoved = true;
    }

    public void SetPosition((int row, int col) inPosition)
    {
        Position = inPosition;
    }

    public virtual (int row, int col) GetCastlePos()
    {
        return (-1, -1);
    }
    
    
    public void SetHasMoved()
    {
        if (MoveCounter > 0)
            HasMoved = true;
        HasMoved = true;
    }

    public void SetHasMoved(bool hasMoved)
    {
        HasMoved = hasMoved;
    }

    public void SetIsPinned()
    {
        IsPinned = true;
    }
    
    public void SetIsPinned(bool isPinned)
    {
        IsPinned = isPinned;
    }

    public void ClearIsPinned()
    {
        IsPinned = false;
    }
    
    public void IncrementMoveCounter()
    {
        MoveCounter++;
    }

    public void SetMoveCounter(int inCount)
    {
        MoveCounter = inCount;
    }
    
    // protected void AssignIconByColor(PieceColor inColor, PieceType inType)
    // {
    //     Dictionary<PieceType, char> iconDict;
    //     iconDict = inColor is PieceColor.White ? WhiteIcons : BlackIcons;
    //
    //     Icon = iconDict[inType];
    // }   
    
    public void SetThreat()
    {
        IsThreatened = true;
    }

    public void SetThreat(bool isThreatened)
    {
        IsThreatened = isThreatened;
    }
    
    public void ClearThreat()
    {
        IsThreatened = false;
    }

    public void SetValidMoveList(List<(int row, int col)>? inValidMoves)
    {
        _validMoves.Clear();
        if (inValidMoves is null)
        {
            // Console.Error.WriteLine("SetValidMoveList() argument was null, valid moves list cleared.");
            return;
        }
        foreach (var move in inValidMoves) _validMoves.Add(move);
    }
    
    public List<(int row, int col)> GetValidMoveList()
    {
        return _validMoves;
    }

    public void PrintValidMoveList()
    {
        Console.WriteLine($"{Type} at {ConvertIndexToPos(Position)} valid moves:");
        if (_validMoves.Count == 0)
        {
            Console.WriteLine("\t--No moves in valid moves list--");
            return;
        }

        var counter = 1;
        foreach (var move in _validMoves)
        {
            Console.WriteLine($"\t\t{counter}- {ConvertIndexToPos(move)}");
            counter++;
        }
        Console.WriteLine("");
    }

    public static PieceType ConvertIntToPieceType(int? input)
    {
        var pieceType = input switch
        {
            1 => PieceType.Knight,
            2 => PieceType.Bishop,
            3 => PieceType.Rook,
            4 => PieceType.Queen,
            _ => PieceType.Pawn
        };
        return pieceType;
    }

    public void SetIconByColorAndType(PieceColor inColor)
    {
        Icon = inColor == PieceColor.Black ? BlackIcons[Type] : WhiteIcons[Type];
    }

    protected void AddValidMove((int row, int col) validMove)
    {
        _validMoves.Add(validMove);
    }

    public void ClearValidMoves()
    {
        _validMoves.Clear();
    }

    // generates all valid moves
    public virtual void GenerateValidMoves(ChessBoard inBoard)
    {
    }

    public void GenerateRookMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        //ClearValidMoves();

        // Define the possible directions for movement as (rowChange, colChange)
        Tuple<int, int>[] directions =
        {
            new(-1, 0), // Up
            new(1, 0), // Down
            new(0, -1), // Left
            new(0, 1) // Right
        };

        foreach (var (rowChange, colChange) in directions)
            for (var i = 1; i < 8; i++)
            {
                var newRow = currentRow + i * rowChange;
                var newCol = currentCol + i * colChange;

                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8)
                    break;

                var tempPiece = inBoard.BoardSpaces[newRow, newCol];

                if (tempPiece is not null && tempPiece.Color == Color)
                    break;

                AddValidMove(new ValueTuple<int, int>(newRow, newCol));

                if (tempPiece is not null)
                    break;
            }
    }

    public void GenerateBishopMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        // Define the possible diagonal directions for movement as (rowChange, colChange)
        Tuple<int, int>[] directions =
        {
            new(-1, -1), // Up-Left
            new(-1, 1), // Up-Right
            new(1, 1), // Down-Right
            new(1, -1) // Down-Left
        };

        foreach (var dir in directions)
        {
            var rowChange = dir.Item1;
            var colChange = dir.Item2;

            for (var i = 1; i < 8; i++) // Max possible steps is 7 in any direction
            {
                var newRow = currentRow + i * rowChange;
                var newCol = currentCol + i * colChange;

                if (newRow < 0 || newRow >= 8 || newCol < 0 || newCol >= 8) // Check boundary conditions
                    break;

                var tempSpace = inBoard.BoardSpaces[newRow, newCol];

                if (tempSpace is not null && tempSpace.Color == Color)
                    break;

                AddValidMove(new ValueTuple<int, int>(newRow, newCol));

                if (tempSpace is not null)
                    break;
            }
        }
    }

    public bool HasMove((int row, int col) inMove)
    {
        return _validMoves.Contains(inMove);
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
        Console.Write("> ");
    }

    public bool IsColorToMove(PieceColor colorToMove)
    {
        if (Color == colorToMove) return true;
        //Console.WriteLine($"Invalid Color: Only {colorToMove} pieces can be moved.");
        return false;
    }
    
    public virtual void PrintAttributes()
    {
        Console.WriteLine($"Type:           {Type}");
        Console.WriteLine($"Color:          {Color}");
        Console.WriteLine($"Position:       {Position}");
        Console.WriteLine($"Move Counter:   {MoveCounter}");
        Console.WriteLine($"Is Threatened:  {IsThreatened}");
        Console.WriteLine($"Has Moved:      {HasMoved}");
        Console.WriteLine($"Icon:           {Icon}");
        PrintValidMoveList();
    }

    public virtual (int row, int col) GetRookPos(PieceType inType)
    {
        return (-1, -1);
    }
}