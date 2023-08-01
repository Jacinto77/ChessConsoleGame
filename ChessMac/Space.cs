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
    
    // indices of space's position, set during InitBoard, readonly and should not be changed
    private Position _position = new();
    private readonly int _rowIndex;
    private readonly int _colIndex;

    // human readable (ie A3, G8, etc), set during initialization, readonly and should not be changed
    public int Row { get; set; }
    public char Col { get; set; }
    
    // full name of space, ie A3, G7, etc
    public string? Name { get; set; }
    
    // holds reference to pieces in game, is changed by methods to move pieces
    public Piece? Piece { get; set; }
    
    // bool property to quickly tell if there is a piece at this location
    public bool HasPiece { get; set; }
    
    // Icon to display on board, set to [] by default, if haspiece=true icon= piece.icon
    // if space chosen to move to, 
    public char Icon { get; set; }
    
    // assign piece to space and set HasPiece
    public void PlacePiece(Piece inPiece)
    {
        Piece = inPiece;
        HasPiece = true;
        Piece.SetPosition(_position);
    }

    // clear space and set to empty
    public void ClearSpace()
    {
        Piece = null;
        HasPiece = false;
    }

    private void SetName()
    {
        Name = Col + Row.ToString();
    }
    
    
    // convert index to readable position
    private void ConvertIndexToPosition(Position inPosition)
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
}