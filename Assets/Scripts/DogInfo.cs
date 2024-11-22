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

    // This works on PC
    private void OnMouseUp()
    {
        HandleSelection();
    }

    private void Update()
    {
        // Handle mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                // Check if this object is tapped
                if (IsTouchedObject(touch.position))
                {
                    HandleSelection();
                }
            }
        }
    }

    private void HandleSelection()
    {
        Debug.Log("Dog selected: " + dogData.dogName + " with personality: " + assignedPersonality);
        gameManager.kennelManager.SelectDog(dogData);
        gameManager.kennelManager.selectedPersonality = assignedPersonality;
        gameManager.kennelManager.selectedGender = assignedGender;

        if (dogSelectedEvent != null)
            dogSelectedEvent.Raise();
    }

    private bool IsTouchedObject(Vector3 touchPosition)
    {
        // Cast a ray from the touch position to check if it hits this object
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }

        return false;
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
