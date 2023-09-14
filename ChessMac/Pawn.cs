namespace ChessMac;

public class Pawn : Piece
{
    public Pawn(string color, string name, char icon, string inType) 
        : base(color, name, icon)
    {
        this.Type = inType;
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        int currentCol = this.GetPosition().Col;
        int currentRow = this.GetPosition().Row;

        if (Color == "white")
        {
            Space forwardOne = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol);
            Space forwardTwo = inBoard.GetSpace(inRow: currentRow - 2, inCol: currentCol);
            Space forwardPos = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol + 1);
            Space forwardNeg = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol - 1);

            if (forwardOne._rowIndex != -1 && forwardOne._colIndex != -1 
                                           && forwardOne.HasPiece == false)
            {
                ValidMoves.Add(forwardOne);
            }
            if (forwardTwo._rowIndex != -1 && forwardTwo._colIndex != -1
                && forwardOne.HasPiece == false && forwardTwo.HasPiece == false)
            {
                ValidMoves.Add(forwardTwo);
            }
            if (forwardPos._rowIndex != -1 && forwardPos._colIndex != -1 
                                           && forwardPos.HasPiece == true )
            {
                ValidMoves.Add(forwardPos);
            }
            if (forwardNeg._rowIndex != -1 && forwardNeg._colIndex != -1
                                           && forwardNeg.HasPiece == true)
            {
                ValidMoves.Add(forwardNeg);
            }
            
        }
        else if (Color == "black")
        {
            Space forwardOne = inBoard.GetSpace(inRow: currentRow + 1, inCol: currentCol);
            Space forwardTwo = inBoard.GetSpace(inRow: currentRow + 2, inCol: currentCol);
            Space forwardPos = inBoard.GetSpace(inRow: currentRow + 1, inCol: currentCol + 1);
            Space forwardNeg = inBoard.GetSpace(inRow: currentRow + 1, inCol: currentCol - 1);

            if (forwardOne._rowIndex != -1 && forwardOne._colIndex != -1
                                           && forwardOne.HasPiece == false)
            {
                ValidMoves.Add(forwardOne);
            }

            if (forwardTwo._rowIndex != -1 && forwardTwo._colIndex != -1
                                           && forwardOne.HasPiece == false && forwardTwo.HasPiece == false)
            {
                ValidMoves.Add(forwardTwo);
            }

            if (forwardPos._rowIndex != -1 && forwardPos._colIndex != -1
                                           && forwardPos.HasPiece == true)
            {
                ValidMoves.Add(forwardPos);
            }

            if (forwardNeg._rowIndex != -1 && forwardNeg._colIndex != -1
                                           && forwardNeg.HasPiece == true)
            {
                ValidMoves.Add(forwardNeg);
            }
        }
    }
}