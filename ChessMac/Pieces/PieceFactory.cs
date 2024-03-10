using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace ChessMac.Pieces;


public static class PieceFactory
{
    public static Piece CreatePiece(Piece.PieceType pieceType, Piece.PieceColor color)
    {
        switch (pieceType)
        {
            case Piece.PieceType.Pawn:
                return new Pawn(color);
            case Piece.PieceType.Rook:
                return new Rook(color);
            case Piece.PieceType.Knight:
                return new Knight(color);
            case Piece.PieceType.Bishop:
                return new Bishop(color);
            case Piece.PieceType.Queen:
                return new Queen(color);
            case Piece.PieceType.King:
                return new King(color);
            default:
                throw new ArgumentException("Invalid piece type", nameof(pieceType));
        }
    }
}