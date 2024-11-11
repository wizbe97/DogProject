using UnityEngine;
using TMPro;

public class UIListener : MonoBehaviour
{
    [SerializeField] private PlayerBalanceManager balanceManager;
    [SerializeField] private GameEventSO onBalanceChangedEvent;
    [SerializeField] private TMP_Text balanceText;

    private void OnEnable()
    {
        if (onBalanceChangedEvent != null)
        {
            onBalanceChangedEvent.RegisterListener(UpdateBalanceText);
        }
    }

    private void OnDisable()
    {
        if (onBalanceChangedEvent != null)
        {
            onBalanceChangedEvent.UnregisterListener(UpdateBalanceText);
        }
    }

    private void Start()
    {
        UpdateBalanceText();
    }

    private void UpdateBalanceText()
    {
        if (balanceText != null && balanceManager != null)
        {
            balanceText.text = "$" + balanceManager.playerBalance.balance;
        }
    }
}
