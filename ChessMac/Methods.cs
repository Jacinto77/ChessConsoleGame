using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    public static readonly Dictionary<string, Tuple<int, int>> DefWhiteStart = new()
    {
        {"WhiteRook_16",   new Tuple<int, int>(7, 7)},
        {"WhiteKnight_15", new Tuple<int, int>(7, 6)},
        {"WhiteBishop_14", new Tuple<int, int>(7, 5)},
        {"WhiteKing_13",   new Tuple<int, int>(7, 4)},
        {"WhiteQueen_12",  new Tuple<int, int>(7, 3)},
        {"WhiteBishop_11", new Tuple<int, int>(7, 2)},
        {"WhiteKnight_10", new Tuple<int, int>(7, 1)},
        {"WhiteRook_9",    new Tuple<int, int>(7, 0)},
        
        {"WhitePawn_8", new Tuple<int, int>(6, 7)},
        {"WhitePawn_7", new Tuple<int, int>(6, 6)},
        {"WhitePawn_6", new Tuple<int, int>(6, 5)},
        {"WhitePawn_5", new Tuple<int, int>(6, 4)},
        {"WhitePawn_4", new Tuple<int, int>(6, 3)},
        {"WhitePawn_3", new Tuple<int, int>(6, 2)},
        {"WhitePawn_2", new Tuple<int, int>(6, 1)},
        {"WhitePawn_1", new Tuple<int, int>(6, 0)}
        
    };

    public static readonly Dictionary<string, Tuple<int, int>> DefBlackStart = new()
    {
        {"BlackRook_16",   new Tuple<int, int>(0, 7)},
        {"BlackKnight_15", new Tuple<int, int>(0, 6)},
        {"BlackBishop_14", new Tuple<int, int>(0, 5)},
        {"BlackKing_13",   new Tuple<int, int>(0, 4)},
        {"BlackQueen_12",  new Tuple<int, int>(0, 3)},
        {"BlackBishop_11", new Tuple<int, int>(0, 2)},
        {"BlackKnight_10", new Tuple<int, int>(0, 1)},
        {"BlackRook_9",    new Tuple<int, int>(0, 0)},
        
        {"BlackPawn_8", new Tuple<int, int>(1, 7)},
        {"BlackPawn_7", new Tuple<int, int>(1, 6)},
        {"BlackPawn_6", new Tuple<int, int>(1, 5)},
        {"BlackPawn_5", new Tuple<int, int>(1, 4)},
        {"BlackPawn_4", new Tuple<int, int>(1, 3)},
        {"BlackPawn_3", new Tuple<int, int>(1, 2)},
        {"BlackPawn_2", new Tuple<int, int>(1, 1)},
        {"BlackPawn_1", new Tuple<int, int>(1, 0)}
    };

    const char emptySpaceIcon = '\u2610';

    public static void InitPieces(Piece?[] inPieces, Piece.PieceColor color)
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

    public static void PlacePieces(Piece?[] pieces, ChessBoard inBoard)
    {
        Console.WriteLine("PlacePieces()");
        foreach (var piece in pieces)
        {
            string? name = piece?.Name;
            Tuple<int, int> defSpace = new Tuple<int, int>(-1, -1);
            if (name is null) throw new Exception("Methods.PlacePieces(), name is null");
            
            Console.WriteLine(piece?.Name);
            Console.WriteLine(piece?.Color);
            // correlate default starting spot with color and piece name
            defSpace = piece?.Color switch
            {
                Piece.PieceColor.White => DefWhiteStart[name],
                Piece.PieceColor.Black => DefBlackStart[name],
                _ => defSpace
            };
            // place the piece and set piece and space information
            Console.WriteLine($"Row: {defSpace.Item1} Col: {defSpace.Item2}\n");
            
            piece?.PlacePiece(inBoard, defSpace);
        }
    }

    public static string GetPlayerInput()
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
        //TODO undetectable bug, nice
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
        return column + row;
    }
    
    public static string ConvertIndexToPos(Tuple<int, int> inIndex)
    {
        string row = "";
        string column = "";
        row = inIndex.Item1 switch
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
        column = inIndex.Item2 switch
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
        return column + row;
    }
    
    public static Tuple<int, int> ConvertPosToIndex(string input)
    {
        var columnChar = input[0];
        var rowChar = input[1];

        int tempCol = -1;
        int tempRow = -1;
        tempCol = columnChar switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => tempCol
        };

        tempRow = rowChar switch
        {
            '1' => 7,
            '2' => 6,
            '3' => 5,
            '4' => 4,
            '5' => 3,
            '6' => 2,
            '7' => 1,
            '8' => 0,
            _ => tempRow
        };
        
        return new Tuple<int, int>(tempRow, tempCol);
    }

    public static void GeneratePieceMoves(Piece?[] pieces, ChessBoard inBoard)
    {
        foreach (var piece in pieces)
        {
            if (piece is null) throw new Exception("GeneratePieceMoves() piece is null");
            
            piece.GenerateValidMoves(inBoard);
        }
    }

    public static Tuple<string, string> GetPlayerMove()
    {
        string? playerInput = GetPlayerInput();
        return ParseInput(playerInput);
    }
    

    public static void PlacePiece(Piece piece, ChessBoard inBoard, Tuple<int, int> inMove)
    {
        inBoard.GetSpace(inMove).SetPieceInfo(piece);
        piece.IsActive = true;
    }

    public static void CheckAndPromotePawn(Piece pieceBeingMoved, ChessBoard inBoard)
    {
        if (!pieceBeingMoved.IsPawnPromotionSpace())
            return;
        pieceBeingMoved.PromotePawn(inBoard);
    }
    
    public static int? GetPlayerTypeInt()
    {
        while (true)
        {
            int choice = Console.Read();
            if (choice is <= 4 and >= 1) return choice;
            
            Console.WriteLine("Input must be 1-4");
        }
    }

    public static bool IsValidMove(ChessBoard tempBoard, Piece.PieceColor colorToMove, 
        Tuple<string, string> playerMove, Piece?[] whitePieces, Piece?[] blackPieces)
    {
        Tuple<int, int> startPos = ConvertPosToIndex(playerMove.Item1);
        Tuple<int, int> destPos = ConvertPosToIndex(playerMove.Item2);

        Piece? pieceBeingMoved = tempBoard.GetPiece(startPos);
        Space? destSpace = tempBoard.GetSpace(destPos);

        if (pieceBeingMoved is null)
        {
            Console.WriteLine("Piece is null");
            return false;
        }
        if (pieceBeingMoved.Color != colorToMove)
        {
            Console.WriteLine("That ain't your piece");
            return false;
        }
        if (!pieceBeingMoved.IsMoveValid(destPos))
        {
            Console.WriteLine("move is not valid");
            return false;
        }
        
        Piece? destPiece;
        if (destSpace.HasPiece)
            destPiece = destSpace.Piece;
        
        pieceBeingMoved.MovePiece(tempBoard, destPos);
        
        tempBoard.GetSpace(startPos)?.ClearPieceInfo();
                        
        if (pieceBeingMoved.Type == Piece.PieceType.Pawn)
            CheckAndPromotePawn(pieceBeingMoved, tempBoard);
        
        GeneratePieceMoves(whitePieces, tempBoard);
        GeneratePieceMoves(blackPieces, tempBoard);

        if (!IsKingInCheck(pieceBeingMoved, tempBoard, whitePieces[3], blackPieces[3])) 
            return true;
        Console.WriteLine("Your king is in check!");
        return false;

    }

    public static bool IsKingInCheck(Piece pieceBeingMoved, ChessBoard tempBoard, 
        Piece whiteKing, Piece blackKing)
    {
        if (!blackKing.IsActive)
            throw new Exception("Methods.IsKingInCheck() black king piece is null");
        if (!whiteKing.IsActive)
            throw new Exception("Methods.IsKingInCheck() black king piece is null");

        Tuple<int, int> whiteKingPos = new Tuple<int, int>(whiteKing.RowIndex, whiteKing.ColIndex);
        Tuple<int, int> blackKingPos = new Tuple<int, int>(blackKing.RowIndex, blackKing.ColIndex);

        return pieceBeingMoved.Color switch
        {
            Piece.PieceColor.White when tempBoard.GetSpace(blackKingPos).IsThreatened => true,
            Piece.PieceColor.Black when tempBoard.GetSpace(whiteKingPos).IsThreatened => true,
            _ => false
        };
    }
}