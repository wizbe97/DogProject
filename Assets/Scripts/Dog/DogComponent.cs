using UnityEngine;

public class DogComponent : MonoBehaviour
{
    private DogData dogData;

    public void InitializeFromData(DogData data)
    {
        dogData = data;
    }

    public DogData GetDogData()
    {
        return dogData;
    }
}
