using UnityEngine;

public class ShaderLerp : MonoBehaviour
{
    public AnimationCurve _curve;
    private float _multiplier = 1f;
    [SerializeField] float _curSpeed = 1f;
    [SerializeField] private Material _plasmaMat;
    [SerializeField] private string _plasmaVar;

    private void Update()
    {
        _plasmaMat.SetFloat(_plasmaVar, _curve.Evaluate(Time.time * _curSpeed) * _multiplier);
    }
}