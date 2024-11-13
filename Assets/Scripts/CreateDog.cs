using UnityEngine;

public class CreateDog : MonoBehaviour
{
    public GameManagerSO gameManager;

    public void CreateDogObject()
    {
        DogSO selectedDogData = gameManager.kennelManager.GetSelectedDog();
        string dogName = gameManager.kennelManager.dogName;

        if (selectedDogData != null && !string.IsNullOrEmpty(dogName))
        {
            DogSO newDog = ScriptableObject.CreateInstance<DogSO>();
            newDog.breed = selectedDogData.breed;
            newDog.personality = gameManager.kennelManager.GetSelectedPersonality();
            newDog.dogName = dogName;

#if UNITY_EDITOR
            string path = $"Assets/ScriptableObjects/Dog/Dogs/PlayerDogs/{dogName}.asset";
            UnityEditor.AssetDatabase.CreateAsset(newDog, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
#endif

            Debug.Log("Dog created and named: " + newDog.dogName);

            // Deduct balance only after the dog has been successfully created
            int dogPrice = selectedDogData.breed.price;
            gameManager.playerBalanceManager.DeductBalance(dogPrice);

            // Clear the selection after the dog has been created
            gameManager.kennelManager.ClearSelectedDog();
        }
    }
}
