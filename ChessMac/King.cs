namespace ChessMac;

public class King : Piece
{
    public King(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}