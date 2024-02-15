using System.ComponentModel.Design;
using System.Runtime.InteropServices.JavaScript;
using ChessMac.Pieces.Base;
using ChessMac.Pieces.Children;

namespace ChessMac.Board;

using static Methods;

public class ChessBoard
{
    
    // initializes board with all spaces set to []
    // public ChessBoard()
    // {
    //     //InitBoardPieces();
    // }
    
    public Piece?[,] BoardPieces = new Piece[8, 8];
    public List<Piece> ActivePieces = new List<Piece>();
    public List<Piece> InactivePieces = new List<Piece>();
    
    private readonly int[] _rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };
    
    
    public ChessBoard DeepCopy()
    {
        var tempBoard = new ChessBoard();
        foreach (var piece in this.ActivePieces)
        {
            tempBoard.ActivePieces.Add(piece.Clone());
        }
        
        tempBoard.PopulateBoardPieces();
        return tempBoard;
    }

    public void InitializeActivePieces()
    {
        ActivePieces.Clear();
        
        // black pieces
        ActivePieces.Add( new Rook  (Piece.PieceColor.Black, (0, 0)));
        ActivePieces.Add( new Knight(Piece.PieceColor.Black, (0, 1)));
        ActivePieces.Add( new Bishop(Piece.PieceColor.Black, (0, 2)));
        ActivePieces.Add( new Queen (Piece.PieceColor.Black, (0, 3)));
        ActivePieces.Add( new King  (Piece.PieceColor.Black, (0, 4)));
        ActivePieces.Add( new Bishop(Piece.PieceColor.Black, (0, 5)));
        ActivePieces.Add( new Knight(Piece.PieceColor.Black, (0, 6)));
        ActivePieces.Add( new Rook  (Piece.PieceColor.Black, (0, 7)));
        
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 0)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 1)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 2)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 3)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 4)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 5)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 6)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.Black, (1, 7)));

        // white pieces
        ActivePieces.Add( new Rook  (Piece.PieceColor.White, (7, 0)));
        ActivePieces.Add( new Knight(Piece.PieceColor.White, (7, 1)));
        ActivePieces.Add( new Bishop(Piece.PieceColor.White, (7, 2)));
        ActivePieces.Add( new Queen (Piece.PieceColor.White, (7, 3)));
        ActivePieces.Add( new King  (Piece.PieceColor.White, (7, 4)));
        ActivePieces.Add( new Bishop(Piece.PieceColor.White, (7, 5)));
        ActivePieces.Add( new Knight(Piece.PieceColor.White, (7, 6)));
        ActivePieces.Add( new Rook  (Piece.PieceColor.White, (7, 7)));
        
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 0)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 1)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 2)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 3)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 4)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 5)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 6)));
        ActivePieces.Add( new Pawn(Piece.PieceColor.White, (6, 7)));
    }

    public void PopulateBoardPieces()
    {
        ClearBoardPieces();
        
        foreach (var piece in ActivePieces)
        {
            var position = piece.Position;
            BoardPieces[position.row, position.col] = piece;
        }
    }

    private void ClearBoardPieces()
    {
        for (var row = 0; row < BoardPieces.GetLength(0); row++)
        for (var col = 0; col < BoardPieces.GetLength(0); col++)
        {
            BoardPieces[row, col] = null;
        }
    }

    private void InactivatePiece(Piece? inPiece)
    {
        if (inPiece is null) return;
        if (ActivePieces.Contains(inPiece))
        {
            ActivePieces.Remove(inPiece);
            InactivePieces.Add(inPiece);
        }
    }

    // public void PlacePieces()
    // {
    //     // black pieces
    //     BoardPieces[0, 0] = new Rook(Piece.PieceColor.Black);
    //     BoardPieces[0, 1] = new Knight(Piece.PieceColor.Black);
    //     BoardPieces[0, 2] = new Bishop(Piece.PieceColor.Black);
    //     BoardPieces[0, 3] = new Queen(Piece.PieceColor.Black);
    //     BoardPieces[0, 4] = new King(Piece.PieceColor.Black);
    //     BoardPieces[0, 5] = new Bishop(Piece.PieceColor.Black);
    //     BoardPieces[0, 6] = new Knight(Piece.PieceColor.Black);
    //     BoardPieces[0, 7] = new Rook(Piece.PieceColor.Black);
    //
    //     BoardPieces[1, 0] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 1] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 2] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 3] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 4] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 5] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 6] = new Pawn(Piece.PieceColor.Black);
    //     BoardPieces[1, 7] = new Pawn(Piece.PieceColor.Black);
    //
    //     // white pieces
    //     BoardPieces[7, 0] = new Rook(Piece.PieceColor.White);
    //     BoardPieces[7, 1] = new Knight(Piece.PieceColor.White);
    //     BoardPieces[7, 2] = new Bishop(Piece.PieceColor.White);
    //     BoardPieces[7, 3] = new Queen(Piece.PieceColor.White);
    //     BoardPieces[7, 4] = new King(Piece.PieceColor.White);
    //     BoardPieces[7, 5] = new Bishop(Piece.PieceColor.White);
    //     BoardPieces[7, 6] = new Knight(Piece.PieceColor.White);
    //     BoardPieces[7, 7] = new Rook(Piece.PieceColor.White);
    //
    //     BoardPieces[6, 0] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 1] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 2] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 3] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 4] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 5] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 6] = new Pawn(Piece.PieceColor.White);
    //     BoardPieces[6, 7] = new Pawn(Piece.PieceColor.White);
    // }
    
    public void PlacePieces()
    {
        Piece.PieceColor[] colors = {
            Piece.PieceColor.Black, 
            Piece.PieceColor.White
        };
        Type[] pieceOrder = { 
            typeof(Rook), 
            typeof(Knight), 
            typeof(Bishop), 
            typeof(Queen), 
            typeof(King), 
            typeof(Bishop), 
            typeof(Knight), 
            typeof(Rook) 
        };
   
        foreach (var color in colors)
        {
            int pawnRow = (color == Piece.PieceColor.Black) ? 1 : 6;
            int pieceRow = (color == Piece.PieceColor.Black) ? 0 : 7;
   
            // Place pawns
            for (int col = 0; col < 8; col++)
            {
                BoardPieces[pawnRow, col] = new Pawn(color);
            }
   
            // Place other pieces based on the pieceOrder array
            for (int col = 0; col < 8; col++)
            {
                BoardPieces[pieceRow, col] = (Piece?)Activator.CreateInstance(pieceOrder[col], color);
            }
        }
    }


    public void ClearValidMoves()
    {
        foreach (var piece in ActivePieces)
        {
            piece.ClearValidMoves();
        }
    }

    public void GeneratePieceMoves()
    {
        foreach (var piece in ActivePieces)
        {
            piece.GenerateValidMoves(this);
        }
    }

    // TODO: change output logic to be based on a larger 2x2 array
    //  will simplify output and allow for easier modifications later
    
    // Output Board Display in ASCII to console
    public void OutputBoard()
    {
        List<(int row, int col)> piecePositions = new List<(int row, int col)>();
        foreach (var piece in ActivePieces)
        {
            var pos = piece.Position;
            piecePositions.Add(pos);
        }
        
        // Black side label
        Console.WriteLine("\t\t\t\t BLACK\n");
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");

        for (var row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(_rowNums[row] + @"     |" + "\t");

            for (var col = 0; col < 8; col++)
            {
                if (piecePositions.Contains((row, col)))
                {
                    Console.Write(BoardPieces[row, col]?.Icon);
                }
                else
                {
                    Console.Write(Piece.EmptySpaceIcon);
                }
                
                if (col < 7) Console.Write("\t");
                // skip to next line after reaching last item in row
                if (col == 7)
                {
                    // row number labels
                    Console.Write(@"  |" + "\t");
                    Console.Write(_rowNums[row]);
                    Console.WriteLine();
                    if (row < 7)
                    {
                        Console.Write(@"      |" + "\t\t\t\t\t\t\t\t" + @"   |");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(@"      ______________________________________________________________");
                    }
                }
            }
        }

        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH\n");
        // White side label
        Console.WriteLine("\t\t\t\t WHITE");
    }

    // not used switch method to convert H3 input to [2, 7] output
    public static (int row, int col) ConvertPosToIndices((char col, int row) position)
    {
        var colIndex = -1;
        var rowIndex = -1;
        colIndex = position.col switch
        {
            'A' => 0,
            'B' => 1,
            'C' => 2,
            'D' => 3,
            'E' => 4,
            'F' => 5,
            'G' => 6,
            'H' => 7,
            _ => colIndex
        };

        rowIndex = position.row switch
        {
            1 => 7,
            2 => 6,
            3 => 5,
            4 => 4,
            5 => 3,
            6 => 2,
            7 => 1,
            8 => 0,
            _ => rowIndex
        };

        return new ValueTuple<int, int>(rowIndex, colIndex);
    }

    public static bool IsWithinBoard((int row, int col) position)
    {
        return position.row is <= 7 and >= 0 && position.col is <= 7 and >= 0;
    }

    public void PlacePiece(Piece? inPiece, (int row, int col) position)
    {
        if (!IsWithinBoard(position)) throw new Exception("PlacePiece() arguments are not within bounds of ChessBoard");
        BoardPieces[position.row, position.col] = inPiece;
    }

    public void MovePiece((int row, int col) startPos, (int row, int col) destPos, out Piece? takenPiece)
    {
        takenPiece = GetPieceByIndex(destPos);
        PlacePiece(GetPieceByIndex(startPos), destPos);
        PlacePiece(null, startPos);
    }
    
    // should receive non-null starting position
    public bool ValidateAndMovePiece(Piece.PieceColor colorToMove, (int row, int col) startPos, (int row, int col) destPos)
    {
        Piece? pieceBeingMoved = GetPieceByIndex(startPos);
        if (pieceBeingMoved is null)
            return false;
        bool isMoveValid = pieceBeingMoved.IsColorToMove(colorToMove) && pieceBeingMoved.HasMove(destPos);
        if (isMoveValid == false)
        {
            // Console.WriteLine("Destination is not a valid move");
            return false;
        }

        Piece? takenPiece;
        MovePiece(startPos, destPos, out takenPiece);

        if (pieceBeingMoved?.Type == Piece.PieceType.Pawn)
            CheckAndPromotePawn(pieceBeingMoved, this, destPos.row);

        ClearThreats();
        GeneratePieceMoves();
        AddThreats();

        //TODO king in check validation
        // if (IsKingInCheck(isMoveValid.Color))
        // {
        //     Console.WriteLine("Your king is in check!");
        //     return false;
        // }

        return true;
    }

    public void AddThreats()
    {
        for (var row = 0; row < BoardPieces.GetLength(0); row++)
        {
            for (var col = 0; col < BoardPieces.GetLength(0); col++)
            {
                var piece = BoardPieces[row, col];
                if (piece is null) continue;
                foreach (var move in piece.GetValidMoveList())
                    GetPieceByIndex(move)?.SetThreat();
            }
        }
    }

    public void ClearThreats()
    {
        foreach (var piece in BoardPieces) piece?.ClearThreat();
    }

    // public bool IsKingInCheck(PieceColor inColor)
    // {
    //     if (inColor == PieceColor.Black)
    //         return BoardPieces[blackKingPos.row, blackKingPos.col]!.IsThreatened;
    //     return BoardPieces[whiteKingPos.row, whiteKingPos.col]!.IsThreatened;
    // }

    public Piece? GetPieceByIndex((int row, int col) inIndex)
    {
        try
        {
            return BoardPieces[inIndex.row, inIndex.col];
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Value provided is not within the bounds of the chessboard. Returning null.");
            return null;
        }
    }
}