using UnityEngine;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public GameEventSO dogSelectedEvent;
    public GameManagerSO gameManager;

    private Personality assignedPersonality;
    private bool personalityAssigned = false;

    private void Start()
    {
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
        gameManager.kennelManager.SelectDog(dogData);
        gameManager.kennelManager.selectedPersonality = assignedPersonality;
        
        if (dogSelectedEvent != null)
            dogSelectedEvent.Raise();
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }
}
