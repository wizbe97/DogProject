using UnityEngine;

[CreateAssetMenu(fileName = "KennelManager", menuName = "Game/Managers/KennelManager")]
public class KennelManagerSO : ScriptableObject
{
    public DogSO selectedDogData;
    public Personality selectedPersonality;
    public string dogName; // Store the player's chosen name here

    public void SelectDog(DogSO dogData)
    {
        selectedDogData = dogData;
    }

    public DogSO GetSelectedDog()
    {
        return selectedDogData;
    }

    public Personality GetSelectedPersonality()
    {
        return selectedPersonality;
    }

    public void SetDogName(string name)
    {
        dogName = name;
    }

    public void ClearSelectedDog()
    {
        selectedDogData = null;
        selectedPersonality = default;
        dogName = "";
    }
}
