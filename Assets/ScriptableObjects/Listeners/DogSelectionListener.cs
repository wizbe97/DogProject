using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DogSelectionListener : MonoBehaviour, IGameEventListener
{
    public GameEventSO OnDogSelected;    // Event triggered when a dog is selected
    public GameObject dogInfoPanel;      // Panel to show dog info
    public TMP_Text breedText;           // UI Text for breed information
    public Image portraitUI;             // UI Image for the dog's portrait
    public TMP_Text personalityText;     // UI Text for personality information
    public TMP_Text priceText;           // UI Text for price information

    private DogInfo selectedDogInfo; // Store reference to the selected dog

    private void OnEnable()
    {
        OnDogSelected.RegisterListener(this); // Register for dog selection events
    }

    private void OnDisable()
    {
        OnDogSelected.UnregisterListener(this); // Unregister when disabled
    }

    // Called when the dog selection event is raised
    public void OnEventRaised(object parameter)
    {
        selectedDogInfo = parameter as DogInfo;
        if (selectedDogInfo != null)
        {
            Debug.Log("Dog selected: " + selectedDogInfo.dogData.dogName);
            ShowDogInfoPanel(selectedDogInfo); // Update the UI
        }
    }

    private void ShowDogInfoPanel(DogInfo dogInfo)
    {
        dogInfoPanel.SetActive(true); // Display the dog info panel
        UpdateDogInfo(dogInfo);       // Populate panel with the selected dog's info
    }

    private void UpdateDogInfo(DogInfo dogInfo)
    {
        // Update UI elements with the selected dog's data
        portraitUI.sprite = dogInfo.dogData.breed.portrait;
        breedText.text = $"Breed: {dogInfo.dogData.breed.breedName}\nDescription: {dogInfo.dogData.breed.description}";
        personalityText.text = $"Personality: {dogInfo.GetAssignedPersonality()}";
        priceText.text = "Price: $" + dogInfo.dogData.breed.price;
    }

    // Expose the selected dog to other scripts
    public DogInfo GetSelectedDog()
    {
        return selectedDogInfo;
    }

    public void HideDogInfoPanel()
    {
        // Hide the dog info panel
        dogInfoPanel.SetActive(false);
    }
}
