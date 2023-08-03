namespace ChessMac;

// king movement is any move one space in any direction
// validMove = currentPos[x, y] = x+/-1, y+/-1, or both

public class King : Piece
{
    public King(string color, string name, char icon, string type) 
        : base(color,  name, icon)
    {
        this.Type = type;
    }

    public bool IsMoveValid(Tuple<int, int> start, Tuple<int, int> destination)
    {
        // adding 1 to avoid sub/add by 0
        int rowDiff = Math.Abs((start.Item1 + 1) - (start.Item1 + 1));
        int colDiff = Math.Abs((start.Item2 + 1) - (destination.Item2 + 1));


        if (rowDiff > 1 || colDiff > 1)
        {
            return false;
        }
        
        return false;
    }
    
}