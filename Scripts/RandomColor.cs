using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        Vector3 color = Vector3.zero;
        while (color.magnitude < 0.5f)
        {
            color = new(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        _spriteRenderer.color = new(color.x, color.y, color.z);
    }
}