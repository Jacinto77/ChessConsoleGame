using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace ChessMac.Pieces;


public static class PieceFactory
{
    public static Piece CreatePiece(string pieceType, Piece.PieceColor color)
    {
        switch (pieceType)
        {
            case "Pawn":
                return new Pawn(color);
            case "Rook":
                return new Rook(color);
            case "Knight":
                return new Knight(color);
            case "Bishop":
                return new Bishop(color);
            case "Queen":
                return new Queen(color);
            case "King":
                return new King(color);
            default:
                throw new ArgumentException("Invalid piece type", nameof(pieceType));
        }
    }
}