using UnityEngine;

public class EventListener : MonoBehaviour, IGameEventListener
{
    public GameEventSO gameEvent;
    public UnityEngine.Events.UnityEvent<object> response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(object parameter)
    {
        response.Invoke(parameter); 
    }
}
