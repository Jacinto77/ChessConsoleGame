using System.Runtime.CompilerServices;
using static ChessMac.Piece;


namespace ChessMac;
using static Methods;
// creation of chessboard
// outputs chess board display
// modifies itself based on moves made
// outputs information about the state of the board
// holds all 64 Spaces

public enum PieceColor
{
    White, 
    Black,
}

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public class ChessBoard
{
    // initializes board with all spaces set to []
    public ChessBoard()
    {
        InitBoardPieces();
        PlacePieces();
    }

    public Piece?[,] BoardPieces = new Piece[8, 8];
    private const char EmptySpaceIcon = '\u2610';
    
    public ChessBoard DeepCopy()
    {
        return new ChessBoard();
    }

    public void InitBoardPieces()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                BoardPieces[row, col] = new Piece();
            }
        }
    }

    public void PlacePieces()
    {
        BoardPieces[0, 0] = new Rook(PieceColor.Black);
        BoardPieces[0, 1] = new Knight(PieceColor.Black);
        BoardPieces[0, 2] = new Bishop(PieceColor.Black);
        BoardPieces[0, 3] = new Queen(PieceColor.Black);
        BoardPieces[0, 4] = new King(PieceColor.Black);
        BoardPieces[0, 5] = new Bishop(PieceColor.Black);
        BoardPieces[0, 6] = new Knight(PieceColor.Black);
        BoardPieces[0, 7] = new Rook(PieceColor.Black);
        
        BoardPieces[1, 0] = new Pawn(PieceColor.Black);
        BoardPieces[1, 1] = new Pawn(PieceColor.Black);
        BoardPieces[1, 2] = new Pawn(PieceColor.Black);
        BoardPieces[1, 3] = new Pawn(PieceColor.Black);
        BoardPieces[1, 4] = new Pawn(PieceColor.Black);
        BoardPieces[1, 5] = new Pawn(PieceColor.Black);
        BoardPieces[1, 6] = new Pawn(PieceColor.Black);
        BoardPieces[1, 7] = new Pawn(PieceColor.Black);
        
        BoardPieces[7, 0] = new Rook(PieceColor.White);
        BoardPieces[7, 1] = new Knight(PieceColor.White);
        BoardPieces[7, 2] = new Bishop(PieceColor.White);
        BoardPieces[7, 3] = new Queen(PieceColor.White);
        BoardPieces[7, 4] = new King(PieceColor.White);
        BoardPieces[7, 5] = new Bishop(PieceColor.White);
        BoardPieces[7, 6] = new Knight(PieceColor.White);
        BoardPieces[7, 7] = new Rook(PieceColor.White);
        
        BoardPieces[6, 0] = new Pawn(PieceColor.White);
        BoardPieces[6, 1] = new Pawn(PieceColor.White);
        BoardPieces[6, 2] = new Pawn(PieceColor.White);
        BoardPieces[6, 3] = new Pawn(PieceColor.White);
        BoardPieces[6, 4] = new Pawn(PieceColor.White);
        BoardPieces[6, 5] = new Pawn(PieceColor.White);
        BoardPieces[6, 6] = new Pawn(PieceColor.White);
        BoardPieces[6, 7] = new Pawn(PieceColor.White);
    }
    
    public void SetInitialPiecePositions()
    {
        
    }
    
    public void GeneratePieceMoves()
    {
        for (int row = 0; row < BoardPieces.Length; row++)
        {
            for (int col = 0; col < BoardPieces.Length; col++)
            {
                if (BoardPieces[row, col] is null) continue;
                BoardPieces[row, col]!.GenerateValidMoves(this, row, col);
            }
            
        }
    }
    
    // Output Board Display in ASCII to console
    public void OutputBoard()
    {
        // Black side label
        Console.WriteLine("\t\t\t\t BLACK\n");
        // column letter labels
        Console.WriteLine("\tA\tB\tC\tD\tE\tF\tG\tH");
        Console.WriteLine(@"      ______________________________________________________________");
        
        for (int row = 0; row < 8; row++)
        {
            // row number labels
            Console.Write(rowNums[row] + @"     |" + "\t");
            
            for (int col = 0; col < 8; col++)
            {
                if (BoardPieces[row, col] is not null)
                {
                    Console.Write(BoardPieces[row, col]!.Icon);
                }
                else Console.Write(EmptySpaceIcon);
                // if (BoardSpaces[row, col].Piece == null)
                //     Console.Write(BoardSpaces[row, col].Icon);
                // else
                //     Console.Write(BoardSpaces[row, col].Piece.Icon);
                
                if (col < 7) Console.Write("\t");
                
                // skip to next line after reaching last item in row
                if (col == 7)
                {
                    // row number labels
                    Console.Write(@"  |" + "\t");
                    Console.Write(rowNums[row]);
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

    // array of numbers 1-8 to be used by OutputBoard()
    private int[] rowNums =
    {
        8, 7, 6, 5, 4, 3, 2, 1
    };

    // not used switch method to convert H3 input to [2, 7] output
    public Tuple<int, int> ConvertPosToIndices(Tuple<char, int> position)
    {
        int colIndex = -1;
        int rowIndex = -1;
        colIndex = position.Item1 switch
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

        rowIndex = position.Item2 switch
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

        return new Tuple<int, int>(colIndex, rowIndex);
    }

    public static bool IsWithinBoard(int currentRow, int currentCol)
    {
        return (currentCol is <= 7 and >= 0) && (currentRow is <= 7 and >= 0);
    }

    public void MovePiece(Piece inPiece, (int row, int col) startPos, 
        (int row, int col) destPos)
    {
        this.BoardPieces[destPos.row, destPos.col] = inPiece;
        this.BoardPieces[startPos.row, startPos.col] = null;

    }
    
    public static Piece? InitialMoveValidation(Piece? activePiece, PieceColor colorToMove, Tuple<int, int> destPos)
    {
        if (activePiece is null)
        {
            Console.WriteLine("Piece is null");
            return null;
        }
        if (activePiece.Color != colorToMove)
        {
            Console.WriteLine("That ain't your piece");
            return null;
        }
        if (!activePiece.IsMoveValid(destPos))
        {
            Console.WriteLine("move is not valid");
            return null;
        }

        return activePiece;
    }
    
    public bool IsValidMove(ChessBoard tempBoard, PieceColor colorToMove, 
        Tuple<string, string> playerMove)
    {
        Tuple<int, int> startPos = ConvertPosToIndex(playerMove.Item1);
        Tuple<int, int> destPos = ConvertPosToIndex(playerMove.Item2);

        Piece? pieceBeingMoved = tempBoard.BoardPieces[startPos.Item1, startPos.Item2];
        Piece? destSpace = tempBoard.BoardPieces[destPos.Item1, destPos.Item2];

        Piece? testedMove = InitialMoveValidation(pieceBeingMoved, colorToMove, destPos);
        if (testedMove is null)
        {
            Console.WriteLine();
            return false;
        }
        if (destSpace is not null)
        {
            // pieceBeingMoved.TakenPieces.Add(destSpace);
        }
        
        MovePiece(testedMove, (row: startPos.Item1, col: startPos.Item2), 
                              (row: destPos.Item1, col: destPos.Item2));

        if (testedMove.Type == PieceType.Pawn)
            CheckAndPromotePawn(testedMove, this, destPos.Item1);
        
        GeneratePieceMoves();
        

        if (!IsKingInCheck(testedMove, tempBoard, BoardPieces)) 
            return true;
        Console.WriteLine("Your king is in check!");
        return false;

    }
}