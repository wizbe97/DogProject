using UnityEngine;

[CreateAssetMenu(fileName = "New Cat", menuName = "Cat/Cat")]
public class CatSO : ScriptableObject
{
    [Header("Cat Details")]
    public string catName;
    public CatBreedSO breed;
    

    [Header("Personality & Abilities")]
    public CatPersonality personality;
}

public enum CatPersonality
{
    Playful,
    Lazy,
    Obedient,
    Curious,
    Clumsy,
    Naughty,
    Clever
}
