using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveManager", menuName = "Game/Managers/SaveManager")]
public class SaveManagerSO : ScriptableObject
{
    public GameManagerSO gameManager;


    private const string SaveFileDogPath = "OwnedDogs";
    private const string SaveFileCatPath = "OwnedCats";
    private const string SaveFileBalancePath = "Balance";
    private string saveDirectory;

    public int currentSlot = 1;

    public void SaveOwnedDogs(bool isAutoSave = false)
    {
        CheckAutoSave();

        List<DogData> dogDataList = new List<DogData>();
        foreach (DogData dog in gameManager.dogManager.ownedDogs)
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
    }
    public void SaveOwnedCats(bool isAutoSave = false)
    {
        CheckAutoSave();

        List<CatData> catDataList = new List<CatData>();
        foreach (CatData cat in gameManager.catManager.ownedCats)
        {
            CatData data = new CatData
            {
                catName = cat.catName,
                breed = cat.breed,
                personality = cat.personality,
            };
            catDataList.Add(data);
        }

        string json = JsonUtility.ToJson(new CatDataWrapper { cats = catDataList });
        File.WriteAllText(CombinePath(SaveFileCatPath, isAutoSave ? 0 : currentSlot), json);
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
                gameManager.dogManager.ownedDogs.Add(dogData);
            }
        }

        else
            StartEmpty();
    }
    public void LoadOwnedCats()
    {
        if (File.Exists(CombinePath(SaveFileCatPath, currentSlot)))
        {
            string json = File.ReadAllText(CombinePath(SaveFileCatPath, currentSlot));
            CatDataWrapper dataWrapper = JsonUtility.FromJson<CatDataWrapper>(json);

            gameManager.catManager.ownedCats.Clear();
            foreach (CatData catData in dataWrapper.cats)
            {
                CatSO cat = CreateInstance<CatSO>();
                cat.catName = catData.catName;
                cat.breed = catData.breed;
                cat.personality = catData.personality;

                gameManager.catManager.ownedCats.Add(catData);
            }
        }

        else
            StartEmpty();
    }

    private void StartEmpty()
    {
        gameManager.catManager.ClearCatData();
        gameManager.dogManager.ClearDoglist();
        gameManager.playerBalanceManager.ClearBalance();
    }

    public void SaveBalance(bool isAutoSave = false)
    {
        CheckAutoSave();

        string json = JsonUtility.ToJson(new BalanceData { balance = gameManager.playerBalanceManager.playerBalance.balance });
        File.WriteAllText(CombinePath(SaveFileBalancePath, isAutoSave ? 0 : currentSlot), json);
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
        SaveOwnedCats(isAutoSave: true);
        SaveBalance(isAutoSave: true);

        SaveTimestamp(0); // Slot 0 for autosave
    }

    private void SaveTimestamp(int slot)
    {
        string timestampPath = CombinePath($"Slot_{slot}_time", 0);
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        File.WriteAllText(timestampPath, timestamp);
    }

    public string GetSaveTime(int slot)
    {
        string timestampPath = CombinePath($"Slot_{slot}_time", 0);

        if (File.Exists(timestampPath))
            return File.ReadAllText(timestampPath);
        else
            return "No save time available.";

    }

    public void SaveAllData()
    {
        SaveOwnedDogs();
        SaveOwnedCats();
        SaveBalance();

        SaveTimestamp(currentSlot);
    }

    public void LoadAllData()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        LoadOwnedDogs();
        LoadOwnedCats();
        LoadBalance();
    }

    public bool IsDataSaved(int slot)
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        string combinedPath = CombinePath(SaveFileDogPath, slot);
        return File.Exists(combinedPath);
    }

    public void RemoveSlot(int slot)
    {
        string[] paths =
        {
        CombinePath(SaveFileDogPath, slot),
        CombinePath(SaveFileCatPath, slot),
        CombinePath(SaveFileBalancePath, slot)
    };

        foreach (string path in paths)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    private string CombinePath(string path, int slot)
    {
        return Path.Combine(saveDirectory, $"{path}_{slot}");
    }
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
    public DogGender gender;
    public DogBreedSO breed;
    public AudioClip bark;
    public Personality personality;
    public Tricks[] tricks;
}

[Serializable]
public class CatDataWrapper
{
    public List<CatData> cats;
}
[Serializable]

public class CatData
{
    public string catName;
    public CatBreedSO breed;
    public CatPersonality personality;

}

[Serializable]
public class BalanceData
{
    public int balance;
}

