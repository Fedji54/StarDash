using Lean.Pool;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private float _size, _speed;
    [SerializeField] private Transform _upPosition;
    private RoomGenerator _generator;
    private bool _spawned, _pause;

    public float Size { get { return _size; } }

    public void Setup(RoomGenerator generator)
    {
        _generator = generator;
        _spawned = false;
    }

    private void Update()
    {
        if (_pause) return;
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if (_generator != null && !_spawned)
        {
            if (transform.position.y < _generator.transform.position.y)
            {
                _generator.SpawnRoom(_upPosition);
                _spawned = true;
            }
        }
        else if (transform.position.y < -(_size / 2f) - 5f)
        {
            LeanPool.Despawn(gameObject);
        }
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Unpause()
    {
        _pause = false;
    }
}