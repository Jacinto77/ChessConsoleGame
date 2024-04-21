using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static ChessBoard;

public class Pawn : Piece
{
    // public Pawn(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.Pawn;
    //     SetIconByColorAndType(inColor);
    //     SetPawnDirection(inColor);
    // }
    
    // public Pawn(PieceColor inColor, PieceType inType) : base(inColor, inType)
    // {
    //     Type = PieceType.Pawn;
    //     SetIconByColorAndType(inColor);
    //     SetPawnDirection(inColor);
    // }
    
    // public Pawn(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.Pawn;
    //     SetIconByColorAndType(inColor);
    //     SetPawnDirection(inColor);
    // }

    public Pawn(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.Pawn,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
        SetPawnDirection(inColor);
    }

    private int Direction { get; set; }

    public override Piece Clone()
    {
        var clonedPawn = new Pawn(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned, MoveCounter, IsThreatened)
            {
                 Direction = Direction
            };

        return clonedPawn;
    }
    
    // public override Piece DeepCopy()
    // {
    //     Pawn pawnCopy = new Pawn(Color, Type);
    //     pawnCopy.Direction = Direction;
    //
    //     return pawnCopy;
    // }

    private void SetPawnDirection(PieceColor inColor)
    {
        if (inColor == PieceColor.Black) Direction = 1;
        else Direction = -1;
    }


    private void CheckAndAddDiagonal((int row, int col) diagonal, ChessBoard inBoard)
    {
        if (IsWithinBoard(diagonal)
            && inBoard.BoardSpaces[diagonal.row, diagonal.col] is not null
            && inBoard.BoardSpaces[diagonal.row, diagonal.col]?.Color != Color)
        {
            AddValidMove(diagonal);
            //inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
        }
    }

    // TODO checks for a space like in this function should have the arguments verified to be within bounds prior to calling
    private void CheckEnPassant((int row, int col) horizontalSpace, (int row, int col) diagonalSpace, ChessBoard inBoard)
    {
        if (!IsWithinBoard(horizontalSpace) || !IsWithinBoard(diagonalSpace)) return;
        
        var horizPiece = inBoard.GetPieceByIndex(horizontalSpace);
        var diagPiece = inBoard.GetPieceByIndex(diagonalSpace);
        // var horizPiece = inBoard.BoardPieces[horizontalSpace.row, horizontalSpace.col];
        // var diagPiece = inBoard.BoardPieces[diagonalSpace.row, diagonalSpace.col];

        if (horizPiece is null || horizPiece.Type != PieceType.Pawn)
            return;
        if (horizPiece.MoveCounter != 1 || diagPiece is not null)
            return;

        AddValidMove(diagonalSpace);
        //inBoard.BoardPieces[diagonal.row, diagonal.col]?.SetThreat();
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        base.GenerateValidMoves(inBoard);

        (int row, int col) forwardOne = new (Position.row + Direction * 1, Position.col);
        (int row, int col) forwardTwo = new (Position.row + Direction * 2, Position.col);
        (int row, int col) diagPos =    new (Position.row + Direction * 1, Position.col + 1);
        (int row, int col) diagNeg =    new (Position.row + Direction * 1, Position.col - 1);
        (int row, int col) horizPos =   new (Position.row, Position.col + 1);
        (int row, int col) horizNeg =   new (Position.row, Position.col - 1);

        if (IsWithinBoard(forwardOne)
            && inBoard.BoardSpaces[forwardOne.row, forwardOne.col] is null)
        {
            AddValidMove(forwardOne);

            if (IsWithinBoard(forwardTwo)
                && inBoard.BoardSpaces[forwardTwo.row, forwardTwo.col] is null
                && HasMoved == false)
                AddValidMove(forwardTwo);
        }

        // diagonal take
        CheckAndAddDiagonal(diagPos, inBoard);
        CheckAndAddDiagonal(diagNeg, inBoard);
        CheckEnPassant(horizPos, diagPos, inBoard);
        CheckEnPassant(horizNeg, diagNeg, inBoard);
    }

    public override void PrintAttributes()
    {
        base.PrintAttributes();
        Console.WriteLine($"Pawn Direction: {Direction}");
    }
}