using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _tap = KeyCode.Space;
    private KeyCode _shoot = KeyCode.K;

    public event Action Tapped;
    public event Action ShootPressed;

    private void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown(_tap))
                Tapped?.Invoke();

            if (Input.GetKeyDown(_shoot))
                ShootPressed?.Invoke();
        }
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

}
