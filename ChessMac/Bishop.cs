namespace ChessMac;

public class Bishop : Piece
{
    public Bishop(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}