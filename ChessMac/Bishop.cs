namespace ChessMac;

// bishop movement is diagonals only

// validmove = currentPos[x,y] = x&&y +/- n
// both x and y have to change by the same amount, but the sign can be different


public class Bishop : Piece
{
    public Bishop(string color, string name, char icon, string type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateValidMoves(inBoard);
        int currentCol = GetPosition().Col;
        int currentRow = GetPosition().Row;

        Tuple<int, int> rangeUpLeft = new Tuple<int, int>(currentRow, currentCol);
        Tuple<int, int> rangeDownRight = new Tuple<int, int>(7 - currentRow, 7 - currentCol);
        Tuple<int, int> rangeUpRight = new Tuple<int, int>(currentRow, 7 - currentCol);
        Tuple<int, int> rangeDownLeft = new Tuple<int, int>(7 - currentRow, currentCol);
        
        // scan up-left
        for (int i = 1; i < rangeUpLeft.Item1 || i < rangeUpLeft.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan up-right
        for (int i = 1; i < rangeUpRight.Item1 || i < rangeUpRight.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-right
        for (int i = 1; i < rangeDownRight.Item1 || i < rangeDownRight.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol + i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        // scan down-left
        for (int i = 1; i < rangeDownLeft.Item1 || i < rangeDownLeft.Item2; i++)
        {
            Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol - i];
            if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
                break;
            if (tempSpace.HasPiece == true)
            {
                ValidMoves.Add(tempSpace);
                break;
            }
            if (tempSpace.HasPiece == false)
                ValidMoves.Add(tempSpace);
        }
        
        int counter = 1;
        foreach (var move in ValidMoves)
        {
            Console.WriteLine(counter);
            Console.WriteLine(move.Col + move.Row.ToString());
            counter++;
        }
    }
}