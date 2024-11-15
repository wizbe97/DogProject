using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DogManager", menuName = "Managers/DogManager")]
public class DogManagerSO : ScriptableObject
{
    public List<DogSO> ownedDogs = new List<DogSO>();
    public List<DogData> ownedDogsData = new List<DogData>();

    public void AddDog(DogSO dog)
    {
        ownedDogs.Add(dog);

        DogData data = new DogData
        {
            dogName = dog.dogName,
            breed = dog.breed,
            personality = dog.personality,
            tricks = dog.tricks,
            bark = dog.bark
        };

        ownedDogsData.Add(data);
    }

    public List<DogSO> GetAllDogs()
    {
        return new List<DogSO>(ownedDogs);
    }
    public List<DogData> GetAllDogsData()
    {
        return new List<DogData>(ownedDogsData);
    }

    public void ClearDoglist()
    {
        ownedDogs.Clear();
        ownedDogsData.Clear();
    }
}