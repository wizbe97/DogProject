using UnityEngine;
using UnityEngine.UI;

public class ButtonClickManager : MonoBehaviour
{
    public GameEventSO eventToRaise;

    public void RaiseEvent()
    {
        if (eventToRaise != null)
        {
            eventToRaise.Raise();
        }
        else
        {
            Debug.LogWarning("No event assigned to ButtonClickHandler.");
        }
    }
}
