using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private bool _disableOnTrigger;

    private void OnEnable()
    {
        if (_disableOnTrigger)
        {
            _object.SetActive(true);
        }
        else
        {
            _object.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_disableOnTrigger)
            {
                _object.SetActive(false);
            }
            else
            {
                _object.SetActive(true);
            }
        }
    }
}