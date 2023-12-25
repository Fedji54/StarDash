using UnityEngine;

public class LerpCameraBackgroundColor : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _colorLerpSpeed;
    [SerializeField] private Color[] _colors;
    private Vector3 _curColor, _nextColor;
    private int _curIndex;
    private bool _forceBlack;

    private void Start()
    {
        _curColor = new(_cam.backgroundColor.r, _cam.backgroundColor.g, _cam.backgroundColor.b);
        _curIndex = Random.Range(0, _colors.Length);
        _nextColor = new(_colors[_curIndex].r, _colors[_curIndex].g, _colors[_curIndex].b);
    }

    private void Update()
    {
        if (_forceBlack)
        {
            _curColor = Vector3.Lerp(_curColor, _nextColor, Time.deltaTime);
            _cam.backgroundColor = new(_curColor.x, _curColor.y, _curColor.z);
        }
        else
        {
            _curColor = Vector3.Lerp(_curColor, _nextColor, _colorLerpSpeed * Time.deltaTime);
            _cam.backgroundColor = new(_curColor.x, _curColor.y, _curColor.z);
            if (Vector3.Distance(_curColor, _nextColor) <= 0.1f)
            {
                _curIndex = Random.Range(0, _colors.Length);
                _nextColor = new(_colors[_curIndex].r, _colors[_curIndex].g, _colors[_curIndex].b);
            }
        }
    }

    public void EnableForceBlack()
    {
        _nextColor = new(0f, 0f, 0f);
        _forceBlack = true;
    }
}