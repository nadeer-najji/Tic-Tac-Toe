using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GridManager _gridManager;
    private GridSquareState _playerSquareState;
    private GridSquareState _enemySquareState;
    private Turn _currentTurn;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartNewGame();
    }

    private void StartNewGame()
    {
        // Reset the grid
        _gridManager.ResetGrid();

        // Randomly decide who goes first
        int firstTurn = Random.Range(0, 2);
        _currentTurn = (Turn)firstTurn;

        // Assign grid sqaure state to player
        if(firstTurn == 0)
        {
            _playerSquareState = GridSquareState.x;
            _enemySquareState = GridSquareState.o;
        }
        else
        {
            _playerSquareState = GridSquareState.o;
            _enemySquareState = GridSquareState.x;
        }
    }

    private void ProcessTurn(Turn turn, int selectedSquare)
    {
        GridSquareState state = GridSquareState.empty;

        if(turn == Turn.playerTurn)
        {
            state = _playerSquareState;
        }
        else if(turn == Turn.enemyTurn)
        {
            state = _enemySquareState;
        }
        _gridManager.SetSpecificSquare(state, selectedSquare);
    }
    public void GridSquareClicked(int clickedSquare)
    {
        Debug.Log("Square Clicked: " + clickedSquare);
    }
}
public enum Turn
{
    playerTurn,
    enemyTurn
}