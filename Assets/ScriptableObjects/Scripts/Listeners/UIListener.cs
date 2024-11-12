using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIListener : MonoBehaviour
{
    [Header("UI Elements")]
    public GameEventSO onDogSelectedEvent;   // Event to trigger when a dog is selected
    public GameObject dogInfoPanel;          // The UI panel for dog information
    public GameObject closeDogInfoPanel;
    public TMP_Text breedText;
    public Image portraitUI;
    public TMP_Text personalityText;
    public TMP_Text priceText;
    public KennelManager kennelManager;      // Reference to KennelManager

    private void OnEnable()
    {
        if (onDogSelectedEvent != null)
        {
            onDogSelectedEvent.RegisterListener(ShowDogInfoPanel);
        }
    }

    private void OnDisable()
    {
        if (onDogSelectedEvent != null)
        {
            onDogSelectedEvent.UnregisterListener(ShowDogInfoPanel);
        }
    }

    private void ShowDogInfoPanel()
    {
        dogInfoPanel.SetActive(true);
        closeDogInfoPanel.SetActive(true);

        // Update the UI
        UpdateDogInfo();
    }

    private void UpdateDogInfo()
    {
        DogSO selectedDog = kennelManager.selectedDogData;

        if (selectedDog != null)
        {
            portraitUI.sprite = selectedDog.breed.portrait;
            breedText.text = $"Breed: {selectedDog.breed.breedName}\nDescription: {selectedDog.breed.description}";
            personalityText.text = $"Personality: {kennelManager.selectedPersonality}";
            priceText.text = "Price: $" + selectedDog.breed.price;
        }
    }

    public void HideDogInfoPanel()
    {
        dogInfoPanel.SetActive(false);
        closeDogInfoPanel.SetActive(false);
    }
}
