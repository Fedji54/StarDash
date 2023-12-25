using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 _startPoint;
    [SerializeField] private Transform _point1, _point2;
    [SerializeField] private bool _reverse, _resetPositionOnEnable;
    private bool _positionSaved, _paused;
    [SerializeField] private float _speed;
    [SerializeField] private bool _stop;
    [SerializeField] private float _time;

    private void OnEnable()
    {
        if (_resetPositionOnEnable)
        {
            if (_positionSaved)
            {
                transform.localPosition = _startPoint;
            }
            else
            {
                _startPoint = transform.localPosition;
                _positionSaved = true;
            }
        }
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
            float distance = Vector3.Distance(transform.localPosition, _point1.localPosition);
            transform.localPosition = Vector3.Lerp(transform.localPosition, _point1.localPosition, _speed / distance * Time.deltaTime);
            if (distance < 0.1f)
            {
                _reverse = false;
            }
        }
        else if(!_paused)
        {
            float distance = Vector3.Distance(transform.localPosition, _point2.localPosition);
            transform.localPosition = Vector3.Lerp(transform.localPosition, _point2.localPosition, _speed / distance * Time.deltaTime);
            if (distance < 0.1f)
            {
                _reverse = true;
            }
        }
    }

    private IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(_time);
        _paused = true;
    }
}