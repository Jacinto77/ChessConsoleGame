namespace ChessMac;

using static ChessBoard;

public class Knight : Piece
{
    public Knight(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Knight;
        Icon = GetColorPieceIcon(inColor);
    }

    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        List<(int row, int col)> tempMoves = CreateList(
            (currentRow + 2, currentCol + 1),
            (currentRow + 2, currentCol - 1),
            (currentRow - 2, currentCol + 1),
            (currentRow - 2, currentCol - 1),
            (currentRow + 1, currentCol + 2),
            (currentRow + 1, currentCol - 2),
            (currentRow - 1, currentCol + 2),
            (currentRow - 1, currentCol - 2)
        );

        foreach (var possiblePosition in tempMoves)
        {
            if (IsWithinBoard(possiblePosition) == false) continue;

            var destSpace = inBoard.BoardPieces[possiblePosition.row, possiblePosition.col];

            if (destSpace?.Icon != EmptySpaceIcon)
                if (destSpace?.Color == Color)
                    continue;

            AddValidMove(possiblePosition);
        }
    }
}