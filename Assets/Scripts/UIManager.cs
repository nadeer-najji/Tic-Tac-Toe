using UnityEngine;
using TMPro;

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

    private void Awake()
    {
        if (Instance == null)
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

    //==================================================
    // HOME PANEL
    //==================================================

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

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    //==================================================
    // SETTINGS
    //==================================================

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

    //==================================================
    // BACK PANEL OPENING
    //==================================================

    public void OpenBackPanelFromGame()
    {
        AudioManager.Instance.PlayButtonClick();

        _backPanelMessageText.text = "DO YOU WANT TO GO BACK?";

        _targetPanel = _homePanel;

        _backPanel.SetActive(true);
    }

    public void OpenBackPanelFromSettings()
    {
        AudioManager.Instance.PlayButtonClick();

        _backPanelMessageText.text = "DO YOU WANT TO GO BACK?";

        _backPanel.SetActive(true);
    }

    //==================================================
    // BACK PANEL BUTTONS
    //==================================================

    public void YesButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();

        _backPanel.SetActive(false);

        _currentPanel.SetActive(false);

        _targetPanel.SetActive(true);

        _currentPanel = _targetPanel;
    }

    public void NoButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        
        _backPanel.SetActive(false);
    }
}