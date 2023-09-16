namespace ChessMac;
using static Space;

// TODO: implement en passant
// TODO: implement promotion
// TODO: code in piece direction with private readonly int direction = 1/-1

public class Pawn : Piece
{
    public Pawn(PieceColor color, string name, char icon, PieceType type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }

    public Pawn(PieceColor color, PieceType type) : base(color, type)
    {
    }

    private void CheckAndAddDiagonal(Position diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal) && inBoard.GetPiece(diagonal)?.Color != Color 
                                    && inBoard.GetSpace(diagonal).HasPiece)
        {
            ValidMoves.Add(inBoard.GetSpace(diagonal));
        }
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        int currentCol = this.GetPosition().ColIndex;
        int currentRow = this.GetPosition().RowIndex;

        int direction = (Color == PieceColor.White) ? -1 : 1;

        Position forwardOne = new Position(inRowIndex: currentRow + (direction * 1), inColIndex: currentCol);
        Position forwardTwo = new Position(inRowIndex: currentRow + (direction * 2), inColIndex: currentCol);
        Position diagPos = new Position(inRowIndex: currentRow + (direction * 1), inColIndex: currentCol + 1);
        Position diagNeg = new Position(inRowIndex: currentRow + (direction * 1), inColIndex: currentCol - 1);

        if (IsWithinBoard(forwardOne) && inBoard.GetSpace(forwardOne).HasPiece == false)
        {
            ValidMoves.Add(inBoard.GetSpace(forwardOne));

            if (IsWithinBoard(forwardTwo) && inBoard.GetSpace(forwardTwo).HasPiece == false
                                       && this.HasMoved == false)
            {
                ValidMoves.Add(inBoard.GetSpace(forwardTwo));
            }
        }
        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
    }
}