using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GridManager _gridManager;
    private GridSquareState _playerSquareState;
    private GridSquareState _enemySquareState;
    private Turn _currentTurn;
    private bool _awaitingTime =false;

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
        _awaitingTime = true;
    }

    private void ProcessTurn(Turn turn, int selectedSquare)
    {
        _awaitingTime = false;
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
        ChangeTurn();
        _awaitingTime = true;
    }
    public void ChangeTurn()
    {
        if(_currentTurn == Turn.playerTurn)
        {
            _currentTurn = Turn.enemyTurn;
        }
        else
        {
            _currentTurn = Turn.playerTurn;
        }
    }
    public void GridSquareClicked(int clickedSquare)
    {
        if(_awaitingTime == false)
        {
            return;
        }
        if(_gridManager.GetSpecificSquareState(clickedSquare) != GridSquareState.empty)
        {
            return;
        }
        ProcessTurn(_currentTurn, clickedSquare);
    }
}
public enum Turn
{
    playerTurn,
    enemyTurn
}