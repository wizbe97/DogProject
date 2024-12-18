using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dog Breed", menuName = "Dog/Breed")]
public class DogBreedSO : ScriptableObject
{
    [Header("Breed Details")]
    public string breedName;
    [TextArea(3, 10)] public string description;
    public int price;

    [Header("Breed Appearance")]
    public Sprite portrait;
    public Sprite appearance;
}
