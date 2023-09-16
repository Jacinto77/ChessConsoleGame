using static ChessMac.Program;
using static ChessMac.Space;
using static ChessMac.Methods;
namespace ChessMac;

/*
 * TODO: implement enum for piece colors
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
        SetupPiece(color, type);
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
  
    public Space.Position PiecePosition = new();

    public Space CurrentSpace;
    // human readable position of piece
    public string? Pos { get; set; }
    
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
    public string? Name { get; set; }
    public char? Icon { get; set; }
    
    public bool HasMoved { get; set; }

    public int MoveCounter = 0;
    // all valid moves for the piece
    public List<Space> ValidMoves = new();
    
    // generates all valid moves
    public abstract void GenerateValidMoves(ChessBoard inBoard);
    

        public void SetupPiece(PieceColor color, PieceType type)
    {
        switch (color)
        {
            case PieceColor.White: Icon = WhiteIcons[type];
                break;
            case PieceColor.Black: Icon = BlackIcons[type];
                break;
        }
        SetName(color, type);
    }

    public void SetName(PieceColor color, PieceType type)
    {
        Name = color.ToString() + type.ToString() + "_" + PieceCounter.ToString();
    }
    
    public void PrintValidMoves()
    {
        foreach (Space position in ValidMoves)
        {
            Console.Write($"{position.Col}{position.Row}\n");
        }
    }

    public void PrintValidMovesIndex()
    {
        foreach (var move in ValidMoves)
        {
            Console.WriteLine($"{move.RowIndex} {move.ColIndex}");
        }
    }
    
    public void SetPosition(Position inPosition)
    {
        PiecePosition.RowIndex = inPosition.RowIndex;
        PiecePosition.ColIndex = inPosition.ColIndex;
        
        Pos = ConvertIndexToPos(inPosition.RowIndex, inPosition.ColIndex);
    }

    public Position GetPosition()
    {
        return new Position()
        {
            RowIndex = PiecePosition.RowIndex,
            ColIndex = PiecePosition.ColIndex
        };
    }

    public void GenerateRookMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        // scan spaces between startSpace and possible destinations
        // walk spaces from start to edges of board
        // if space.HasPiece == True, stop

        int currentCol = GetPosition().ColIndex;
        int currentRow = GetPosition().RowIndex;

        // subtract current position from 7 (last index of board array) to find boundaries
        int rangeLeft = currentCol + 1;
        int rangeRight = 8 - currentCol;
        int rangeUp = currentRow + 1;
        int rangeDown = 8 - currentRow;

        //scan up
        for (int i = 1; i < rangeUp; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol];                                          
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan down
        for (int i = 1; i < rangeDown; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
            {  
                break;
            }

            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        //scan left
        for (int i = 1; i < rangeLeft; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        //scan right
        for (int i = 1; i < rangeRight; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }

        // int counter = 1;
        // foreach (var move in ValidMoves)
        // {
        //     Console.WriteLine(counter);
        //     Console.WriteLine(move.Col + move.Row.ToString());
        //     counter++;
        // }
    }

    public void GenerateBishopMoves(ChessBoard inBoard)
    {
        int currentCol = GetPosition().ColIndex;
        int currentRow = GetPosition().RowIndex;

        int rangeUp = currentRow + 1;
        int rangeDown = 8 - currentRow;
        int rangeLeft = currentCol + 1;
        int rangeRight = 8 - currentCol;

        // scan up-left
        for (int i = 1; i < rangeUp && i < rangeLeft; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan up-right
        for (int i = 1; i < rangeUp && i < rangeRight; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-right
        for (int i = 1; i < rangeDown && i < rangeRight; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-left
        for (int i = 1; i < rangeDown && i < rangeLeft; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
    }

    public void PlacePiece(Space inSpace, ChessBoard inBoard)
    {
        inSpace.SetPieceInfo(this);
        CurrentSpace = inSpace;
        SetPosition(inSpace.Pos);
    }

    // prob won't use this
    public void PlacePiece(int inRow, int inCol, ChessBoard inBoard)
    {
        Position tempPos = new Position(inRow, inCol);
        if (IsWithinBoard(tempPos) == false)
            return;
        else
        {
            Space tempSpace = inBoard.GetSpace(inRow, inCol);
            tempSpace.Piece = this;
            tempSpace.HasPiece = true;
            tempSpace.Icon = this.Icon;
            this.SetPosition(tempSpace.Pos);
        }
    }

    public bool IsMoveValid(Space destSpace)
    {
        foreach (var space in ValidMoves)
        {
            if (destSpace == space)
                return true;
        }

        return false;
    }
    
    public void MovePiece(Space destSpace, ChessBoard inBoard)
    {
        Space startSpace = this.CurrentSpace;
        PlacePiece(destSpace, inBoard);
        startSpace.ClearPieceInfo();
    }

    //
    public void AddPieceToSpaceThreats()
    {
        foreach (var space in ValidMoves)
        {
            space.AddPieceToThreats(this);
        }
    }

    public void PrintThreats()
    {
        
    }
    
}