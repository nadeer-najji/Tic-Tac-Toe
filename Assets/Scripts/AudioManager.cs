using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _bgMusicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _bgMusicClip;
    [SerializeField] private AudioClip _buttonClickClip;
    [SerializeField] private AudioClip _placeXOClip;

    [Header("Settings UI")]
    [SerializeField] private Image _musicButtonImage;
    [SerializeField] private Image _sfxButtonImage;

    [Header("Button Sprites")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private bool _musicEnabled;
    private bool _sfxEnabled;

    private const string MUSIC_KEY = "MusicEnabled";
    private const string SFX_KEY = "SFXEnabled";

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
        LoadSettings();

        _bgMusicSource.clip = _bgMusicClip;
        _bgMusicSource.loop = true;
        _bgMusicSource.Play();

        ApplySettings();
    }

    //=========================================
    // MUSIC
    //=========================================

    public void ToggleMusic()
    {
        PlayButtonClick();

        _musicEnabled = !_musicEnabled;

        SaveSettings();
        ApplySettings();
    }

    //=========================================
    // SFX
    //=========================================

    public void ToggleSFX()
    {
        PlayButtonClick();
        
        _sfxEnabled = !_sfxEnabled;

        SaveSettings();
        ApplySettings();
    }

    //=========================================
    // PLAY SOUNDS
    //=========================================

    public void PlayButtonClick()
    {
        if (!_sfxEnabled)
            return;

        _sfxSource.PlayOneShot(_buttonClickClip);
    }

    public void PlayPlaceXO()
    {
        if (!_sfxEnabled)
            return;

        _sfxSource.PlayOneShot(_placeXOClip);
    }

    //=========================================
    // SETTINGS
    //=========================================

    private void ApplySettings()
    {
        _bgMusicSource.mute = !_musicEnabled;

        _musicButtonImage.sprite =
            _musicEnabled ? _onSprite : _offSprite;

        _sfxButtonImage.sprite =
            _sfxEnabled ? _onSprite : _offSprite;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(MUSIC_KEY, _musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt(SFX_KEY, _sfxEnabled ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        _musicEnabled =
            PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;

        _sfxEnabled =
            PlayerPrefs.GetInt(SFX_KEY, 1) == 1;
    }

    //=========================================
    // GETTERS
    //=========================================

    public bool IsMusicEnabled()
    {
        return _musicEnabled;
    }

    public bool IsSFXEnabled()
    {
        return _sfxEnabled;
    }
}