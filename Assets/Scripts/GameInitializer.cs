using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public SaveManagerSO saveManager;

    private void Start()
    {
        //saveManager.LoadOwnedDogs();      //Load only dogs

        saveManager.LoadAllData();          //Load all data
    }

    private void OnApplicationQuit()
    {
        // saveManager.SaveOwnedDogs();     // option to Save only dogs
        saveManager.AutoSaveAll();          //SAve All data
    }
}

