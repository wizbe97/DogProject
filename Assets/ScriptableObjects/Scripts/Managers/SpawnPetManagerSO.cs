using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPetManager", menuName = "Managers/SpawnPetManager")]
public class SpawnPetManagerSO : ScriptableObject
{
    public GameObject dogPrefab; // Reference to the prefab template

    public void SpawnAllDogs(DogManagerSO dogManager)
    {
        if (dogPrefab == null)
        {
            Debug.LogError("Dog prefab is not assigned in SpawnPetManager!");
            return;
        }

        foreach (var dogData in dogManager.ownedDogs)
        {
            SpawnDog(dogData);
        }
    }

    private void SpawnDog(DogData dogData)
    {
        // Instantiate the prefab
        GameObject newDog = Instantiate(dogPrefab);
        newDog.name = dogData.dogName; // Set the GameObject name

        // Set the sprite
        SpriteRenderer spriteRenderer = newDog.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = dogData.breed.appearance;
        }
        else
        {
            Debug.LogError("Dog prefab is missing a SpriteRenderer component!");
        }

        // Optionally set audio
        AudioSource audioSource = newDog.GetComponent<AudioSource>();
        if (audioSource != null && dogData.bark != null)
        {
            audioSource.clip = dogData.bark;
        }

        // Attach additional data via DogComponent
        DogComponent dogComponent = newDog.GetComponent<DogComponent>();
        if (dogComponent != null)
        {
            dogComponent.InitializeFromData(dogData);
        }
        else
        {
            Debug.LogError("Dog prefab is missing a DogComponent script!");
        }

        Debug.Log($"Spawned Dog: {dogData.dogName}");
    }
}