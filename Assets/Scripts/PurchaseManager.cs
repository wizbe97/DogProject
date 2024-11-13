using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void AttemptPurchaseSelectedDog()
    {
        DogSO selectedDogData = gameManager.kennelManager.GetSelectedDog();
        if (selectedDogData != null)
        {
            AttemptPurchaseDog(selectedDogData);
        }
    }

    // For Dog Purchases
    private void AttemptPurchaseDog(DogSO selectedDogData)
    {
        int dogPrice = selectedDogData.breed.price;
        if (gameManager.playerBalanceManager.CanAfford(dogPrice))
        {
            gameManager.playerBalanceManager.DeductBalance(dogPrice);
            Debug.Log("Purchase successful! Dog purchased: " + selectedDogData.dogName);
            gameManager.kennelManager.ClearSelectedDog(); // Clear selection after purchase
        }
        else
        {
            Debug.LogWarning("Not enough balance to purchase this dog.");
        }
    }
    // Add More Purchase Options Here
}
