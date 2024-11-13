using UnityEngine;

[CreateAssetMenu(fileName = "KennelManager", menuName = "Game/Managers/KennelManager")]
public class KennelManagerSO : ScriptableObject
{
    public DogSO selectedDogData;
    public Personality selectedPersonality;

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

    public void ClearSelectedDog()
    {
        selectedDogData = null;
        selectedPersonality = default;
    }
}
