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

    public Bishop(string color, string type) : base(color, type)
    {
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateValidMoves(inBoard);
        base.GenerateBishopMoves(inBoard);
        
        // int currentCol = GetPosition().ColIndex;
        // int currentRow = GetPosition().RowIndex;
        //
        // int rangeUp = currentRow + 1;
        // int rangeDown = 8 - currentRow;
        // int rangeLeft = currentCol + 1;
        // int rangeRight = 8 - currentCol;
        //
        // // scan up-left
        // for (int i = 1; i < rangeUp && i < rangeLeft; i++)
        // {
        //     Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol - i];
        //     if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
        //         break;
        //     if (tempSpace.HasPiece == true)
        //     {
        //         ValidMoves.Add(tempSpace);
        //         break;
        //     }
        //     if (tempSpace.HasPiece == false)
        //         ValidMoves.Add(tempSpace);
        // }
        //
        // // scan up-right
        // for (int i = 1; i < rangeUp && i < rangeRight; i++)
        // {
        //     Space tempSpace = inBoard.BoardSpaces[currentRow - i, currentCol + i];
        //     if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
        //         break;
        //     if (tempSpace.HasPiece == true)
        //     {
        //         ValidMoves.Add(tempSpace);
        //         break;
        //     }
        //     if (tempSpace.HasPiece == false)
        //         ValidMoves.Add(tempSpace);
        // }
        //
        // // scan down-right
        // for (int i = 1; i < rangeDown && i < rangeRight; i++)
        // {
        //     Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol + i];
        //     if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
        //         break;
        //     if (tempSpace.HasPiece == true)
        //     {
        //         ValidMoves.Add(tempSpace);
        //         break;
        //     }
        //     if (tempSpace.HasPiece == false)
        //         ValidMoves.Add(tempSpace);
        // }
        //
        // // scan down-left
        // for (int i = 1; i < rangeDown && i < rangeLeft; i++)
        // {
        //     Space tempSpace = inBoard.BoardSpaces[currentRow + i, currentCol - i];
        //     if (tempSpace.HasPiece && tempSpace.Piece.Color == Color)
        //         break;
        //     if (tempSpace.HasPiece == true)
        //     {
        //         ValidMoves.Add(tempSpace);
        //         break;
        //     }
        //     if (tempSpace.HasPiece == false)
        //         ValidMoves.Add(tempSpace);
        // }
        
        // int counter = 1;
        // foreach (var move in ValidMoves)
        // {
        //     Console.WriteLine(counter);
        //     Console.WriteLine(move.Col + move.Row.ToString());
        //     counter++;
        // }
    }
}