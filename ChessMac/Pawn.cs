namespace ChessMac;

public class Pawn : Piece
{
    public Pawn(string color, string name, char icon, string inType) 
        : base(color, name, icon)
    {
        this.Type = inType;
    }

    // public Pawn()
    //     : this (color: "white", name: "pawn", icon: 'n', inType: "pawn")
    // {
    //     
    // }

    // public override void GenerateValidMoves(ChessBoard inBoard)
    // {
    //     // for pawn
    //     // check if destination is a valid distance first
    //     // check if dest is occupied, if yes, it blocks a pawn and is invalid
    //     // if destination is valid distance and not occupied, check if move
    //     //  places king in check
    //     // if not, then move is valid and add to list of valid moves
    //     
    //     // valid move for pawn is also one diagonal space if space is occupied
    //         // to capture
    //     // if all of the above is true and a piece is detected one space diagonally 
    //         // that pieces location is a valid move
    //     
    //     // for en passant, pawn can move diagonally to take a space if that space
    //     // was moved through by a pawn moving 2 spaces on first turn
    //     // if pawn is one square deep on opponents side and enemy pawn moves two
    //     // spaces to land directly horizontal to it, it can move diagonally and
    //     // "back capture" the pawn
    //     
    //     
    //     
    //     // set up values for pawn offset as pawns can only move one direction
    //     // if black, offset is positive (moving down the grid)
    //     // if white, offset is negative (moving up the grid)
    //     // if IsFirstMove == true, add second possible movement, the 2 space move
    //     // otherwise it is only one space move in the direction of the offset
    //     
    //     int offset = 0;
    //     if (Color == "white")
    //     {
    //         if (IsFirstMove)
    //         {
    //             ValidMoves.Add(new Space
    //                     (PiecePosition.Row + 1, PiecePosition.Col));
    //             ValidMoves.Add(new Space
    //                 (PiecePosition.Row + 2, PiecePosition.Col));
    //         }
    //         else
    //         {
    //             ValidMoves.Add(new Space
    //                 (PiecePosition.Row + 1, PiecePosition.Col));
    //         }
    //     }
    //     else if (Color == "black")
    //     {
    //         
    //     }
    //     ValidMoves.Add
    //     (new Space
    //         (PiecePosition.Row + offset, PiecePosition.Col));
    // }
}