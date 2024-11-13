using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManagerSO gameManager;

    [Header("Dog Info UI")]
    public GameObject dogInfoPanel;
    public TMP_Text breedText;
    public Image portraitUI;
    public TMP_Text personalityText;
    public TMP_Text priceText;

    [Header("Balance UI")]
    public TMP_Text balanceText;

    private void Awake()
    {
        UpdateBalanceUI();
    }

    public void ShowDogInfoPanel()
    {
        DogSO selectedDogData = gameManager.kennelManager.GetSelectedDog();
        Personality selectedPersonality = gameManager.kennelManager.GetSelectedPersonality();

        if (selectedDogData != null)
        {
            breedText.text = $"Breed: {selectedDogData.breed.breedName}\nDescription: {selectedDogData.breed.description}";
            personalityText.text = "Personality: " + selectedPersonality;
            priceText.text = "Price: $" + selectedDogData.breed.price;
            portraitUI.sprite = selectedDogData.breed.portrait;
            dogInfoPanel.SetActive(true);
        }
    }

    public void HideDogInfoPanel()
    {
        dogInfoPanel.SetActive(false);
    }

    public void UpdateBalanceUI()
    {
        balanceText.text = "Balance: $" + gameManager.playerBalanceManager.playerBalance.balance;
    }
}
