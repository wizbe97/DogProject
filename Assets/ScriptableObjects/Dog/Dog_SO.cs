using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName = "New Dog", menuName = "Dog/Dog")]
public class DogSO : ScriptableObject
{
    [Header("Dog Details")]
    public string dogName;
    public DogGender gender;
    public DogBreedSO breed;

    [Header("Audio")]
    public AudioClip bark;

    [Header("Personality & Abilities")]
    public Personality personality;
    public Tricks[] tricks;

    [Header("Dog Stats")]
    public DogStatsSO stats;

}

public enum Personality
{
    Playful,
    Lazy,
    Obedient,
    Curious,
    Clumsy,
    Naughty,
    Clever
}

public enum Tricks
{
    Sit,
    Shake,
    RollOver,
    PlayDead,
    Beg,
    Spin,
    Speak,
    Dance,
    Flip
}

public enum DogGender
{
    Male,
    Female
}

