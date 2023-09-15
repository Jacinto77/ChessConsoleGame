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

        _position.RowIndex = inRowIndex;
        _position.ColIndex = inColIndex;
        
        ConvertIndexToPosition(_position);
        SetName();
        HasPiece = false;
        Piece = null;
        Icon = IconDefault;
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
    
    private Position _position = new();
    public readonly int RowIndex;
    public readonly int ColIndex;

    public int Row { get; set; }
    public char Col { get; set; }
    
    // ie A3, G7, etc
    public string? Name { get; set; }
    
    public Piece? Piece { get; set; }
    
    public bool HasPiece { get; set; }
    
    // Icon to display on board, set to [] by default, if haspiece=true icon= piece.icon
    // if space chosen to move to, 
    public char? IconDefault = '\u2610';
    public char? Icon { get; set; }
    public char? IconStore { get; set; }
    public char? HighlightIcon = '\u25C9';
    
    public void PlacePiece(Piece inPiece)
    {
        Piece = inPiece;
        HasPiece = true;
        Piece.SetPosition(_position);
        Icon = Piece.Icon;
        
    }

    public void ClearSpace()
    {
        Piece = null;
        HasPiece = false;
        // square symbol in unicode
        Icon = IconDefault;
        
    }

    public void SetIconHighlight()
    {
        IconStore = Icon;
        Icon = HighlightIcon;
    }

    public void UnsetIconHighlight()
    {
        Icon = IconStore;
        IconStore = null;
    }
    
    private void SetName()
    {
        Name = Col + Row.ToString();
    }

    public string Get_Readable_Pos()
    {
        return Col + Row.ToString();
    }
    
    // convert index to readable position
    void ConvertIndexToPosition(Position inPosition)
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