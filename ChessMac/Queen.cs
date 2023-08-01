namespace ChessMac;

public class Queen : Piece
{
    public Queen(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}