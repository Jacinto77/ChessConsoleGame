namespace ChessMac;
using static Methods;
// Each space on the chessboard
// 64 instances of this class will be created and stored in array within ChessBoard
    // instance
// holds own data regarding which space it is, whether is is currently occupied, 
    // and a reference to the piece currently on it, if any

public class Space
{
    public Space(Tuple<int, int> inPosition)
    {
        HasPiece = false;
        Piece = null;
        Icon = IconDefault;
        IsThreatened = false;
        SpaceIndex = inPosition;
        SpaceName = SetName(inPosition);
    }
    
    public Piece? Piece { get; set; }
    
    private const char EmptySpaceIcon = '\u2610';
    private const char HighlightedIcon = '\u25C9';
    
    public Tuple<int, int> SpaceIndex { get; }
    public string SpaceName { get; private set; }
    public bool HasPiece { get; set; }
    
    public char? IconDefault = EmptySpaceIcon;
    public char? Icon { get; set; }
    
    public char? IconBuffer { get; set; }
    public char? HighlightIcon = HighlightedIcon;
    
    public bool IsThreatened = false;

    public Space DeepCopy()
    {
        Space tempSpace = (Space)MemberwiseClone();
        return tempSpace;
    }

    public string SetName(Tuple<int, int> inPosition)
    {
        return ConvertIndexToPos(inPosition);
    }

    public void SetPieceInfo(Piece inPiece)
    {
        ClearPieceInfo();
        this.Piece = inPiece;
        this.HasPiece = true;
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
    
    public static bool IsWithinBoard((int row, int col) inMove)
    {
        if (inMove.row is > 7 or < 0)
        {
            return false;
        }

        if (inMove.col is > 7 or < 0)
        {
            return false;
        }

        return true;
    }
}