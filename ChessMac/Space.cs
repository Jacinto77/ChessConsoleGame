namespace ChessMac;

// Each space on the chessboard
// 64 instances of this class will be created and stored in array within ChessBoard
    // instance
// holds own data regarding which space it is, whether is is currently occupied, 
    // and a reference to the piece currently on it, if any

public class Space
{
    public Space(int inRowIndex, int inColIndex)
    {
        RowIndex = inRowIndex;
        ColIndex = inColIndex;

        Pos.RowIndex = inRowIndex;
        Pos.ColIndex = inColIndex;
        
        ConvertIndexToReadable(Pos);
        SetName();
        HasPiece = false;
        Piece = null;
        Icon = IconDefault;
    }

    public Space()
    {
    }

    public Space? DeepCopy()
    {
        Space newSpace = (Space)this.MemberwiseClone();
        newSpace.Piece = this.Piece?.DeepCopy();
        return newSpace;
    }
    
    public struct Position
    {
        public int RowIndex;
        public int ColIndex;
        
        public Position(int inRowIndex, int inColIndex)
        {
            RowIndex = inRowIndex;
            ColIndex = inColIndex;
        }
    }
    
    private const char EmptySpaceIcon = '\u2610';
    private const char HighlightedIcon = '\u25C9';
    
    // indices
    public readonly Position Pos = new();
    public readonly int RowIndex;
    public readonly int ColIndex;

    // readable
    public int Row { get; set; }
    public char Col { get; set; }
    
    
    public string? Name { get; set; } // ie A3, G7, etc
    public Piece? Piece { get; set; }
    public bool HasPiece { get; set; }
    
    public char? IconDefault = EmptySpaceIcon;
    public char? Icon { get; set; }
    
    public char? IconBuffer { get; set; }
    public char? HighlightIcon = HighlightedIcon;
    
    public List<Piece> Threats = new List<Piece>();

    
    public void AddPieceToThreats(Piece inPiece)
    {
        Threats.Add(inPiece);
    }

    public void ResetThreats()
    {
        Threats.Clear();
    }

    public void PrintThreats()
    {
        Console.WriteLine(this.GetReadablePos());
        foreach (var threat in Threats)
        {
            Console.WriteLine(threat.Name);
        }
    }
    
    public void PlacePiece(Piece inPiece)
    {
        Piece = inPiece;
        HasPiece = true;
        Piece.SetPosition(Pos);
        Icon = Piece.Icon;
    }

    public void SetPieceInfo(Piece inPiece)
    {
        Piece = inPiece;
        HasPiece = true;
        Icon = inPiece.Icon;
    }
    
    public void ClearPieceInfo()
    {
        Piece = null;
        HasPiece = false;
        // square symbol in unicode
        Icon = IconDefault;
    }

    public void SetIconHighlight()
    {
        IconBuffer = Icon;
        Icon = HighlightIcon;
    }

    public void UnsetIconHighlight()
    {
        Icon = IconBuffer;
        IconBuffer = null;
    }
    
    private void SetName()
    {
        Name = Col + Row.ToString();
    }

    public string GetReadablePos()
    {
        return Col + Row.ToString();
    }

    public string GetReadableFromIndex()
    {
        return "";

    }
    
    // convert index to readable position
    void ConvertIndexToReadable(Position inPosition)
    {
        Col = inPosition.ColIndex switch
        {
            0 => 'A',
            1 => 'B',
            2 => 'C',
            3 => 'D',
            4 => 'E',
            5 => 'F',
            6 => 'G',
            7 => 'H',
            _ => Col
        };

        Row = inPosition.RowIndex switch
        {
            0 => 8,
            1 => 7,
            2 => 6,
            3 => 5,
            4 => 4,
            5 => 3,
            6 => 2,
            7 => 1,
            _ => Row
        };
    }

    public string GetReadableFromIndex(Position inPosition)
    {
        char tempCol = '.';
        int tempRow = '.';
        
        tempCol = inPosition.ColIndex switch
        {
            0 => 'A',
            1 => 'B',
            2 => 'C',
            3 => 'D',
            4 => 'E',
            5 => 'F',
            6 => 'G',
            7 => 'H',
            _ => tempCol
        };

        tempRow = inPosition.RowIndex switch
        {
            0 => 8,
            1 => 7,
            2 => 6,
            3 => 5,
            4 => 4,
            5 => 3,
            6 => 2,
            7 => 1,
            _ => tempRow
        };

        return new string($"{tempCol}{tempRow}");
    }

    public Position GetPosition()
    {
        return this.Pos;
    }
    
    public static bool IsWithinBoard(Position pos)
    {
        if (pos.ColIndex is > 7 or < 0)
        {
            return false;
        }

        if (pos.RowIndex is > 7 or < 0)
        {
            return false;
        }

        return true;
    }
}