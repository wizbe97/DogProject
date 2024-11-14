using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Game/Managers/GameManager")]
public class GameManagerSO : ScriptableObject
{
    public PlayerBalanceManagerSO playerBalanceManager;
    public KennelManagerSO kennelManager;
    public DogManagerSO dogManager;
}
