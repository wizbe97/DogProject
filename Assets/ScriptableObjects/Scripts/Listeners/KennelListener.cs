using UnityEngine;
using TMPro;

public class KennelListener : MonoBehaviour
{
    public GameEventSO onDogSelectedEvent;
    public GameEventSO onBalanceChangedEvent; 
    public KennelManager kennelManager;
    public GameManager gameManager;
    public TMP_InputField nameInputField;

    private DogInfo selectedDogInfo;

    public void ShowDogInfo(DogInfo dogInfo)
    {
        selectedDogInfo = dogInfo;
        kennelManager.SetSelectedDog(dogInfo.dogData, dogInfo.GetAssignedPersonality());
        onDogSelectedEvent?.RaiseEvent();
    }

    public void PurchaseSelectedDog()
    {
        if (selectedDogInfo == null)
        {
            Debug.Log("No dog selected for purchase.");
            return;
        }

        int dogPrice = selectedDogInfo.dogData.breed.price;

        if (gameManager.playerBalanceManager.CanAfford(dogPrice))
        {
            gameManager.playerBalanceManager.DeductBalance(dogPrice);
            onBalanceChangedEvent?.RaiseEvent();
            PromptForDogName();
        }
        else
        {
            Debug.Log("Not enough balance to purchase this dog.");
        }
    }

    private void PromptForDogName()
    {
        nameInputField.gameObject.SetActive(true);
        nameInputField.onEndEdit.AddListener(OnNameEntered);
    }

    private void OnNameEntered(string enteredName)
    {
        if (!string.IsNullOrEmpty(enteredName) && selectedDogInfo != null)
        {
            DogSO purchasedDogData = ScriptableObject.CreateInstance<DogSO>();
            purchasedDogData.breed = selectedDogInfo.dogData.breed;
            purchasedDogData.personality = selectedDogInfo.GetAssignedPersonality();
            purchasedDogData.dogName = enteredName;

#if UNITY_EDITOR
            string path = $"Assets/ScriptableObjects/Dog/Dogs/{enteredName}.asset";
            UnityEditor.AssetDatabase.CreateAsset(purchasedDogData, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif

            Debug.Log("Dog purchased and named: " + purchasedDogData.dogName);

            nameInputField.gameObject.SetActive(false);
            nameInputField.onEndEdit.RemoveListener(OnNameEntered);
            nameInputField.text = "";
        }
        else
        {
            Debug.LogWarning("Please enter a valid name for the dog.");
        }
    }
}
