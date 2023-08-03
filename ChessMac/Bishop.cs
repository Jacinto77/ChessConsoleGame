namespace ChessMac;

// bishop movement is diagonals only

// validmove = currentPos[x,y] = x&&y +/- n
// both x and y have to change by the same amount, but the sign can be different


public class Bishop : Piece
{
    public Bishop(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}