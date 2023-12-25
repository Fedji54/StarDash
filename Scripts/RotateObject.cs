using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _stop;
    [SerializeField] private float _time;
    private Quaternion _startRotation;
    private bool _rotationSaved, _paused;

    private void OnEnable()
    {
        StopAllCoroutines();
        if (!_rotationSaved)
        {
            _startRotation = transform.rotation;
            _rotationSaved = true;
        }
        transform.rotation = _startRotation;
        if (_stop)
        {
            _paused = false;
            StartCoroutine(StopTimer());
        }
    }

    private void Update()
    {
        if (!_paused)
        {
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
        }
    }

    private IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(_time);
        _paused = true;
    }
}