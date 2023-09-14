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
        _rowIndex = inRowIndex;
        _colIndex = inColIndex;

        _position.Row = inRowIndex;
        _position.Col = inColIndex;
        
        ConvertIndexToPosition(_position);
        SetName();
        HasPiece = false;
        Piece = null;
        
    }
    
    public struct Position
    {
        public int Row;
        public int Col;
        
        public Position(int inRow, int inCol)
        {
            Row = inRow;
            Col = inCol;
        }
    }
    
    private Position _position = new();
    public readonly int _rowIndex;
    public readonly int _colIndex;

    public int Row { get; set; }
    public char Col { get; set; }
    
    // ie A3, G7, etc
    public string? Name { get; set; }
    
    public Piece? Piece { get; set; }
    
    public bool HasPiece { get; set; }
    
    // Icon to display on board, set to [] by default, if haspiece=true icon= piece.icon
    // if space chosen to move to, 
    public char? Icon { get; set; }
    public char? IconStore { get; set; }
    
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
        Icon = null;
        
    }

    public void SetIconHighlight()
    {
        IconStore = Icon;
        Icon = '\u25C9';
    }

    public void UnsetIconHighlight()
    {
        Icon = IconStore;
        IconStore = '\u25C9';
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
        Col = inPosition.Col switch
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

        Row = inPosition.Row switch
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

    public static bool IsValidPosition(Position pos)
    {
        if (pos.Col is > 7 or < 0)
        {
            return false;
        }

        if (pos.Row is > 7 or < 0)
        {
            return false;
        }

        return true;
    }
}