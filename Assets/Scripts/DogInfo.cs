using UnityEngine;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public KennelListener kennelListener;

    private Personality assignedPersonality;
    private bool personalityAssigned = false;

    private void Start()
    {
        if (!personalityAssigned)
        {
            assignedPersonality = (Personality)Random.Range(0, System.Enum.GetValues(typeof(Personality)).Length);
            personalityAssigned = true;
        }
    }

    private void OnMouseUp()
    {
        if (kennelListener != null)
        {
            kennelListener.ShowDogInfo(this);
        }
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }
}
