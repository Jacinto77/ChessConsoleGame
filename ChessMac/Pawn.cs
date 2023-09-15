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
        int currentCol = this.GetPosition().ColIndex;
        int currentRow = this.GetPosition().RowIndex;

        if (Color == "white")
        {
            Space forwardOne = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol);
            Space forwardTwo = inBoard.GetSpace(inRow: currentRow - 2, inCol: currentCol);
            Space forwardPos = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol + 1);
            Space forwardNeg = inBoard.GetSpace(inRow: currentRow - 1, inCol: currentCol - 1);

            if (forwardOne.RowIndex != -1 && forwardOne.ColIndex != -1 
                                           && forwardOne.HasPiece == false)
            {
                ValidMoves.Add(forwardOne);
            }
            if (forwardTwo.RowIndex != -1 && forwardTwo.ColIndex != -1
                                           && forwardOne.HasPiece == false 
                                           && forwardTwo.HasPiece == false)
            {
                ValidMoves.Add(forwardTwo);
            }
            if (forwardPos.RowIndex != -1 && forwardPos.ColIndex != -1 
                                           && forwardPos.HasPiece == true 
                                           && forwardPos.Piece?.Color != this.Color )
            {
                ValidMoves.Add(forwardPos);
            }
            if (forwardNeg.RowIndex != -1 && forwardNeg.ColIndex != -1
                                           && forwardNeg.HasPiece == true 
                                           && forwardNeg.Piece?.Color != this.Color)
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

            if (forwardOne.RowIndex != -1 && forwardOne.ColIndex != -1 
                                           && forwardOne.HasPiece == false)
            {
                ValidMoves.Add(forwardOne);
            }
            if (forwardTwo.RowIndex != -1 && forwardTwo.ColIndex != -1
                                           && forwardOne.HasPiece == false 
                                           && forwardTwo.HasPiece == false)
            {
                ValidMoves.Add(forwardTwo);
            }
            if (forwardPos.RowIndex != -1 && forwardPos.ColIndex != -1 
                                           && forwardPos.HasPiece == true 
                                           && forwardPos.Piece?.Color != this.Color )
            {
                ValidMoves.Add(forwardPos);
            }
            if (forwardNeg.RowIndex != -1 && forwardNeg.ColIndex != -1
                                           && forwardNeg.HasPiece == true 
                                           && forwardNeg.Piece?.Color != this.Color)
            {
                ValidMoves.Add(forwardNeg);
            }
        }
    }
}