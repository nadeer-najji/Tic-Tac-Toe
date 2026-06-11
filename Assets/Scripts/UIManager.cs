using UnityEngine;
using TMPro;
using UnityEditor.ProjectWindowCallback;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject _homePanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _backPanel;

    [Header("Back Panel")]
    [SerializeField] private TextMeshProUGUI _backPanelMessageText;

    private GameObject _currentPanel;
    private GameObject _targetPanel;
    private bool _quitRequested;

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowHomePanel();
    }

    #endregion

    #region Home Panel

    private void ShowHomePanel()
    {
        _homePanel.SetActive(true);
        _gamePanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _backPanel.SetActive(false);

        _currentPanel = _homePanel;
    }

    public void PlayButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _homePanel.SetActive(false);
        _gamePanel.SetActive(true);

        _currentPanel = _gamePanel;

        GameManager.Instance.RestartClicked();
    }

    public void QuitButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _quitRequested = true;

        _backPanelMessageText.text = "DO YOU WANT TO QUIT?";

        _backPanel.SetActive(true);
    }

    #endregion

    #region Settings

    public void OpenSettingsFromHome()
    {
        AudioManager.Instance.PlayButtonClick();

        _homePanel.SetActive(false);
        _settingsPanel.SetActive(true);

        _currentPanel = _settingsPanel;

        // Back should return to Home
        _targetPanel = _homePanel;
    }

    public void OpenSettingsFromGame()
    {
        AudioManager.Instance.PlayButtonClick();

        _gamePanel.SetActive(false);
        _settingsPanel.SetActive(true);

        _currentPanel = _settingsPanel;

        // Back should return to Game
        _targetPanel = _gamePanel;
    }

    public void SettingsBackClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _settingsPanel.SetActive(false);

        _targetPanel.SetActive(true);

        _currentPanel = _targetPanel;
    }

    #endregion

    #region Back Panel

    public void OpenBackPanelFromGame()
    {
        AudioManager.Instance.PlayButtonClick();

        _quitRequested = false;

        _backPanelMessageText.text = "DO YOU WANT TO GO BACK?";

        _targetPanel = _homePanel;

        _backPanel.SetActive(true);
    }

    public void YesButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _backPanel.SetActive(false);

        if (_quitRequested)
        {
            Application.Quit();

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
            return;
        }

        _currentPanel.SetActive(false);

        _targetPanel.SetActive(true);

        _currentPanel = _targetPanel;
    }

    public void NoButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _backPanel.SetActive(false);
    }

    #endregion

}