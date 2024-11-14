using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Game/Managers/SaveManager")]
public class SaveManagerSO : ScriptableObject
{
    public GameManagerSO gameManager;


    private const string SaveFileDogPath = "OwnedDogs";
    private const string SaveFileBalancePath = "Balance";
    private string saveDirectory;

    public int currentSlot = 1;
    public int autoSaveSlot = 0;

    public void SaveOwnedDogs(bool isAutoSave = false)
    {
        CheckAutoSave();

        List<DogData> dogDataList = new List<DogData>();
        foreach (DogSO dog in gameManager.dogManager.ownedDogs)
        {
            DogData data = new DogData
            {
                dogName = dog.dogName,
                breed = dog.breed,
                personality = dog.personality,
                tricks = dog.tricks,
                bark = dog.bark
            };
            dogDataList.Add(data);
        }

        string json = JsonUtility.ToJson(new DogDataWrapper { dogs = dogDataList });
        File.WriteAllText(CombinePath(SaveFileDogPath, isAutoSave ? 0 : currentSlot), json);
        Debug.Log("Owned dogs saved to slot: " + currentSlot);
    }

    public void LoadOwnedDogs()
    {
        if (File.Exists(CombinePath(SaveFileBalancePath, currentSlot)))
        {
            string json = File.ReadAllText(CombinePath(SaveFileDogPath, currentSlot));
            DogDataWrapper dataWrapper = JsonUtility.FromJson<DogDataWrapper>(json);

            gameManager.dogManager.ownedDogs.Clear();
            foreach (DogData dogData in dataWrapper.dogs)
            {
                DogSO dog = CreateInstance<DogSO>();
                dog.dogName = dogData.dogName;
                dog.breed = dogData.breed;
                dog.personality = dogData.personality;
                dog.tricks = dogData.tricks;
                dog.bark = dogData.bark;
#if UNITY_EDITOR
                string path = $"Assets/ScriptableObjects/Dog/Dogs/PlayerDogs/{dog.dogName}.asset";
                UnityEditor.AssetDatabase.CreateAsset(dog, path);
                UnityEditor.AssetDatabase.SaveAssets();
                UnityEditor.AssetDatabase.Refresh();
#endif
                gameManager.dogManager.ownedDogs.Add(dog);
            }
            Debug.Log("Owned dogs loaded from JSON");
        }
        else
        {
            StartEmpty();
            Debug.LogWarning("No save file found, starting with empty owned dogs list.");
        }
    }

    private void StartEmpty()
    {
        gameManager.dogManager.ClearDoglist();
        gameManager.playerBalanceManager.ClearBalance();
    }
    public void SaveBalance(bool isAutoSave = false)
    {
        CheckAutoSave();

        string json = JsonUtility.ToJson(new BalanceData { balance = gameManager.playerBalanceManager.playerBalance.balance });
        File.WriteAllText(CombinePath(SaveFileBalancePath, isAutoSave ? 0 : currentSlot), json);
        Debug.Log("Balance saved to slot: " + currentSlot);
    }

    public void LoadBalance()
    {
        if (File.Exists(CombinePath(SaveFileBalancePath, currentSlot)))
        {
            string json = File.ReadAllText(CombinePath(SaveFileBalancePath, currentSlot));
            BalanceData balanceData = JsonUtility.FromJson<BalanceData>(json);
            gameManager.playerBalanceManager.playerBalance.balance = balanceData.balance;
        }
    }

    private void CheckAutoSave()
    {
        Debug.Log("CURRENT SLOT:" + currentSlot);
        //we make sure we don't override slot 0 when saving manually because slot 0 will be kept for auto saving
        if (currentSlot == 0)
        {
            //loop through slots to see if we have a empty slot to manually save
            for (int i = 1; i < 4; i++)
            {
                if (!IsDataSaved(i))
                {
                    currentSlot = i;
                    break;
                }
            }
        }
    }
    public void AutoSaveAll()
    {
        SaveOwnedDogs(isAutoSave: true);
        SaveBalance(isAutoSave: true);
    }

    public void SaveAllData()
    {
        SaveOwnedDogs();
        SaveBalance();
    }

    public void LoadAllData()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        LoadOwnedDogs();
        LoadBalance();
    }

    public bool IsDataSaved(int slot)
    {
        string combinedPath = CombinePath(SaveFileDogPath, slot);
        return File.Exists(combinedPath);
    }
    public void RemoveSlot(int slot)
    {
        string ownedDogsPath = CombinePath(SaveFileDogPath, slot);
        string balancePath = CombinePath(SaveFileBalancePath, slot);

        if (File.Exists(ownedDogsPath))
        {
            File.Delete(ownedDogsPath);
        }
        if (File.Exists(balancePath))
        {
            File.Delete(balancePath);
        }
    }
    private string CombinePath(string path, int slot)
    {
        return Path.Combine(saveDirectory, $"{path}_{slot}");
    }
    // private DogBreedSO FindBreedByName(string breedName)
    // {
    //     DogBreedSO breed = Resources.Load<DogBreedSO>($"Breeds/{breedName}");
    //     if (breed == null)
    //     {
    //         Debug.LogError($"Breed '{breedName}' not found in Resources/Dog/Breeds/. Please check the asset name and location.");
    //     }
    //     return breed;
    // }


}

[Serializable]
public class DogDataWrapper
{
    public List<DogData> dogs;
}
[Serializable]
public class DogData
{
    public string dogName;
    public DogBreedSO breed;
    public AudioClip bark;
    public Personality personality;
    public Tricks[] tricks;

}

[Serializable]
public class BalanceData
{
    public int balance;
}

