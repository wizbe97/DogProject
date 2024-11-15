using UnityEngine;

public class DogComponent : MonoBehaviour
{
    private DogData dogData;

    public void InitializeFromData(DogData data)
    {
        dogData = data;

        // Optionally log details
        Debug.Log($"Initialized DogComponent with Name: {dogData.dogName}, Breed: {dogData.breed.breedName}");
    }

    public DogData GetDogData()
    {
        return dogData;
    }
}
