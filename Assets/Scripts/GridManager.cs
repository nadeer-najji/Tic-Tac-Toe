using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSquareManager[] _grid;

    public void ResetGrid()
    {
        foreach(GridSquareManager square in _grid)
        {
            square.SetSquare(GridSquareState.empty);
        }

        for(int i=0;i<_grid.Length;i++)
        {
            _grid[i].SetSquare(GridSquareState.empty);
            _grid[i].SetSquareId(i);
        }
    }

    public void SetSpecificSquare(GridSquareState gridSquareState, int square)
    {
        _grid[square].SetSquare(gridSquareState);
    }
}
