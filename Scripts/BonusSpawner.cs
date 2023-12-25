using Lean.Pool;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonuses;
    [Range(0f, 100f)]
    [SerializeField] private float _spawnChance;
    private GameObject _spawnedBonus;

    private void OnEnable()
    {
        if (_spawnedBonus != null && _spawnedBonus.activeInHierarchy)
        {
            LeanPool.Despawn(_spawnedBonus);
        }
        float chance = Random.Range(0f, 100f);
        if (_spawnChance >= chance)
        {
            _spawnedBonus = LeanPool.Spawn(_bonuses[Random.Range(0, _bonuses.Length)], transform);
        }
    }
}