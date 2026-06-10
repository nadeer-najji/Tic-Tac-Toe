using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSquareManager[] _grid;

    public void ResetGrid()
    {
        for(int i=0;i<_grid.Length;i++)
        {
            _grid[i].SetSquare(GridSquareState.empty, Color.white);
            _grid[i].SetSquareId(i);
        }
    }

    public void SetSpecificSquare(GridSquareState gridSquareState, int square, Color color)
    {
        _grid[square].SetSquare(gridSquareState, color);
    }
    public GridSquareState GetSpecificSquareState(int squareId)
    {
        return _grid[squareId].GetSquareState();
    }
    public bool CheckIfGridFull()
    {
        foreach(GridSquareManager square in _grid)
        {
            if(square.GetSquareState() == GridSquareState.empty)
            {
                return false;
            }
        }
        return true;
    }
    public bool HasAnyMove()
    {
        foreach (GridSquareManager square in _grid)
        {
            if(square.GetSquareState() != GridSquareState.empty)
            {
                return true;
            }
        }

        return false;
    }
    public GridSquareState CheckForWWin(int gridSquare1, int gridSquare2, int gridSquare3)
    {
        GridSquareState state1 = _grid[gridSquare1].GetSquareState();
        GridSquareState state2 = _grid[gridSquare2].GetSquareState();
        GridSquareState state3 = _grid[gridSquare3].GetSquareState();

        if(state1 != GridSquareState.empty)
        {
            if(state1 == state2 && state2 == state3)
            {
                return state1;
            }
            else
            {
                return GridSquareState.empty;
            } 
        }
        return GridSquareState.empty;
    }
}
