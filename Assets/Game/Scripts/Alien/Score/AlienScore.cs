using System;
using UnityEngine;

public class AlienScore : MonoBehaviour
{
    public event Action<float> Changed;

    public float Value { get; private set; } = 0;

    public void Reset()
    {
        Value = 0;
        Changed?.Invoke(Value);
    }

    public void Add(float value)
    {
        Value += value;
        Changed?.Invoke(Value);
    }
}