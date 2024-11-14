using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DogManager", menuName = "Managers/DogManager")]
public class DogManagerSO : ScriptableObject
{
    public List<DogSO> ownedDogs = new List<DogSO>();

    public void AddDog(DogSO dog)
    {
        ownedDogs.Add(dog);
    }

    public List<DogSO> GetAllDogs()
    {
        return new List<DogSO>(ownedDogs);
    }
}