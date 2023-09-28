using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ChessMac;

// TODO: get rid of magic numbers

public static class Methods
{
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
    
    public static (int row, int col) ConvertPosToIndex(string input)
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
        
        return (tempRow, tempCol);
    }

    public static Tuple<string, string> GetPlayerMove()
    {
        string playerInput = GetPlayerInput();
        return ParseInput(playerInput);
    }

    public static void CheckAndPromotePawn(Piece pieceBeingMoved, ChessBoard inBoard, int currentRow)
    {
        if (!pieceBeingMoved.IsPawnPromotionSpace(currentRow))
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

    

    public static bool IsKingInCheck(Piece pieceBeingMoved, ChessBoard tempBoard, 
        Piece?[,] inBoardPieces)
    {
        // if (!blackKing.IsActive)
        //     throw new Exception("Methods.IsKingInCheck() black king piece is null");
        // if (!whiteKing.IsActive)
        //     throw new Exception("Methods.IsKingInCheck() black king piece is null");
        //
        // Tuple<int, int> whiteKingPos = new Tuple<int, int>(whiteKing.RowIndex, whiteKing.ColIndex);
        // Tuple<int, int> blackKingPos = new Tuple<int, int>(blackKing.RowIndex, blackKing.ColIndex);
        //
        // return pieceBeingMoved.Color switch
        // {
        //     Piece.PieceColor.White when tempBoard.GetSpace(blackKingPos).IsThreatened => true,
        //     Piece.PieceColor.Black when tempBoard.GetSpace(whiteKingPos).IsThreatened => true,
        //     _ => false
        // };

        return false;
    }
}