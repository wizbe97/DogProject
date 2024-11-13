using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameManagerSO gameManager;
    public GameEventSO onPurchaseEvent; // Event to trigger naming and creation

    public void AttemptPurchaseSelectedDog()
    {
        DogSO selectedDogData = gameManager.kennelManager.GetSelectedDog();
        if (selectedDogData != null)
        {
            AttemptPurchaseDog(selectedDogData);
        }
    }

    private void AttemptPurchaseDog(DogSO selectedDogData)
    {
        int dogPrice = selectedDogData.breed.price;
        if (gameManager.playerBalanceManager.CanAfford(dogPrice))
        {
            // Raise the event to prompt naming the dog without deducting balance yet
            onPurchaseEvent.Raise();
            Debug.Log("Purchase can proceed. Awaiting dog name input.");
        }
        else
        {
            Debug.LogWarning("Not enough balance to purchase this dog.");
        }
    }
}
