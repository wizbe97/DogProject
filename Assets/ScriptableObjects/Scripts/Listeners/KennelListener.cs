using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class KennelListener : MonoBehaviour
{
    public PlayerBalanceManager balanceManager;     // Reference to the PlayerBalanceManager ScriptableObject
    public GameEventSO onDogPurchasedEvent;         // Event to notify other systems of a dog purchase
    public TMP_InputField nameInputField;           // UI input field for naming the dog

    private DogInfo selectedDog;
    private DogSO playerDogData;

    private void OnEnable()
    {
        if (onDogPurchasedEvent != null)
        {
            onDogPurchasedEvent.RegisterListener(OnDogPurchased);
        }
    }

    private void OnDisable()
    {
        if (onDogPurchasedEvent != null)
        {
            onDogPurchasedEvent.UnregisterListener(OnDogPurchased);
        }
    }

    public void SetSelectedDog(DogInfo dogInfo)
    {
        selectedDog = dogInfo;
    }

    public void PurchaseSelectedDog()
    {
        if (selectedDog == null)
        {
            Debug.Log("No dog selected for purchase.");
            return;
        }

        int dogPrice = selectedDog.dogData.breed.price;

        // Check balance using PlayerBalanceManager
        if (balanceManager.CanAfford(dogPrice))
        {
            balanceManager.DeductBalance(dogPrice);
            onDogPurchasedEvent?.RaiseEvent(); // Trigger purchase event
        }
        else
        {
            Debug.Log("Not enough balance to purchase this dog.");
        }
    }

    private void OnDogPurchased()
    {
        if (selectedDog == null) return;

        // Reset playerDogData to ensure it's a fresh instance
        playerDogData = null;
        playerDogData = ScriptableObject.CreateInstance<DogSO>();
        playerDogData.breed = selectedDog.dogData.breed;
        playerDogData.personality = selectedDog.GetAssignedPersonality();

        // Prompt for dog name
        PromptForDogName();
    }

    private void PromptForDogName()
    {
        nameInputField.gameObject.SetActive(true);
        nameInputField.onEndEdit.AddListener(delegate { OnNameEntered(nameInputField.text); });
        nameInputField.text = "";
    }

    private void OnNameEntered(string enteredName)
    {
        if (!string.IsNullOrEmpty(enteredName) && playerDogData != null)
        {
#if UNITY_EDITOR
            // Define the path where the new asset would be saved
            string path = $"Assets/ScriptableObjects/Dog/Dogs/PlayerDogs/{enteredName}.asset";

            // Check if an asset with this name already exists
            if (AssetDatabase.LoadAssetAtPath<DogSO>(path) != null)
            {
                Debug.LogWarning("A dog with this name already exists. Please enter a unique name.");
                nameInputField.text = ""; // Clear the input field for a new name
                return; // Exit without saving
            }

            // Set the entered name to the duplicated DogSO
            playerDogData.dogName = enteredName;

            // Ensure the directory exists
            if (!System.IO.Directory.Exists("Assets/ScriptableObjects/Dog/Dogs/PlayerDogs/"))
            {
                System.IO.Directory.CreateDirectory("Assets/ScriptableObjects/Dog/Dogs/PlayerDogs/");
            }

            // Create and save the asset
            AssetDatabase.CreateAsset(playerDogData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif

            nameInputField.gameObject.SetActive(false);  // Hide input field after naming
            Debug.Log("Dog named: " + playerDogData.dogName);

            // Reset playerDogData to avoid reusing the same instance
            playerDogData = null;
        }
        else
        {
            Debug.Log("Please enter a valid name for the dog.");
        }
    }


}
