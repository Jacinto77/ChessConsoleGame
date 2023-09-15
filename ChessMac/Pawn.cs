namespace ChessMac;
using static Space;

public class Pawn : Piece
{
    public Pawn(string color, string name, char icon, string type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        int currentCol = this.GetPosition().ColIndex;
        int currentRow = this.GetPosition().RowIndex;

        Space.Position forwOne = new Space.Position(inRowIndex: currentRow, inColIndex: currentCol);
        Space.Position forwTwo = new Space.Position(inRowIndex: currentRow, inColIndex: currentCol);
        Space.Position forwPos = new Space.Position(inRowIndex: currentRow, inColIndex: currentCol + 1);
        Space.Position forwNeg = new Space.Position(inRowIndex: currentRow, inColIndex: currentCol - 1);


        if (Color == "white")
        {
            //TODO: choose between ChessBoard.GetSpace() or using Space.IsWithinBounds() for validation
            //TODO: actually ya know what, Space.Is... is way better

            int offsetOne = -1;
            int offsetTwo = -2;

            forwOne.RowIndex += offsetOne;
            forwTwo.RowIndex += offsetTwo;
            forwPos.RowIndex += offsetOne;
            forwNeg.RowIndex += offsetOne;

            if (IsWithinBoard(forwOne) && inBoard.GetPiece(forwOne) == null)
            {
                ValidMoves.Add(inBoard.GetSpace(forwOne));

                if (IsWithinBoard(forwTwo) && inBoard.GetSpace(forwTwo).HasPiece == false
                                           && this.HasMoved == false)
                {
                    ValidMoves.Add(inBoard.GetSpace(forwTwo));
                }
            }

            if (IsWithinBoard(forwPos) && inBoard.GetPiece(forwPos).Color != Color)
            {
                ValidMoves.Add(inBoard.GetSpace(forwPos));
            }

            if (IsWithinBoard(forwNeg) && inBoard.GetPiece(forwNeg).Color != Color)
            {
                ValidMoves.Add(inBoard.GetSpace(forwNeg));
            }
        }
        else if (Color == "black") // TODO: finish new pawn implementation and test
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
                                           && forwardTwo is { HasPiece: false, Piece.HasMoved: false })
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