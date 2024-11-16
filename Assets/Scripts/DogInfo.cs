using UnityEngine;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public GameEventSO dogSelectedEvent;
    public GameManagerSO gameManager;

    private Personality assignedPersonality;
    private DogGender assignedGender;
    private bool personalityAssigned = false;
    private bool genderAssigned = false;

    private void Start()
    {
        AssignRandomPersonality();
        AssignRandomGender();
    }

    private void AssignRandomPersonality()
    {
        if (!personalityAssigned)
        {
            assignedPersonality = (Personality)Random.Range(0, System.Enum.GetValues(typeof(Personality)).Length);
            personalityAssigned = true;
        }
    }

    private void AssignRandomGender()
    {
        if (!genderAssigned)
        {
            assignedGender = (DogGender)Random.Range(0, System.Enum.GetValues(typeof(DogGender)).Length);
            genderAssigned = true;
        }
    }


    private void OnMouseUp()
    {
        Debug.Log("Dog selected: " + dogData.dogName + " with personality: " + assignedPersonality);
        gameManager.kennelManager.SelectDog(dogData);
        gameManager.kennelManager.selectedPersonality = assignedPersonality;
        gameManager.kennelManager.selectedGender = assignedGender;

        if (dogSelectedEvent != null)
            dogSelectedEvent.Raise();
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }

    public DogGender GetAssignedGender()
    {
        return assignedGender;
    }
}
