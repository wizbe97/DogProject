using UnityEngine;

public class DogPurchaseListener : MonoBehaviour, IGameEventListener
{
    public GameEventSO OnDogPurchased;             // Event triggered when a dog is purchased
    public DogSelectionListener dogSelectionListener; // Reference to DogSelectionListener to access selected dog
    public GameManager gameManager;               // Reference to GameManager to access PlayerBalanceManager

    private void OnEnable()
    {
        OnDogPurchased.RegisterListener(this); // Register for purchase events
    }

    private void OnDisable()
    {
        OnDogPurchased.UnregisterListener(this); // Unregister when disabled
    }

    // Called when the purchase event is raised
    public void OnEventRaised(object parameter)
    {
        DogInfo selectedDog = dogSelectionListener.GetSelectedDog();
        if (selectedDog != null)
        {
            int dogPrice = selectedDog.dogData.breed.price;
            Debug.Log("Attempting to purchase dog: " + selectedDog.dogData.dogName);

            // Check if the player can afford the dog and update balance
            if (gameManager.playerBalanceManager.CanAfford(dogPrice))
            {
                gameManager.playerBalanceManager.DeductBalance(dogPrice);
                Debug.Log("Purchase successful! Dog purchased: " + selectedDog.dogData.dogName);
            }
            else
            {
                Debug.LogWarning("Not enough balance to purchase this dog.");
            }
        }
        else
        {
            Debug.LogWarning("No dog selected for purchase.");
        }
    }
}
