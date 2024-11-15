using UnityEngine;

public class SpawnButtonHandler : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void SpawnAllDogs()
    {
        if (gameManager != null)
        {
            gameManager.spawnPetManager.SpawnAllDogs(gameManager.dogManager);
            Debug.Log("All dogs have been spawned!");
        }
        else
        {
            Debug.LogError("SpawnPetManager or GameManager is not assigned!");
        }
    }
}
