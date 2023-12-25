using System.Collections;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private float _minXSize, _maxXSize, _minYSize, _maxYSize, _speed;
    private float _xStartSize, _yStartSize;
    private float _xSize, _ySize;
    [SerializeField] private bool _scaleX, _scaleY;
    private bool _reverse, _startSizeSaved, _paused;
    [SerializeField] private bool _stop;
    [SerializeField] private float _time;

    private void Awake()
    {
        _xSize = transform.localScale.x;
        _ySize = transform.localScale.y;
    }

    private void OnEnable()
    {
        if (!_startSizeSaved)
        {
            _xStartSize = transform.localScale.x;
            _yStartSize = transform.localScale.y;
            _startSizeSaved = true;
        }
        _xSize = _xStartSize;
        _ySize = _yStartSize;
        transform.localScale = new(_xSize, _ySize, 1f);
        if (_stop)
        {
            _paused = false;
            StartCoroutine(StopTimer());
        }
    }

    private void Update()
    {
        if (_reverse && !_paused)
        {
            if (_scaleX)
            {
                _xSize += _speed * Time.deltaTime;
                _xSize = Mathf.Clamp(_xSize, _minXSize, _maxXSize);
            }
            if (_scaleY)
            {
                _ySize += _speed * Time.deltaTime;
                _ySize = Mathf.Clamp(_ySize, _minYSize, _maxYSize);
            }
            transform.localScale = new(_xSize, _ySize, 1f);
            if (_scaleX && _scaleY)
            {
                if (_xSize >= _maxXSize && _ySize >= _maxYSize)
                {
                    _reverse = false;
                }
            }
            else if (_scaleX && !_scaleY)
            {
                if (_xSize >= _maxXSize)
                {
                    _reverse = false;
                }
            }
            else if (!_scaleX && _scaleY)
            {
                if (_ySize >= _maxYSize)
                {
                    _reverse = false;
                }
            }
        }
        else if(!_paused)
        {
            if (_scaleX)
            {
                _xSize -= _speed * Time.deltaTime;
                _xSize = Mathf.Clamp(_xSize, _minXSize, _maxXSize);
            }
            if (_scaleY)
            {
                _ySize -= _speed * Time.deltaTime;
                _ySize = Mathf.Clamp(_ySize, _minYSize, _maxYSize);
            }
            transform.localScale = new(_xSize, _ySize, 1f);
            if (_scaleX && _scaleY)
            {
                if (_xSize <= _minXSize && _ySize <= _minYSize)
                {
                    _reverse = true;
                }
            }
            else if (_scaleX && !_scaleY)
            {
                if (_xSize <= _minXSize)
                {
                    _reverse = true;
                }
            }
            else if (!_scaleX && _scaleY)
            {
                if (_ySize <= _minYSize)
                {
                    _reverse = true;
                }
            }
        }
    }

    private IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(_time);
        _paused = true;
    }
}