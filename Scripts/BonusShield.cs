using Lean.Pool;
using UnityEngine;

public class BonusShield : Bonus
{
    //[SerializeField] private float  _time;
    //[SerializeField] private bool _random;
    //[SerializeField] private float  _minTime, _maxTime;

    //private void OnEnable()
    //{
    //    if (_random)
    //    {
    //        _time = Random.Range(_minTime, _maxTime);
    //    }
    //}

    public override void PickUp(GameObject who)
    {
        who.GetComponent<Player>().Shield();
        LeanPool.Despawn(gameObject);
    }
}