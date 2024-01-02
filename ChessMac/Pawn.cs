namespace ChessMac;

using static ChessBoard;

public class Pawn : Piece
{
    public Pawn(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Pawn;
        Icon = GetColorPieceIcon(inColor);
        SetPawnDirection(inColor);
    }

    private int Direction { get; set; }

    public override Piece DeepCopy()
    {
        var pawnCopy = (Pawn)base.DeepCopy();
        pawnCopy.Direction = Direction;

        return pawnCopy;
    }

    private void SetPawnDirection(PieceColor inColor)
    {
        if (inColor == PieceColor.Black) Direction = 1;
        else Direction = -1;
    }


    private void CheckAndAddDiagonal((int row, int col) diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal.row, diagonal.col)
            && inBoard.BoardPieces[diagonal.row, diagonal.col]?.Icon != EmptySpaceIcon
            && inBoard.BoardPieces[diagonal.row, diagonal.col]?.Color != Color)
        {
            AddValidMove(diagonal);
            inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
        }
    }

    private void CheckEnPassant((int row, int col) horizontal, (int row, int col) diagonal, ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontal.row, horizontal.col)) return;

        var horizPiece = inBoard.BoardPieces[horizontal.row, horizontal.col];
        var diagPiece = inBoard.BoardPieces[diagonal.row, diagonal.col];

        if (horizPiece?.Icon == EmptySpaceIcon || horizPiece?.Type != PieceType.Pawn)
            return;
        if (horizPiece.MoveCounter != 1 || diagPiece?.Icon != EmptySpaceIcon)
            return;

        AddValidMove(diagonal);
        inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
    }

    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        base.GenerateValidMoves(inBoard, currentRow, currentCol);

        (int row, int col) forwardOne = new(currentRow + Direction * 1, currentCol);
        (int row, int col) forwardTwo = new(currentRow + Direction * 2, currentCol);
        (int row, int col) diagPos = new(currentRow + Direction * 1, currentCol + 1);
        (int row, int col) diagNeg = new(currentRow + Direction * 1, currentCol - 1);
        (int row, int col) horizPos = new(currentRow, currentCol + 1);
        (int row, int col) horizNeg = new(currentRow, currentCol - 1);

        if (IsWithinBoard(forwardOne.row, forwardOne.col)
            && inBoard.BoardPieces[forwardOne.row, forwardOne.col]?.Icon == EmptySpaceIcon)
        {
            AddValidMove(forwardOne);

            if (IsWithinBoard(forwardTwo.row, forwardTwo.col)
                && inBoard.BoardPieces[forwardTwo.row, forwardTwo.col]?.Icon == EmptySpaceIcon
                && HasMoved == false)
                AddValidMove(forwardTwo);
        }

        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
        CheckEnPassant(horizPos, diagPos, inBoard);
        CheckEnPassant(horizNeg, diagNeg, inBoard);
    }
}