namespace ChessMac;

public class Empty : Piece
{
    public Empty()
    {
        Type = PieceType.Null;
        Color = PieceColor.Null;
        Icon = EmptySpaceIcon;
        HasMoved = false;
        IsPinned = false;
        MoveCounter = 0;
    }
}