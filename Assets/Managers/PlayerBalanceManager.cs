using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBalanceManager", menuName = "Game/PlayerBalanceManager")]
public class PlayerBalanceManager : ScriptableObject
{
    public PlayerBalanceSO playerBalance;
    public GameEventSO onBalanceChangedEvent;

    public bool CanAfford(int amount)
    {
        return playerBalance.balance >= amount;
    }

    public void DeductBalance(int amount)
    {
        if (CanAfford(amount))
        {
            playerBalance.balance -= amount;
            onBalanceChangedEvent?.RaiseEvent();
        }
        else
        {
            Debug.Log("Not enough balance to buy this dog.");
        }
    }
}
