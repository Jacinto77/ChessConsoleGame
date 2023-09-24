namespace ChessMac;
using static Methods;
// Each space on the chessboard
// 64 instances of this class will be created and stored in array within ChessBoard
    // instance
// holds own data regarding which space it is, whether is is currently occupied, 
    // and a reference to the piece currently on it, if any

public class Space
{
    public Space()
    {
        HasPiece = false;
        Piece = null;
        Icon = IconDefault;
        IsThreatened = false;
    }
    
    
    public Piece? Piece { get; set; }
    
    private const char EmptySpaceIcon = '\u2610';
    private const char HighlightedIcon = '\u25C9';
    
    public bool HasPiece { get; set; }
    
    public char? IconDefault = EmptySpaceIcon;
    public char? Icon { get; set; }
    
    public char? IconBuffer { get; set; }
    public char? HighlightIcon = HighlightedIcon;
    
    public bool IsThreatened;

    public Space? DeepCopy()
    {
        Space tempSpace = (Space)MemberwiseClone();
        
        if (Piece != null)
        {
            tempSpace.Piece = Piece.DeepCopy();
        }

        return tempSpace;
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
    
    public static bool IsWithinBoard(Tuple<int, int> inMove)
    {
        if (inMove.Item1 is > 7 or < 0)
        {
            return false;
        }

        if (inMove.Item2 is > 7 or < 0)
        {
            return false;
        }

        return true;
    }
}