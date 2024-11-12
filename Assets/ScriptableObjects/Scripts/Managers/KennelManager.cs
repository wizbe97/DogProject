using UnityEngine;

[CreateAssetMenu(fileName = "KennelManager", menuName = "Game/Managers/KennelManager")]
public class KennelManager : ScriptableObject
{
    public DogSO selectedDogData;
    public Personality selectedPersonality;

    public void SetSelectedDog(DogSO dogData, Personality personality)
    {
        selectedDogData = dogData;
        selectedPersonality = personality;
    }
}
