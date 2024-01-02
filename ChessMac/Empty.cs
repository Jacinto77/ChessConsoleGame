namespace ChessMac;

public class Empty : Piece
{
    public Empty()
    {
        Type = PieceType.NULL;
        Color = PieceColor.NULL;
        Icon = EmptySpaceIcon;
        HasMoved = false;
        IsPinned = false;
        MoveCounter = 0;
    }
}