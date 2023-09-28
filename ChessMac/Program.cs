
namespace ChessMac;
using static Methods;

// TODO: turn all Tuple<int, int> into ValueTuples<int row, int col>
internal static class Program
{
    static void Main()
    {
        ChessBoard board = new ChessBoard();
        board.OutputBoard();

        int moveCounter = 1;
        while (true)
        {
            if (moveCounter > 100)
            {
                Console.WriteLine("Move limit reached");
                return;
            }
            
            board.OutputBoard();
        
            var colorToMove = moveCounter % 2 != 0 ? PieceColor.White 
                                                   : PieceColor.Black;
            Console.WriteLine($"{colorToMove.ToString().ToUpper()} to move");
            Tuple<string, string> parsedInput = GetPlayerMove();
            
            moveCounter++;
        }
        
    }
}