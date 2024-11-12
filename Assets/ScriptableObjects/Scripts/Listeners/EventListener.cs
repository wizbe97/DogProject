using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventListener", menuName = "Game/Event Listener")]
public class EventListenerSO : ScriptableObject
{
    public GameEventSO gameEvent; // The event to listen to
    private Action responseAction;

    public void RegisterListener(Action response)
    {
        responseAction = response;
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(responseAction);
        }
    }

    public void UnregisterListener()
    {
        if (gameEvent != null)
        {
            gameEvent.UnregisterListener(responseAction);
        }
    }
}
