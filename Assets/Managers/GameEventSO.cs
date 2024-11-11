using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game/Event")]
public class GameEventSO : ScriptableObject
{
    private event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

    public void RegisterListener(Action listener)
    {
        OnEventRaised += listener;
    }

    public void UnregisterListener(Action listener)
    {
        OnEventRaised -= listener;
    }
}
