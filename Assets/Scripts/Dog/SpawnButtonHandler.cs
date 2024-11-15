using UnityEngine;

public class SpawnButtonHandler : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void SpawnAllDogs()
    {

        if (gameManager == null)
        {
            Debug.LogError("GameManagerSO is not assigned!");
            return;
        }

        if (gameManager.dogManager == null)
        {
            Debug.LogError("DogManagerSO is not assigned in GameManager!");
            return;
        }

        gameManager.spawnPetManager.SpawnAllDogs(gameManager.dogManager);
        Debug.Log("All dogs have been spawned!");
    }
}
