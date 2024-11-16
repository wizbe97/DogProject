using UnityEngine;

[CreateAssetMenu(fileName = "KennelManager", menuName = "Game/Managers/KennelManager")]
public class KennelManagerSO : ScriptableObject
{
    public DogSO selectedDogData;
    public Personality selectedPersonality;
    public DogGender selectedGender;
    [HideInInspector] public string dogName;

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

    public DogGender GetSelectedGender()
    {
        return selectedGender;
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
