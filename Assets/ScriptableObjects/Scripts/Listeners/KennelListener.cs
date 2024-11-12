using UnityEngine;

public class KennelListener : MonoBehaviour
{
    public GameEventSO onDogSelectedEvent;       // Event for dog selection
    public KennelManager kennelManager;          // Reference to KennelManager
    public GameManager gameManager;              // Reference to GameManager for balance checks
    public GameObject dogInfoPanel;              // Reference to the UI panel for displaying dog info

    private DogInfo selectedDogInfo;             // Reference to the selected DogInfo component
    private bool panelVisible = false;           // Track panel visibility state

    public void ShowDogInfo(DogInfo dogInfo)
    {
        Debug.Log("KennelListener: ShowDogInfo called");  // Debug to confirm selection

        selectedDogInfo = dogInfo;

        // Set the selected dog's data and unique personality in KennelManager
        kennelManager.SetSelectedDog(dogInfo.dogData, dogInfo.GetAssignedPersonality());

        // Raise event to notify UIListener of the selected dog
        onDogSelectedEvent?.RaiseEvent();

        // Show the UI panel if it's not already visible
        if (!panelVisible)
        {
            dogInfoPanel.SetActive(true);
            panelVisible = true;
            Debug.Log("KennelListener: Dog info panel activated");
        }
    }

    public void HideDogInfoPanel()
    {
        // Hide the UI panel and reset state
        if (panelVisible)
        {
            dogInfoPanel.SetActive(false);
            panelVisible = false;
            Debug.Log("KennelListener: Dog info panel deactivated");
        }
    }
}
