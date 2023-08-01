
namespace ChessMac;

// Program
// Create instance of board
// Create all pieces
// game flow control

internal static class Program
{
    static Dictionary<string, char> blackIcons = new()
    {
        { "PawnIcon", '\u2659' },
        { "RookIcon", '\u2656' },
        { "KnightIcon", '\u2658' },
        { "BishopIcon",'\u2657' },
        { "QueenIcon", '\u2655' },
        { "KingIcon", '\u2654' },
    };

    static Dictionary<string, char> whiteIcons = new()
    {
        { "PawnIcon", '\u265F' },
        { "RookIcon", '\u265C' },
        { "KnightIcon", '\u265E' },
        { "BishopIcon", '\u265D' },
        { "QueenIcon", '\u265B' },
        { "KingIcon", '\u265A' },
    };
    
    const char emptySpaceIcon = '\u2610';
    
    public static void InitializePieces(Piece[] inPieces, string color, Dictionary<string, char> inIcons)
    {
        // pieces
        inPieces[0] = new Rook
            (color: color, 
            name: color + "Rook" + 1, 
            icon: inIcons["RookIcon"], 
            type: "rook");
        inPieces[1] = new Knight
        (color: color, 
            name: color + "Knight" + 1, 
            icon: inIcons["KnightIcon"], 
            type: "knight");
        inPieces[2] = new Bishop
            (color: color, 
            name: color + "Bishop" + 1, 
            icon: inIcons["BishopIcon"], 
            type: "bishop");
        inPieces[3] = new King
        (color: color, 
            name: color + "King", 
            icon: inIcons["KingIcon"], 
            type: "king");
        inPieces[4] = new Queen
        (color: color, 
            name: color + "Queen", 
            icon: inIcons["QueenIcon"], 
            type: "queen");
        inPieces[5] = new Bishop
            (color: color, 
            name: color + "Bishop" + 2, 
            icon: inIcons["BishopIcon"], 
            type: "bishop");
        inPieces[6] = new Knight
        (color: color, 
            name: color + "Knight" + 2, 
            icon: inIcons["KnightIcon"], 
            type: "knight");
        inPieces[7] = new Rook
        (color: color, 
            name: color + "Rook" + 2, 
            icon: inIcons["RookIcon"], 
            type: "rook");
        
        // pawns
        for (int i = 8; i < 16; i++)
        {
            inPieces[i] = new Pawn
            (color: color, 
                name: color + "Pawn" + (i - 7), 
                icon: inIcons["PawnIcon"], 
                inType: "pawn");
        }
    }

    static void PlacePieces
        (Piece[] whitePieces, Piece[] blackPieces, ChessBoard inBoard)
    {
        int whitePieceCounter = 0;
        int blackPieceCounter = 0;
        
        // black pieces
        for (int row = 0; row < 2; row++)
        {
            for (int col = 7; col > -1; col--)
            {
                inBoard.BoardSpaces[row, col].Piece = 
                    blackPieces[blackPieceCounter];
                blackPieceCounter++;
            }
        }
        
        // white pieces
        for (int row = 7; row > 5; row--)
        {
            for (int col = 7; col > -1; col--)
            {
                inBoard.BoardSpaces[row, col].Piece = 
                    whitePieces[whitePieceCounter];
                whitePieceCounter++;
            }
        }

        for (int i = 0; i < blackPieces.Length; i++)
        {
            blackPieces[i].GenerateValidMoves();
        }

        for (int i = 0; i < whitePieces.Length; i++)
        {
            whitePieces[i].GenerateValidMoves();
        }
    }

    // TODO finish this function and test
    // needs to reset the space's Piece reference to default icon and 
    // set destPosition space to inputPosition space.Piece so that the
    // icon is updated and piece is now referenced by a new position
    static void MovePiece(ChessBoard inBoard)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input != null)
            {
                Tuple<string, string> positions = ParseInput(input);
                Space.Position inputPosition = ConvertPosToIndices(positions.Item1);
                Space.Position destPosition = ConvertPosToIndices(positions.Item2);

                inBoard.BoardSpaces[destPosition.Row, destPosition.Col].Piece =
                    inBoard.BoardSpaces[inputPosition.Row, inputPosition.Col].Piece;

                inBoard.BoardSpaces[inputPosition.Row, inputPosition.Col].Piece =
                    null;
                return;
            }
            else
            {
                Console.WriteLine("invalid input");
            }
        }
    }

    static Tuple<string, string> ParseInput(string input)
    {
        string piece;
        string dest;

        if (input != null)
        {
            piece = input[0..2];
            dest = input[3..5];
        }
        //undetectable bug, nice
        else return new Tuple<string, string>("A1", "A8");
        return new Tuple<string, string>(piece, dest);
    }
    
    static Space.Position ConvertPosToIndices(string input)
    {
        var column = input[0];
        var row = input[1];

        Space.Position position = new Space.Position();
        position.Col = column switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => position.Col
        };

        position.Row = row switch
        {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => position.Row
        };

        return position;
    }
    
    static void Main()
    {
        // create chessboard
        ChessBoard board = new ChessBoard();

        board.OutputBoard();
        
        // generate pieces and initialize them
        Piece[] whitePieces = new Piece[16];
        Piece[] blackPieces = new Piece[16];
        InitializePieces(inPieces: whitePieces, color: "white", inIcons: whiteIcons);
        InitializePieces(inPieces: blackPieces, color: "black", inIcons: blackIcons);
        
        PlacePieces(whitePieces, blackPieces, board);
        
        board.OutputBoard();

        string colorToMove = "white";
        // while (true)
        // {
        //     // prompt color to move
        //     // get move input
        //     // validate that it is a proper move
        //         // is proper color?
        //         // is destination move in list of valid moves for source piece
        //         // 
        // }
        
        
        for (int i = 0; i < whitePieces.Length; i++)
        {
            Console.WriteLine(whitePieces[i].Name + whitePieces[i].Icon);
        }
        
        for (int i = 0; i < blackPieces.Length; i++)
        {
            Console.WriteLine(blackPieces[i].Name + blackPieces[i].Icon);
        }
        
        
        board.BoardSpaces[6, 2].Piece.PrintValidMoves();
        MovePiece(board);
        board.OutputBoard();
        
        
        /*
        while (true)
        {
            // prompt to start
            
            // generate board and pieces
            
            // create array of valid moves for each piece
                // methods for each piece with the rules for valid moves

                while (true)
            {
                // output board
                
                // prompt for <COLOR> move
                    // get move from console in format <char, int>
                // validate move
                    // check if input is valid (char: A-H int: 1-8)
                    // convert input to indices of board array
                    // check position of input
                        // if it has a piece, check if proper color
                        // if it is a valid piece and the proper color, check for destination
                    // validate that destination is within bounds of board, and
                            // complies with piece's allowed moves
                    // check that the move doesn't place self in check
                    // check that the move doesn't move through other pieces
                // if move is to empty space
                    // update previous space to be empty
                    // update new space with piece
                
                // if move takes a piece
                    // update previous space to be empty
                    // update new space with piece and color
                    // remove taken piece from list of active pieces
                        // to be used for calculating score
                        
                // if move places other king in check, set isInCheck = true
                    // restricts black to only moves that makes isInCheck = false
                        
                // check for check and checkmate
                    // King piece has property isInCheck
                    // has method to check for checkmate
                        // checkmate is any board position where the king is in the line of fire
                        // and no valid moves will take the king out of check
                            // block check: check path of attacking piece, if a piece has a valid
                                // move that == one of the spaces in the attacking pieces path
                                // checkmate = false
                            // take piece: check valid moves from pieces and if a piece has a valid
                                // move to take the attacking piece, checkmate = false
                            // move king: check valid moves from King, if king can move to position
                                // that is not in check, checkmate = false
                        // if blockCheck, takePiece, and moveKing all == false, then checkMate=true
                    
                // regenerate arrays of valid moves
                    // for any piece, first check current array vs current state of board
                        // if a piece's valid moves now has a piece occupying any of the previous
                            // spaces, regenerate moves
                    // if a piece's valid moves/path/spaces have not been changed, do not regenerate
                    
            }
            
        }
        */
    }
}



    