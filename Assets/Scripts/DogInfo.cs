using UnityEngine;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public GameEventSO dogSelectedEventSO;

    private Personality assignedPersonality;
    private bool personalityAssigned = false;

    private void Start()
    {
        // Assign a random personality at the start of the game if not already assigned
        AssignRandomPersonality();
    }

    private void AssignRandomPersonality()
    {
        if (!personalityAssigned)
        {
            assignedPersonality = (Personality)Random.Range(0, System.Enum.GetValues(typeof(Personality)).Length);
            personalityAssigned = true;
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Dog selected: " + dogData.dogName + " with personality: " + assignedPersonality);
        dogSelectedEventSO.Raise(this); // Raise the event with this DogInfo instance
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }
}
