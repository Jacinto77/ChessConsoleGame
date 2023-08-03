namespace ChessMac;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    public Queen(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }
}