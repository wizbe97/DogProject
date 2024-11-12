using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIListener : MonoBehaviour
{
    [Header("UI Elements")]
    public GameEventSO onDogSelectedEvent;
    public GameEventSO onBalanceChangedEvent;  
    public GameObject dogInfoPanel;
    public TMP_Text breedText;
    public Image portraitUI;
    public TMP_Text personalityText;
    public TMP_Text priceText;
    public TMP_Text balanceText;
    public KennelManager kennelManager; 
    public PlayerBalanceManager playerBalanceManager;  

    private void OnEnable()
    {
        if (onDogSelectedEvent != null)
        {
            onDogSelectedEvent.RegisterListener(ShowDogInfoPanel);
        }
        if (onBalanceChangedEvent != null)
        {
            onBalanceChangedEvent.RegisterListener(UpdateBalanceDisplay);
        }
    }

    private void OnDisable()
    {
        if (onDogSelectedEvent != null)
        {
            onDogSelectedEvent.UnregisterListener(ShowDogInfoPanel);
        }
        if (onBalanceChangedEvent != null)
        {
            onBalanceChangedEvent.UnregisterListener(UpdateBalanceDisplay);
        }
    }

    private void ShowDogInfoPanel()
    {
        dogInfoPanel.SetActive(true);
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

    private void UpdateBalanceDisplay()
    {
        balanceText.text = "Balance: $" + playerBalanceManager.playerBalance.balance;
    }

    public void HideDogInfoPanel()
    {
        // Deactivate the panel
        dogInfoPanel.SetActive(false);
    }
}
