using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManagerSO gameManager;
    public GameEventSO dogNameReceivedEvent;

    [Header("Dog Info UI")]
    public TMP_InputField nameInputField;
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
        nameInputField.gameObject.SetActive(false);
    }

    public void ShowNameInputPrompt()
    {
        // Enable the input field and ensure it's active
        nameInputField.gameObject.SetActive(true);

        // Activate the input field to prompt the keyboard
        nameInputField.ActivateInputField();

        // Add listener for when the player finishes typing
        nameInputField.onEndEdit.AddListener(OnNameEntered);
    }


    private void OnNameEntered(string enteredName)
    {
        if (!string.IsNullOrEmpty(enteredName))
        {
            gameManager.kennelManager.SetDogName(enteredName);
            dogNameReceivedEvent.Raise(); // Trigger dog creation in CreateDog

            Debug.Log("Dog name received: " + enteredName);
            nameInputField.gameObject.SetActive(false);
            nameInputField.onEndEdit.RemoveListener(OnNameEntered);
            nameInputField.text = "";
        }
        else
        {
            Debug.LogWarning("Please enter a valid name for the dog.");
        }
    }

    public void UpdateBalanceUI()
    {
        balanceText.text = "Balance: $" + gameManager.playerBalanceManager.playerBalance.balance;
    }
}
