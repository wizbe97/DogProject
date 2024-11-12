using UnityEngine;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public KennelListener kennelListener; // Reference to the KennelListener component

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
        Debug.Log("DogInfo: OnMouseUp triggered");  // Debug to confirm click event
        if (kennelListener != null)
        {
            kennelListener.ShowDogInfo(this);
        }
        else
        {
            Debug.LogWarning("DogInfo: KennelListener reference is missing!");
        }
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }
}
