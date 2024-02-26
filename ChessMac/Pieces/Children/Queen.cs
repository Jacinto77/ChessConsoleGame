using ChessMac.Board;
using ChessMac.Pieces.Base;

namespace ChessMac.Pieces.Children;

// queen movement is a combination of bishop and rook movement
// any space diagonally or vertically/horizontally

public class Queen : Piece
{
    // public Queen(PieceColor inColor) : base(inColor)
    // {
    //     Type = PieceType.Queen;
    //     SetIconByColorAndType(inColor);
    // }
    //
    // public Queen(PieceColor inColor, (int row, int col) inPosition) : base(inColor, inPosition)
    // {
    //     Type = PieceType.Queen;
    //     SetIconByColorAndType(inColor);
    // }
    
    // public Queen(PieceType inType, PieceColor inColor, List<(int row, int col)> inValidMoves, char? inIcon,
    //     bool inHasMoved, bool inIsPinned, int inMoveCounter, bool inIsThreatened, (int row, int col) inPosition) : 
    //     base(inColor, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened, inPosition) {}

    public Queen(
        PieceColor inColor = default,
        (int row, int col) inPosition = default,
        PieceType inType = PieceType.Queen,
        List<(int row, int col)>? inValidMoves = default,
        char? inIcon = default,
        bool inHasMoved = default,
        bool inIsPinned = default,
        int inMoveCounter = default,
        bool inIsThreatened = default) 
        : base(inColor, inPosition, inType, inValidMoves, inIcon, inHasMoved, inIsPinned, inMoveCounter, inIsThreatened)
    {
    }
    
    public override Piece Clone()
    {
        return new Queen(Color, Position, Type, GetValidMoveList(), Icon, HasMoved, IsPinned,
            MoveCounter, IsThreatened);
    }

    // TODO make methods into extensions
    public override void GenerateValidMoves(ChessBoard inBoard)
    {
        ClearValidMoves();
        GenerateRookMoves(inBoard, Position.row, Position.col);
        // TODO: rook moves are being cleared from the list of valid moves or GenBishopMoves is overwriting them?
        // TODO: perhaps the deepcopy isn't working properly for the pieces?
        GenerateBishopMoves(inBoard, Position.row, Position.col);
    }
}