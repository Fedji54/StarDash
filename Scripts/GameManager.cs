using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private Player _player;
    [SerializeField] private RoomGenerator _generator;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private TMP_Text _scoreText, _inGameScoreText;
    [SerializeField] private AudioSource _ambientS, _soundS;
    [SerializeField] private AudioClip _startClip, _endClip;
    private float _curAlpha = 1f;
    private bool _gameLoaded, _paused;
    [SerializeField] private float _speedUp;
    private float _score = 0f;
    [SerializeField] private Camera _cam;
    [SerializeField] private float _zoomOutSize = 10f;
    [SerializeField] private GameObject _reviveDialog;

    private void Awake()
    {
        _reviveDialog.SetActive(false);
        _scoreText.text = string.Empty;
        _inGameScoreText.text = string.Empty;
        _fadeImage.color = new(0f, 0f, 0f, 1f);
        _scoreText.color = new(1f, 1f, 1f, _curAlpha);
    }

    private void Start()
    {
        TargetFrameRate.SetLimit(_targetFrameRate);
        Time.timeScale = 1f;
        if (YandexGame.savesData.MaxScore > 0)
        {
            _scoreText.text = $"Последний рекорд: {YandexGame.savesData.MaxScore:#}";
        }
        StartCoroutine(LoadGame());
    }

    private void Update()
    {
        if (_gameLoaded && !_paused)
        {
            _score += Time.timeScale * Time.deltaTime;
            Time.timeScale += _speedUp * Time.deltaTime;
            _inGameScoreText.text = $"{_score:#}";
        }
        if (Input.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }

    private IEnumerator LoadGame()
    {
        _generator.EnableGenerator();
        while (!_gameLoaded)
        {
            yield return new WaitForSeconds(0.02f);
            _cam.orthographicSize = _zoomOutSize - (_zoomOutSize * _curAlpha) + 0.1f;
            _curAlpha -= 0.01f;
            _curAlpha = Mathf.Clamp(_curAlpha, 0f, 1f);
            _fadeImage.color = new(0f, 0f, 0f, _curAlpha);
            _scoreText.color = new(1f, 1f, 1f, _curAlpha);
            if (_curAlpha <= 0f)
            {
                _curAlpha = 0f;
                _cam.orthographicSize = _zoomOutSize;
                _gameLoaded = true;
            }
        }
        _player.EnableInput();
        _soundS.PlayOneShot(_startClip);
        yield return new WaitForSeconds(1f);
        _ambientS.Play();
    }

    public void GameOver()
    {
        _inGameScoreText.text = string.Empty;
        _reviveDialog.SetActive(false);
        Time.timeScale = 1f;
        _score = Mathf.RoundToInt(_score);
        if (_score > YandexGame.savesData.MaxScore)
        {
            YandexGame.savesData.MaxScore = _score;
            YandexGame.SaveProgress();
            YandexGame.NewLeaderboardScores("Score", (int)_score);
            _scoreText.text = $"Новый рекорд: {YandexGame.savesData.MaxScore:#}";
        }
        else
        {
            _scoreText.text = $"Счёт: {_score:#}";
        }
        _ambientS.Stop();
        StartCoroutine(ReloadGame());
    }

    private IEnumerator ReloadGame()
    {
        FindObjectOfType<LerpCameraBackgroundColor>().EnableForceBlack();
        yield return new WaitForSeconds(0.5f);
        _soundS.PlayOneShot(_endClip);
        while (_gameLoaded)
        {
            yield return new WaitForSeconds(0.02f);
            _curAlpha += 0.01f;
            _cam.orthographicSize = _zoomOutSize - (_zoomOutSize * _curAlpha) + 0.1f;
            _curAlpha = Mathf.Clamp(_curAlpha, 0f, 1f);
            _fadeImage.color = new(0f, 0f, 0f, _curAlpha);
            _scoreText.color = new(1f, 1f, 1f, _curAlpha);
            if (_curAlpha >= 1f)
            {
                _gameLoaded = false;
            }
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        _paused = true;
        _ambientS.Stop();
        Room[] rooms = FindObjectsOfType<Room>();
        foreach (Room room in rooms)
        {
            room.Pause();
        }
    }

    public void UnpauseGame()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        foreach (Room room in rooms)
        {
            room.Unpause();
        }
        _paused = false;
        _ambientS.Play();
    }

    public void AskForRevive()
    {
        _reviveDialog.SetActive(false);
        StartCoroutine(AskForReviveTimer());
    }

    private IEnumerator AskForReviveTimer()
    {
        yield return null;
        yield return new WaitForSeconds(2f);
        _reviveDialog.SetActive(true);
    }

    public void ShowAdForRevive()
    {
        _reviveDialog.SetActive(false);
        YandexGame.RewVideoShow(0);
    }
}