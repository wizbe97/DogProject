using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Game/Managers/GameManager")]
public class GameManager : ScriptableObject
{
    public PlayerBalanceManager playerBalanceManager;
    public KennelManager kennelManager;
}
