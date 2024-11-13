using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameManagerSO gameManager;
    public GameEventSO onPurchaseEvent;

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
            gameManager.playerBalanceManager.DeductBalance(dogPrice);
            gameManager.kennelManager.ClearSelectedDog();
            Debug.Log("Dog attempt purchase event received and alerting UI Manager and Create Dog");

            // Raise event to notify UIManager and CreateDog
            onPurchaseEvent.Raise();
        }
    }
}
