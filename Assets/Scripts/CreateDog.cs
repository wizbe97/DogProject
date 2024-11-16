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
            newDog.gender = gameManager.kennelManager.GetSelectedGender();
            newDog.dogName = dogName;
            Debug.Log("Dog created and named: " + newDog.dogName);

            int dogPrice = selectedDogData.breed.price;
            gameManager.playerBalanceManager.DeductBalance(dogPrice);
            gameManager.dogManager.AddDog(newDog);
            gameManager.saveManager.AutoSaveAll();

            gameManager.kennelManager.ClearSelectedDog();
            FindAnyObjectByType<UIManager>().HideDogInfoPanel();
        }
    }

}
