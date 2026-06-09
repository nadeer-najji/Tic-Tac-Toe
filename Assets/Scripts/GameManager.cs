using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GridManager _gridManager;
    private GridSquareState _playerSquareState;
    private GridSquareState _enemySquareState;
    private Turn _currentTurn;
    private bool _awaitingTime =false;
    private GameResult _currentGameState;
    [SerializeField] private TextMeshProUGUI _playerCharacterText;
    [SerializeField] private TextMeshProUGUI _enemyCharacterText;
    [SerializeField] private TextMeshProUGUI _currentPlayerNumberText;
    [SerializeField] private TextMeshProUGUI _currentPlayerCharText;

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

    public void RestartClicked()
    {
        StartNewGame();
    }
    private void StartNewGame()
    {
        _currentGameState = GameResult.ongoing;
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

        //Set Player and Enemy Character UI
        _playerCharacterText.text = _playerSquareState.ToString();
        _enemyCharacterText.text = _enemySquareState.ToString();

        SetCurrentTurnUI();
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

        bool gameEnded =CheckIfGameEnded();
        if(!gameEnded)
        {
            ChangeTurn();
            _awaitingTime = true;
        }
    }

    private bool CheckIfGameEnded()
    {
        bool gridFull = _gridManager.CheckIfGridFull();

        GridSquareState winner = CheckForWin();
        if(winner != GridSquareState.empty)
        {
            if(winner == _playerSquareState)
            {
                _currentGameState = GameResult.PlayerWin;
                return true;
            }
            else
            {
                _currentGameState = GameResult.EnemyWin;
                return true;
            }
        }
        else
        {
            if(gridFull)
            {
                _currentGameState = GameResult.draw;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    private GridSquareState CheckForWin()
    {
        GridSquareState winner = GridSquareState.empty;

        // Horizontal Win Checks
        winner = _gridManager.CheckForWWin(0, 1, 2);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }
        winner = _gridManager.CheckForWWin(3, 4, 5);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }
        winner = _gridManager.CheckForWWin(6, 7, 8);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }

        // Vertical Win Checks
        winner = _gridManager.CheckForWWin(0, 3, 6);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }
        winner = _gridManager.CheckForWWin(1, 4, 7);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }
        winner = _gridManager.CheckForWWin(2, 5, 8);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }

        // Diagonal Win Checks
        winner = _gridManager.CheckForWWin(0, 4, 8);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }
        winner = _gridManager.CheckForWWin(2, 4, 6);
        if(winner != GridSquareState.empty)
        {
            return winner;
        }

        return winner;
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
        SetCurrentTurnUI();
    }
    private void SetCurrentTurnUI()
    {
        if(_currentTurn == Turn.playerTurn)
        {
            _currentPlayerNumberText.text = "Player 1";
            _currentPlayerCharText.text = _playerSquareState.ToString();
        }
        else
        {
            _currentPlayerNumberText.text = "Player 2";
            _currentPlayerCharText.text = _enemySquareState.ToString();
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

public enum GameResult
{
    ongoing,
    draw,
    PlayerWin,
    EnemyWin
}