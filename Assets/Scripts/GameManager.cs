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
    [SerializeField] private GameObject _gameResultUI;
    [SerializeField] private GameObject _currentTurnUI;
    [SerializeField] private TextMeshProUGUI _gameResultText;
    [SerializeField] private Color _playerColor;
    [SerializeField] private Color _enemyColor;

    private void Awake()
    {
        if(Instance == null && Instance != this)
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

        _gameResultUI.SetActive(false);
        _currentTurnUI.SetActive(true);
        SetCurrentTurnUI();
        _awaitingTime = true;
    }

    private void ProcessTurn(Turn turn, int selectedSquare)
    {
        _awaitingTime = false;
        GridSquareState state = GridSquareState.empty;

        Color turnColor = Color.white;
        if(turn == Turn.playerTurn)
        {
            state = _playerSquareState;
            turnColor = _playerColor;
        }
        else if(turn == Turn.enemyTurn)
        {
            state = _enemySquareState;
            turnColor = _enemyColor;
        }
        _gridManager.SetSpecificSquare(state, selectedSquare, turnColor);

        bool gameEnded =CheckIfGameEnded();
        if(!gameEnded)
        {
            ChangeTurn();
            _awaitingTime = true;
        }
        else
        {
            _currentTurnUI.SetActive(false);
            _gameResultUI.SetActive(true);
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
                //Player has won
                _currentGameState = GameResult.PlayerWin;
                _gameResultText.text = _playerSquareState.ToString() +" WINS";
                _gameResultText.color = _playerColor;
                return true;
            }
            else
            {
                // Enemy has won
                _currentGameState = GameResult.EnemyWin;
                _gameResultText.text = _enemySquareState.ToString() +" WINS";
                _gameResultText.color = _enemyColor;
                return true;
            }
        }
        else
        {
            if(gridFull)
            {
                // Game is a draw
                _currentGameState = GameResult.draw;
                _gameResultText.text = "DRAW";
                _gameResultText.color = Color.black;
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
            _currentPlayerNumberText.color = _playerColor;
            _currentPlayerCharText.color = _playerColor;
            _currentPlayerCharText.text = _playerSquareState.ToString();
        }
        else
        {
            _currentPlayerNumberText.text = "Player 2";
            _currentPlayerNumberText.color = _enemyColor;
            _currentPlayerCharText.color = _enemyColor;
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