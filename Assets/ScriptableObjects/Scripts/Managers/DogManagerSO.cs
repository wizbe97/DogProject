using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DogManager", menuName = "Managers/DogManager")]
public class DogManagerSO : ScriptableObject
{
    public List<DogData> ownedDogs = new List<DogData>();

    public void AddDog(DogSO dog)
    {
        DogData data = new DogData
        {
            dogName = dog.dogName,
            breed = dog.breed,
            personality = dog.personality,
            tricks = dog.tricks,
            bark = dog.bark
        };

        ownedDogs.Add(data);
    }

    public List<DogData> GetAllDogsData()
    {
        return new List<DogData>(ownedDogs);
    }

    public void ClearDoglist()
    {
        ownedDogs.Clear();
    }
}