using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBalanceManager", menuName = "Game/PlayerBalanceManager")]
public class PlayerBalanceManager : ScriptableObject
{
    public PlayerBalanceSO playerBalance;
    public GameEventSO onBalanceChangedEvent; // Event to notify balance changes

    // Check if the player can afford an item based on its cost
    public bool CanAfford(int amount)
    {
        return playerBalance.balance >= amount;
    }

    // Deducts the balance if affordable and raises the balance change event
    public void DeductBalance(int amount)
    {
        if (CanAfford(amount))
        {
            playerBalance.balance -= amount;
            onBalanceChangedEvent?.Raise(); // Raise event to notify balance change
        }
    }
}
