using UnityEngine;

public class PurchaseProcessor : MonoBehaviour, IGameEventListener
{
    public GameEventSO OnDogPurchased;            // Event to listen for purchase attempts
    public GameEventSO onBalanceChangedEvent;      // Event to notify balance change
    public PlayerBalanceManager playerBalanceManager; // Reference to PlayerBalanceManager

    private void OnEnable()
    {
        OnDogPurchased.RegisterListener(this); // Register to listen for purchase attempts
    }

    private void OnDisable()
    {
        OnDogPurchased.UnregisterListener(this); // Unregister when disabled
    }

    // Called when the dog purchase event is raised
    public void OnEventRaised(object parameter)
    {
        DogInfo selectedDog = parameter as DogInfo;
        if (selectedDog != null)
        {
            int dogPrice = selectedDog.dogData.breed.price;
            if (playerBalanceManager.CanAfford(dogPrice))
            {
                playerBalanceManager.DeductBalance(dogPrice); // Deduct balance
                onBalanceChangedEvent?.Raise(); // Notify listeners of the balance change
                Debug.Log("Purchase successful: " + selectedDog.dogData.dogName);
            }
            else
            {
                Debug.LogWarning("Not enough balance to purchase this dog.");
            }
        }
    }
}
