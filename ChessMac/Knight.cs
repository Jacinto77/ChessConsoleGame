namespace ChessMac;

public class Knight : Piece
{
    public Knight(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}