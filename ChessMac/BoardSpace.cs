namespace ChessMac;

public class BoardSpace
{
    private int? rowIndex = null;
    private int? colIndex = null;

    private int? rowNum = null;
    private int? colChar = null;

    private bool? isOccupied = false;

    public void setSpaceState(bool state)
    {
        isOccupied = state;
    }
    
}