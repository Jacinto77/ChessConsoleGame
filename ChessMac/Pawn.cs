namespace ChessMac;
using static Space;
using static Methods;

// TODO: implement en passant
// TODO: implement promotion
// TODO: code in piece direction with private readonly int direction = 1/-1

public class Pawn : Piece
{
    private readonly int direction;
    
    public Pawn(PieceColor color, string name, char icon, PieceType type) 
        : base(color, name, icon)
    {
        this.Type = type;
    }
    
    public Pawn(PieceColor color, PieceType type) : base(color, type)
    {
        direction = (Color == PieceColor.White) ? -1 : 1;
    }
    
    private void CheckAndAddDiagonal(Tuple<int, int> diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal) && inBoard.GetPiece(diagonal)?.Color != Color 
                                    && inBoard.GetSpace(diagonal).HasPiece)
        {
            ValidMoves.Add(diagonal);
        }
    }
    
    private void CheckEnPassant(Tuple<int, int> horizontal, Tuple<int, int> diagonal, ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontal)) return;
        if (!inBoard.GetSpace(horizontal).HasPiece || inBoard.GetPiece(horizontal)?.Type != PieceType.Pawn) 
            return;
        if (inBoard.GetPiece(horizontal)?.MoveCounter != 1 || inBoard.GetSpace(diagonal).HasPiece)
            return;

        ValidMoves.Add(horizontal);
        
    }
    
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ValidMoves.Clear();
        int currentCol = ColIndex;
        int currentRow = RowIndex;
        
        Tuple<int, int> forwardOne = new Tuple<int, int>(currentRow + (direction * 1), currentCol);
        Tuple<int, int> forwardTwo = new Tuple<int, int>(currentRow + (direction * 2), currentCol);
        Tuple<int, int> diagPos = new Tuple<int, int>(currentRow + (direction * 1),  currentCol + 1);
        Tuple<int, int> diagNeg = new Tuple<int, int>( currentRow + (direction * 1),  currentCol - 1);
        Tuple<int, int> horizPos = new Tuple<int, int>(currentRow, currentCol + 1);
        Tuple<int, int> horizNeg = new Tuple<int, int>(currentRow, currentCol - 1);
        
        if (IsWithinBoard(forwardOne) && inBoard.GetSpace(forwardOne).HasPiece == false)
        {
            ValidMoves.Add(forwardOne);

            if (IsWithinBoard(forwardTwo) && inBoard.GetSpace(forwardTwo).HasPiece == false
                                       && this.HasMoved == false)
            {
                ValidMoves.Add(forwardOne);
            }
        }
        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
        CheckEnPassant(horizPos, diagPos, inBoard);
        CheckEnPassant(horizNeg, diagNeg, inBoard);
    }
}