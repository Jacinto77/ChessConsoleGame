using ChessMac.Board;
using ChessMac.ChessBoard;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static Board.ChessBoard;

public class Pawn : Piece
{
    public Pawn(PieceColor inColor) : base(inColor)
    {
        Type = PieceType.Pawn;
        Icon = GetColorPieceIcon(inColor);
        SetPawnDirection(inColor);
    }

    public Pawn(PieceColor inColor, PieceType inType) : base(inColor, inType)
    {
        SetPawnDirection(inColor);
    }

    public Pawn(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
        bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened) : 
        base(inType, inColor, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
        SetPawnDirection(inColor);
    }

    private int Direction { get; set; }

    public override Piece Clone()
    {
        return new Pawn(this.Type, this.Color, this.GetValidMoveList(), this.Icon, this.HasMoved, this.IsPinned,
            this.MoveCounter, this.IsThreatened);
    }
    
    public override Piece DeepCopy()
    {
        Pawn pawnCopy = new Pawn(Color, Type);
        pawnCopy.Direction = this.Direction;

        return pawnCopy;
    }

    private void SetPawnDirection(PieceColor inColor)
    {
        if (inColor == PieceColor.Black) Direction = 1;
        else Direction = -1;
    }


    private void CheckAndAddDiagonal((int row, int col) diagonal, Board.ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal)
            && inBoard.BoardPieces[diagonal.row, diagonal.col] is null
            && inBoard.BoardPieces[diagonal.row, diagonal.col]?.Color != Color)
        {
            AddValidMove(diagonal);
            inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
        }
    }

    private void CheckEnPassant((int row, int col) horizontal, (int row, int col) diagonal, Board.ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontal)) return;

        var horizPiece = inBoard.BoardPieces[horizontal.row, horizontal.col];
        var diagPiece = inBoard.BoardPieces[diagonal.row, diagonal.col];

        if (horizPiece is null || horizPiece.Type != PieceType.Pawn)
            return;
        if (horizPiece.MoveCounter != 1 || diagPiece is null)
            return;

        AddValidMove(diagonal);
        inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
    }

    public override void GenerateValidMoves(Board.ChessBoard inBoard, int currentRow, int currentCol)
    {
        base.GenerateValidMoves(inBoard, currentRow, currentCol);

        (int row, int col) forwardOne = new(currentRow + Direction * 1, currentCol);
        (int row, int col) forwardTwo = new(currentRow + Direction * 2, currentCol);
        (int row, int col) diagPos = new(currentRow + Direction * 1, currentCol + 1);
        (int row, int col) diagNeg = new(currentRow + Direction * 1, currentCol - 1);
        (int row, int col) horizPos = new(currentRow, currentCol + 1);
        (int row, int col) horizNeg = new(currentRow, currentCol - 1);

        if (IsWithinBoard(forwardOne)
            && inBoard.BoardPieces[forwardOne.row, forwardOne.col] is null)
        {
            AddValidMove(forwardOne);

            if (IsWithinBoard(forwardTwo)
                && inBoard.BoardPieces[forwardTwo.row, forwardTwo.col] is null
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