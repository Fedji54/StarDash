using Lean.Pool;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager _manager;
    [SerializeField] private Camera _cam;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;
    private Vector3 _position;
    [SerializeField] private float _speed;
    private bool _inputEnabled;
    [SerializeField] private GameObject _reviveEffect, _deathEffect, _shieldEffect;
    [SerializeField] private AudioSource _soundS;
    [SerializeField] private AudioClip _obstacleClip, _explosionClip;
    private bool _shieldEnabled, _onceRevived;
    private GameObject _shield;
    [SerializeField] private bool _godMode;

    private void Update()
    {
        if (_inputEnabled)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _position = _cam.ScreenToWorldPoint(touch.position);
                _position.z = 0f;
                transform.position = Vector3.Lerp(transform.position, _position, _speed * Time.deltaTime);
            }
            else if (Input.GetMouseButton(0))
            {
                _position = _cam.ScreenToWorldPoint(Input.mousePosition);
                _position.z = 0f;
                transform.position = Vector3.Lerp(transform.position, _position, _speed * Time.deltaTime);
            }
        }
    }

    public void EnableInput()
    {
        _inputEnabled = true;
    }

    public void DisableInput()
    {
        _inputEnabled = false;
    }

    private void GameOver()
    {
        _manager.PauseGame();
        DisableInput();
        Time.timeScale = 1f;
        _soundS.PlayOneShot(_obstacleClip);
        StartCoroutine(GameOverAction());
    }

    private IEnumerator GameOverAction()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject effect = LeanPool.Spawn(_deathEffect, transform.position, Quaternion.identity);
        _soundS.PlayOneShot(_explosionClip);
        yield return new WaitForSeconds(1.25f);
        if (_onceRevived)
        {
            _manager.GameOver();
        }
        else
        {
            _manager.AskForRevive();
        }
        LeanPool.Despawn(effect, 3f);
        _spriteRenderer.gameObject.SetActive(false);
        _trailRenderer.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void RewardAdSuccess()
    {
        Time.timeScale = 1f;
        StartCoroutine(ReviveTimer());
    }

    private IEnumerator ReviveTimer()
    {
        yield return null;
        GameObject effect = LeanPool.Spawn(_reviveEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        _spriteRenderer.gameObject.SetActive(true);
        _trailRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        LeanPool.Despawn(effect, 2f);
        _onceRevived = true;
        _manager.UnpauseGame();
        _inputEnabled = true;
        _godMode = true;
        Shield();
        yield return new WaitForSeconds(2f);
        _godMode = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            bonus.PickUp(gameObject);
        }
        if (collision.gameObject.layer == 10 && _inputEnabled && _shieldEnabled && !_godMode)
        {
            LeanPool.Despawn(_shield);
            _shieldEnabled = false;
        }
        else if (collision.gameObject.layer == 10 && _inputEnabled && !_shieldEnabled && !_godMode)
        {
            GameOver();
        }
    }

    public void Shield()
    {
        if (_inputEnabled && !_shieldEnabled)
        {
            _shieldEnabled = true;
            _shield = LeanPool.Spawn(_shieldEffect, transform);
            //StartCoroutine(ShieldTimer(time));
        }
    }

    private IEnumerator ShieldTimer(float time)
    {
        yield return null;
        //GameObject shield = LeanPool.Spawn(_shieldEffect, transform);
        yield return new WaitForSeconds(time);
        //LeanPool.Despawn(shield);
        yield return null;
        _shieldEnabled = false;
    }
}