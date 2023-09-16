using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace ChessMac;

// TODO: get rid of magic numbers

public static class Methods
{
    public static readonly Dictionary<string, char> BlackIcons = new()
    {
        { "PawnIcon", '\u2659' },
        { "RookIcon", '\u2656' },
        { "KnightIcon", '\u2658' },
        { "BishopIcon",'\u2657' },
        { "QueenIcon", '\u2655' },
        { "KingIcon", '\u2654' },
    };

    public static readonly Dictionary<string, char> WhiteIcons = new()
    {
        { "PawnIcon", '\u265F' },
        { "RookIcon", '\u265C' },
        { "KnightIcon", '\u265E' },
        { "BishopIcon", '\u265D' },
        { "QueenIcon", '\u265B' },
        { "KingIcon", '\u265A' },
    };

    public static readonly Dictionary<string, string> DefWhiteStart = new()
    {
        {"WhiteRook_16", "H1"},
        {"WhiteKnight_15", "G1"},
        {"WhiteBishop_14", "F1"},
        {"WhiteKing_13", "E1"},
        {"WhiteQueen_12", "D1"},
        {"WhiteBishop_11", "C1"},
        {"WhiteKnight_10", "B1"},
        {"WhiteRook_9", "A1"},
        {"WhitePawn_8", "H2"},
        {"WhitePawn_7", "G2"},
        {"WhitePawn_6", "F2"},
        {"WhitePawn_5", "E2"},
        {"WhitePawn_4", "D2"},
        {"WhitePawn_3", "C2"},
        {"WhitePawn_2", "B2"},
        {"WhitePawn_1", "A2"}
        
    };

    public static readonly Dictionary<string, string> DefBlackStart = new()
    {
        {"BlackRook_16", "H8"},
        {"BlackKnight_15", "G8"},
        {"BlackBishop_14", "F8"},
        {"BlackKing_13", "E8"},
        {"BlackQueen_12", "D8"},
        {"BlackBishop_11", "C8"},
        {"BlackKnight_10", "B8"},
        {"BlackRook_9", "A8"},
        {"BlackPawn_8", "H7"},
        {"BlackPawn_7", "G7"},
        {"BlackPawn_6", "F7"},
        {"BlackPawn_5", "E7"},
        {"BlackPawn_4", "D7"},
        {"BlackPawn_3", "C7"},
        {"BlackPawn_2", "B7"},
        {"BlackPawn_1", "A7"}
    };

    const char emptySpaceIcon = '\u2610';

    public static void InitPieces(Piece[] inPieces, Piece.PieceColor color)
    {
        inPieces[0] = Piece.CreatePiece(color, Piece.PieceType.Rook);
        inPieces[1] = Piece.CreatePiece(color, Piece.PieceType.Knight);
        inPieces[2] = Piece.CreatePiece(color, Piece.PieceType.Bishop);
        inPieces[3] = Piece.CreatePiece(color, Piece.PieceType.King);
        inPieces[4] = Piece.CreatePiece(color, Piece.PieceType.Queen);
        inPieces[5] = Piece.CreatePiece(color, Piece.PieceType.Bishop);
        inPieces[6] = Piece.CreatePiece(color, Piece.PieceType.Knight);
        inPieces[7] = Piece.CreatePiece(color, Piece.PieceType.Rook);
        for (int i = 8; i < 16; i++)
        {
            inPieces[i] = Piece.CreatePiece(color, Piece.PieceType.Pawn);
        }
        Piece.ResetPieceCounter();
    }
    
    // public static void InitializePieces(Piece[] inPieces, string color, Dictionary<string, char> inIcons)
    // {
    //     // pieces
    //     inPieces[0] = new Rook
    //         (color: color, 
    //         name: color + "Rook" + 1, 
    //         icon: inIcons["RookIcon"], 
    //         type: "rook");
    //     inPieces[1] = new Knight
    //     (color: color, 
    //         name: color + "Knight" + 1, 
    //         icon: inIcons["KnightIcon"], 
    //         type: "knight");
    //     inPieces[2] = new Bishop
    //         (color: color, 
    //         name: color + "Bishop" + 1, 
    //         icon: inIcons["BishopIcon"], 
    //         type: "bishop");
    //     inPieces[3] = new King
    //     (color: color, 
    //         name: color + "King", 
    //         icon: inIcons["KingIcon"], 
    //         type: "king");
    //     inPieces[4] = new Queen
    //     (color: color, 
    //         name: color + "Queen", 
    //         icon: inIcons["QueenIcon"], 
    //         type: "queen");
    //     inPieces[5] = new Bishop
    //         (color: color, 
    //         name: color + "Bishop" + 2, 
    //         icon: inIcons["BishopIcon"], 
    //         type: "bishop");
    //     inPieces[6] = new Knight
    //     (color: color, 
    //         name: color + "Knight" + 2, 
    //         icon: inIcons["KnightIcon"], 
    //         type: "knight");
    //     inPieces[7] = new Rook
    //     (color: color, 
    //         name: color + "Rook" + 2, 
    //         icon: inIcons["RookIcon"], 
    //         type: "rook");
    //     
    //     // pawns
    //     for (int i = 8; i < 16; i++)
    //     {
    //         inPieces[i] = new Pawn
    //         (color: color, 
    //             name: color + "Pawn" + (i - 7), 
    //             icon: inIcons["PawnIcon"], 
    //             type: "pawn");
    //     }
    // }

    public static void PlacePieces(Piece[] pieces, ChessBoard inBoard)
    {
        foreach (var piece in pieces)
        {
            string? name = piece.Name;
            Space? space = null;
            if (name is null) return;

            if (piece.Color == Piece.PieceColor.White)
                space = inBoard.GetSpace(DefWhiteStart[name]);
            if (piece.Color == Piece.PieceColor.Black)
                space = inBoard.GetSpace(DefBlackStart[name]);
            if (space is null)
            {
                return;
            }
            piece.PlacePiece(space, inBoard);
        }
    }

    public static string? GetPlayerInput()
    {
        while (true)
        {
            Console.Write(
                @"Move input must be in format:
                    'A4 A5'            
                > ");
            string? playerInput = Console.ReadLine();
            //Console.WriteLine(playerInput);
            if (playerInput?.Length != 5)
            {
                continue;
            }

            if (!playerInput.Contains(' '))
            {
                continue;
            }

            return playerInput;
        }
        
        return Console.ReadLine();
    }
    
    // TODO: check input w/ regex and known words instead
    public static Tuple<string, string> ParseInput(string? input)
    {
        string piece;
        string dest;

        if (input != null)
        {
            piece = input[0..2];
            dest = input[3..5];
        }
        //undetectable bug, nice
        else
        {
            Console.WriteLine("Error in input validation in: ParseInput()");
            return new Tuple<string, string>("A1", "A7");
        }
        return new Tuple<string, string>(piece, dest);
    }

    public static string ConvertIndexToPos(int inRow, int inCol)
    {
        string column = "";
        string row = "";

        column = inCol switch
        {
            0 => "A",
            1 => "B",
            2 => "C",
            3 => "D",
            4 => "E",
            5 => "F",
            6 => "G",
            7 => "H",
            _ => column
        };

        row = inRow switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => row
        };
        
        return column + row;
    }
    
    public static string ConvertIndexToPos(Space.Position inPosition)
    {
        string column = "";
        string row = "";

        column = inPosition.ColIndex switch
        {
            0 => "A",
            1 => "B",
            2 => "C",
            3 => "D",
            4 => "E",
            5 => "F",
            6 => "G",
            7 => "H",
            _ => column
        };

        row = inPosition.RowIndex switch
        {
            0 => "8",
            1 => "7",
            2 => "6",
            3 => "5",
            4 => "4",
            5 => "3",
            6 => "2",
            7 => "1",
            _ => row
        };
        
        return column + row;
    }
    
    public static Space.Position ConvertPosToIndex(string input)
    {
        var column = input[0];
        var row = input[1];

        Space.Position position = new Space.Position();
        position.ColIndex = column switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => position.ColIndex
        };

        position.RowIndex = row switch
        {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => position.RowIndex
        };

        return position;
    }

    public static void GeneratePieceMoves(Piece[] pieces, ChessBoard inBoard)
    {
        foreach (var piece in pieces)
        {
            piece.GenerateValidMoves(inBoard);
            piece.AddPieceToSpaceThreats();
        }
    }

    public static void PlayerMove(ChessBoard inBoard, Piece.PieceColor color)
    {
        while (true)
        {
            Console.WriteLine($"{color.ToString().ToUpper()} to move");
            string? playerInput = GetPlayerInput();
            Tuple<string, string> parsedInput = ParseInput(playerInput);
            Space.Position originalPosition = ConvertPosToIndex(parsedInput.Item1);
            Space.Position destinationPosition = ConvertPosToIndex(parsedInput.Item2);

            Piece? pieceBeingMoved = inBoard.GetPiece(originalPosition);
            Space destSpace = inBoard.GetSpace(destinationPosition);

            if (pieceBeingMoved is null)
            {
                Console.WriteLine("Piece is null");
                continue;
            }

            if (pieceBeingMoved.Color != color)
            {
                Console.WriteLine("That ain't your piece");
                continue;
            }

            if (pieceBeingMoved.IsMoveValid(destSpace))
            {
                pieceBeingMoved.MovePiece(destSpace, inBoard);
                return;
            }
            else
            {
                Console.WriteLine("move is not valid");
                
            }
        }
    }
}