using static ChessMac.Program;
using static ChessMac.Space;
using static ChessMac.Methods;
namespace ChessMac;

/*
 * TODO: create move generation functionality work to identify and tag pieces as being threatened, as well as what piece is threatening
 * TODO: implement pinned piece recognition, for when king would be threatened if a piece wasn't in the way
 * TODO: implement piece blocking checking for when King is in check
 * TODO: add secondary move generation method that scans all possible movements as if there were no other pieces, tags them and assigns a score based on how many pieces are in the way
 * TODO: add move counter
 */

public class Piece
{
    public Piece(string color, string name, char icon)
    {
        Color = color;
        Name = name;
        Icon = icon;
        HasMoved = false;
        
    }

    public Piece(string color, string type)
    {
        Color = color;
        Type = type;
        SetupPiece(color, type);
    }

    public static Piece? CreatePiece(string color, string type)
    {
        switch (type)
        {
            case "pawn": return new Pawn(color, type);
            case "knight": return new Knight(color, type);
            case "bishop": return new Bishop(color, type);
            case "rook": return new Rook(color, type);
            case "queen": return new Queen(color, type);
            case "king": return new King(color, type);
            
            default: return new Piece(color, type);
        }
    }
    
    public static readonly Dictionary<string, char> BlackIcons = new()
    {
        { "pawn", '\u2659' },
        { "rook", '\u2656' },
        { "knight", '\u2658' },
        { "bishop",'\u2657' },
        { "queen", '\u2655' },
        { "king", '\u2654' },
    };
    public static readonly Dictionary<string, char> WhiteIcons = new()
    {
        { "pawn", '\u265F' },
        { "rook", '\u265C' },
        { "knight", '\u265E' },
        { "bishop", '\u265D' },
        { "queen", '\u265B' },
        { "king", '\u265A' },
    };
  
    public Space.Position PiecePosition = new();
    // human readable position of piece
    public string? Pos { get; set; }
    
    public string? Type { get; set; }
    public string? Color { get; set; }
    public string? Name { get; set; }
    public char? Icon { get; set; }
    
    public bool HasMoved { get; set; }

    // all valid moves for the piece
    public List<Space> ValidMoves = new();
    
    // generates all valid moves
    public virtual void GenerateValidMoves(ChessBoard inBoard)
    {
    }

    public void SetupPiece(string color, string type)
    {
        switch (color)
        {
            case "white": Icon = WhiteIcons[type];
                break;
            case "black": Icon = BlackIcons[type];
                break;
        }
        SetName(color, type);
    }

    public void SetName(string color, string type)
    {
        Name = color + type;
    }
    
    public void PrintValidMoves()
    {
        foreach (Space position in ValidMoves)
        {
            Console.Write($"{position.Col}{position.Row}\n");
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
                break;
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

        int counter = 1;
        foreach (var move in ValidMoves)
        {
            Console.WriteLine(counter);
            Console.WriteLine(move.Col + move.Row.ToString());
            counter++;
        }
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

    public void PlacePiece(Space inSpace)
    {
        inSpace.Piece = this;
        inSpace.HasPiece = true;
        inSpace.Icon = this.Icon;
        this.SetPosition(inSpace.Pos);
    }

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

    public void MovePiece(Space destSpace)
    {
        
    }
    
    public void ScanHorizVert(Space currentSpace, int boundary)
    {
        
    }

    public void ScanDiagonal(Space currentSpace)
    {
        
    }
}