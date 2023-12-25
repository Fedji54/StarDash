using Lean.Pool;
using UnityEngine;

public class BonusSlowTime : Bonus
{
    [SerializeField] private float _amount;
    [SerializeField] private bool _random;
    [SerializeField] private float _minAmount, _maxAmount;

    private void OnEnable()
    {
        if (_random)
        {
            _amount = Random.Range(_minAmount, _maxAmount);
        }
    }

    public override void PickUp(GameObject who)
    {
        float value = Time.timeScale - _amount;
        value = Mathf.Clamp(value, 0.5f, 1000f);
        Time.timeScale = value;
        LeanPool.Despawn(gameObject);
    }
}