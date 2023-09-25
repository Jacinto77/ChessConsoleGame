
namespace ChessMac;
using static Methods;


// TODO: make chessboard a static object
// TODO: make pieces part of the board


internal static class Program
{
    static void Main()
    {

        // ChessBoard board = new ChessBoard();
        // foreach (var space in board.BoardSpaces)
        // {
        //     Console.WriteLine(space.HasPiece);
        //     Console.WriteLine(space.Icon);
        //     Console.WriteLine(space.Piece?.Name);
        //     Console.WriteLine(space.Piece?.Pos);
        //     Console.WriteLine(space.SpaceName);
        //     Console.WriteLine($"Row: {space.Piece?.RowIndex} Col: {space.Piece?.ColIndex}");
        // }
        //
        // board.OutputBoard();
        // GeneratePieceMoves(board.WhitePieces, board);
        // GeneratePieceMoves(board.BlackPieces, board);
        // foreach (var piece in board.WhitePieces)
        // {
        //     Console.WriteLine("--------");
        //     if (piece is null)
        //     {
        //         Console.WriteLine("piece is null in board");
        //     }
        //     Console.WriteLine(piece?.Name);
        //     Console.WriteLine($"Piece has moved? : {piece?.HasMoved}");
        //     piece?.PrintValidMoves();
        // }
        //
        // Console.WriteLine("----TEMP BOARD----");
        // ChessBoard tempBoard = board.DeepCopy();
        // foreach (var space in tempBoard.BoardSpaces)
        // {
        //     Console.WriteLine(space.HasPiece);
        //     Console.WriteLine(space.Icon);
        //     Console.WriteLine(space.Piece?.Name);
        //     Console.WriteLine(space.Piece?.Pos);
        //     Console.WriteLine(space.SpaceName);
        //     Console.WriteLine($"Row: {space.Piece?.RowIndex} Col: {space.Piece?.ColIndex}");
        // }
        //
        // foreach (var piece in tempBoard.WhitePieces)
        // {
        //     Console.WriteLine("--------");
        //     if (piece is null)
        //     {
        //         Console.WriteLine("piece is null in board");
        //     }
        //     else
        //     {
        //         Console.WriteLine(piece?.Name);
        //
        //         Console.WriteLine($"Piece has moved? : {piece?.HasMoved}");
        //         Console.WriteLine($"{piece?.RowIndex} {piece?.ColIndex}");
        //         piece?.PrintValidMoves();
        //     }
        // }
        //
        // int moveCounter = 1;
        // board.OutputBoard();
        //
        // var colorToMove = moveCounter % 2 != 0 ? Piece.PieceColor.White 
        //                                        : Piece.PieceColor.Black;
        // Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
        // Tuple<string, string> parsedInput = GetPlayerMove();
        //
        // if (IsValidMove(tempBoard, colorToMove, parsedInput, tempBoard.WhitePieces, tempBoard.BlackPieces))
        // {
        //     // playermove() makes the pieces disappear
        //     // either something about the copying
        //     // or my movePiece functions are too convoluted
        //     board = tempBoard.DeepCopy();
        //     board.OutputBoard();
        // }
        // else
        // {
        //     Console.WriteLine("Move not vaaaaalid, dude");
        // }
        //
        //
        // moveCounter++;
        
        ChessBoard board = new ChessBoard();
        // board.OutputBoard();
        
        
        
        
        // :testing:
        // Piece? testPiece = board.GetPiece(new Space.Position(6, 5));
        // board.GetSpace(5, 4).PlacePiece(new Pawn(Piece.PieceColor.Black, Piece.PieceType.Pawn));
        // testPiece.GenerateValidMoves(board);
        // testPiece.PrintValidMoves();
        // board.OutputBoard();
        
        int moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }
        
            GeneratePieceMoves(board.WhitePieces, board);
            GeneratePieceMoves(board.BlackPieces, board);
            // foreach (var piece in board.WhitePieces)
            // {
            //     if (piece is null)
            //     {
            //         Console.WriteLine("piece is null in board");
            //     }
            //     Console.WriteLine(piece?.Name);
            //     piece?.PrintValidMoves();
            // }
            
            // AssignThreatsToSpaces(board.WhitePieces, board);
            // AssignThreatsToSpaces(board.BlackPieces, board);
        
            ChessBoard tempBoard = board.DeepCopy();
            // tempBoard.OutputBoard();
            
            // foreach (var piece in tempBoard.WhitePieces)
            // {
            //     if (piece is null)
            //     {
            //         Console.WriteLine("piece is null in tempBoard");
            //     }
            //     Console.WriteLine(piece?.Name);
            //     piece?.PrintValidMoves();
            // }
            
            board.OutputBoard();
        
            var colorToMove = moveCounter % 2 != 0 ? Piece.PieceColor.White 
                                                   : Piece.PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            Tuple<string, string> parsedInput = GetPlayerMove();
        
            // TODO currently, a pawn taking another pawn results in the pawn icon staying the same color
            // TODO, piece taking not quite working yet
            if (IsValidMove(tempBoard, colorToMove, parsedInput, tempBoard.WhitePieces, tempBoard.BlackPieces))
            {
                Piece? pieceBeingMoved = board.GetPiece(ConvertPosToIndex(parsedInput.Item1));
                if (pieceBeingMoved is null) throw new Exception("Program.Main() pieceBeingMoved is null");
                else
                {
                    Tuple<int, int> startPos = ConvertPosToIndex(parsedInput.Item1);
                    Tuple<int, int> destPos = ConvertPosToIndex(parsedInput.Item2);
                    pieceBeingMoved.MovePiece(board, destPos);    
                    
                    board.GetSpace(startPos)?.ClearPieceInfo();
                        
                    if (pieceBeingMoved.Type == Piece.PieceType.Pawn)
                        CheckAndPromotePawn(pieceBeingMoved, board);
                }
                
            }
            else
            {
                continue;
            }
            
            moveCounter++;
        }
        
    }
}



// have pinned pieces identified as well as which direction
// they're blocking from so that if that piece tries to move out of that
// direction they are disallowed

// maybe alter the piece generation for rook and bishop and queen to see
// all possible moves as if there was no board

// check for if there is a piece in between that possible moves and the king
// use the direction that the piece was found in to mark it as pinned
// and record it's pinned direction to restrict it only to moves along that
// direction ie. not out of pin

// place pieces
// generate moves
// move piece
//  check if move places own king in check
//    if yes
//      

    