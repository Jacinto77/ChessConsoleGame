using System.Runtime.CompilerServices;

namespace ChessMac;
using static ChessBoard;

public class Pawn : Piece
{
    private int Direction { get; set; }

    public Pawn(PieceColor inColor) : base(inColor)
    {
        this.Type = PieceType.Pawn;
        this.Icon = GetColorPieceIcon(inColor); 
        SetPawnDirection(inColor);
    }

    public override Piece DeepCopy()
    {
        Pawn pawnCopy =  (Pawn)base.DeepCopy();
        pawnCopy.Direction = this.Direction;

        return pawnCopy;
    }

    private void SetPawnDirection(PieceColor inColor)
    {
        if (inColor == PieceColor.Black) Direction = 1;
        else Direction = -1;
    }
    
    
    private void CheckAndAddDiagonal((int row, int col) diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal.row, diagonal.col) && inBoard.BoardPieces[diagonal.row, diagonal.col] is not null
                                    && inBoard.BoardPieces[diagonal.row, diagonal.col]?.Color != Color)
        {
            AddValidMove(diagonal);
        }
    }
    
    private void CheckEnPassant((int row, int col) horizontal, (int row, int col) diagonal, ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontal.row, horizontal.col)) return;
        
        Piece? horizPiece = inBoard.BoardPieces[horizontal.row, horizontal.col];
        Piece? diagPiece = inBoard.BoardPieces[diagonal.row, diagonal.col];
        
        if (horizPiece is null || horizPiece.Type != PieceType.Pawn) 
            return;
        if (horizPiece.MoveCounter != 1 || diagPiece is not null)
            return;

        AddValidMove(horizontal);
        
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard, int currentRow, int currentCol)
    {
        base.GenerateValidMoves(inBoard, currentRow, currentCol);
        ClearValidMoves();

        (int row, int col) forwardOne = new (currentRow + (Direction * 1), currentCol);
        (int row, int col) forwardTwo = new (currentRow + (Direction * 2), currentCol);
        (int row, int col) diagPos = new (currentRow + (Direction * 1),  currentCol + 1);
        (int row, int col) diagNeg = new ( currentRow + (Direction * 1),  currentCol - 1);
        (int row, int col) horizPos = new (currentRow, currentCol + 1);
        (int row, int col) horizNeg = new (currentRow, currentCol - 1);
        
        if (IsWithinBoard(forwardOne.row, forwardOne.col) && inBoard.BoardPieces[forwardOne.row, forwardOne.col] is null)
        {
            AddValidMove(forwardOne);

            if (IsWithinBoard(forwardTwo.row, forwardTwo.col) 
                && inBoard.BoardPieces[forwardTwo.row, forwardTwo.col] is null
                && this.HasMoved == false)
            {
                AddValidMove(forwardTwo);
            }
        }
        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
        CheckEnPassant(horizPos, diagPos, inBoard);
        CheckEnPassant(horizNeg, diagNeg, inBoard);
    }
}