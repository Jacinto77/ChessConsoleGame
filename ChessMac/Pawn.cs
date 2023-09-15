namespace ChessMac;
using static Space;

// TODO: implement en passant

public class Pawn : Piece
{
    public Pawn(string color, string name, char icon, string type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Pawn(string color, string type) : base(color, type)
    {
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        int currentCol = this.GetPosition().ColIndex;
        int currentRow = this.GetPosition().RowIndex;

        int offsetOne = 0;
        int offsetTwo = 0;
        
        switch (Color)
        {
            case "white":
                offsetOne = -1;
                offsetTwo = -2;
                break;
            case "black":
                offsetOne = 1;
                offsetTwo = 2;
                break;
        }
        
        Position forwOne = new Position(inRowIndex: currentRow + offsetOne, inColIndex: currentCol);
        Position forwTwo = new Position(inRowIndex: currentRow + offsetTwo, inColIndex: currentCol);
        Position forwPos = new Position(inRowIndex: currentRow + offsetOne, inColIndex: currentCol + 1);
        Position forwNeg = new Position(inRowIndex: currentRow + offsetOne, inColIndex: currentCol - 1);

        if (IsWithinBoard(forwOne) && inBoard.GetPiece(forwOne) == null)
        {
            ValidMoves.Add(inBoard.GetSpace(forwOne));

            if (IsWithinBoard(forwTwo) && inBoard.GetSpace(forwTwo).HasPiece == false
                                       && this.HasMoved == false)
            {
                ValidMoves.Add(inBoard.GetSpace(forwTwo));
            }
        }
        // diagonal take
        if (IsWithinBoard(forwPos) && inBoard.GetPiece(forwPos)?.Color != Color 
                                   && inBoard.GetSpace(forwPos).HasPiece)
        {
            ValidMoves.Add(inBoard.GetSpace(forwPos));
        }
        else
        {
            inBoard.GetSpace(forwPos).AddPieceToThreats(this);
        }
        // diagonal take
        if (IsWithinBoard(forwNeg) && inBoard.GetPiece(forwNeg)?.Color != Color 
                                   && inBoard.GetSpace(forwPos).HasPiece)
        {
            ValidMoves.Add(inBoard.GetSpace(forwNeg));
        }
        else
        {
            inBoard.GetSpace(forwNeg).AddPieceToThreats(this);
        }
    }
}