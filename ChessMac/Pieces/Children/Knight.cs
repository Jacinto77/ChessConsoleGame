using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

using static ChessBoard;

public class Knight : Piece
{
    // public Knight(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.Knight;
    //     SetIconByColorAndType(inColor);
    // }
    //
    // public Knight(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.Knight;
    //     SetIconByColorAndType(inColor);
    // }
    public Knight(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.Knight,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
    }
    
    // public Knight(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
    //     bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
    //     base(inColor, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition)
    // {
    // }

    public override Piece Clone()
    {
        return new Knight(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned,
            MoveCounter, IsThreatened);
    }

    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        // List<(int row, int col)> tempMoves = CreateList(
        //     (currentRow + 2, currentCol + 1),
        //     (currentRow + 2, currentCol - 1),
        //     (currentRow - 2, currentCol + 1),
        //     (currentRow - 2, currentCol - 1),
        //     (currentRow + 1, currentCol + 2),
        //     (currentRow + 1, currentCol - 2),
        //     (currentRow - 1, currentCol + 2),
        //     (currentRow - 1, currentCol - 2)
        // );
        List<(int row, int col)> tempMoves = new List<(int row, int col)>
        { 
            (Position.row + 2, Position.col + 1), 
            (Position.row + 2, Position.col - 1),
            (Position.row - 2, Position.col + 1),
            (Position.row - 2, Position.col - 1),
            (Position.row + 1, Position.col + 2),
            (Position.row + 1, Position.col - 2),
            (Position.row - 1, Position.col + 2),
            (Position.row - 1, Position.col - 2)
        };

        foreach (var possiblePosition in tempMoves)
        {
            if (IsWithinBoard(possiblePosition) == false) continue;

            var destSpace = inBoard.BoardSpaces[possiblePosition.row, possiblePosition.col];

            if (destSpace is not null)
                if (destSpace?.Color == Color)
                    continue;

            AddValidMove(possiblePosition);
        }
    }
}