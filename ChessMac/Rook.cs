namespace ChessMac;

public class Rook : Piece
{
    public Rook(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}