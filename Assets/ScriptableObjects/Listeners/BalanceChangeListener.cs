using UnityEngine;
using TMPro;

public class BalanceChangeListener : MonoBehaviour, IGameEventListener
{
    public GameEventSO onBalanceChangedEvent;   // Event for balance changes
    public PlayerBalanceManager playerBalanceManager; // Reference to access the balance
    public TMP_Text balanceText;                // UI Text to display the balance

    private void OnEnable()
    {
        onBalanceChangedEvent.RegisterListener(this); // Register for balance change events
    }

    private void OnDisable()
    {
        onBalanceChangedEvent.UnregisterListener(this); // Unregister when disabled
    }

    // Called when the balance change event is raised
    public void OnEventRaised(object parameter)
    {
        UpdateBalanceDisplay();
    }

    private void UpdateBalanceDisplay()
    {
        balanceText.text = "Balance: $" + playerBalanceManager.playerBalance.balance;
    }
}
