using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance { get; private set; } = null;

    public bool IsPaused { get; private set; }

    public event Action OnPaused;
    public event Action OnUnpaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple PauseController instances detected.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void TogglePause()
    {
        if (IsPaused)
            Unpause();
        else
            Pause();
    }

    public void Pause()
    {
        if (IsPaused) return;

        IsPaused = true;
        OnPaused?.Invoke();
    }

    public void Unpause()
    {
        if (!IsPaused) return;

        IsPaused = false;
        OnUnpaused?.Invoke();
    }
}