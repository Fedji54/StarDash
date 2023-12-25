using Lean.Pool;
using UnityEngine;

public class BonusBoostTime : Bonus
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
        Time.timeScale += _amount;
        LeanPool.Despawn(gameObject);
    }
}