using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DogInfo : MonoBehaviour
{
    public DogSO dogData;
    public Image portraitUI;
    public TMP_Text breedText;
    public TMP_Text personalityText;
    public TMP_Text priceText;

    private Personality assignedPersonality;
    private bool personalityAssigned = false;

    private void Start()
    {
        if (!personalityAssigned)
        {
            assignedPersonality = (Personality)Random.Range(0, System.Enum.GetValues(typeof(Personality)).Length);
            personalityAssigned = true;
        }
    }

    private void OnMouseDown()
    {
        GameManager.Instance.SelectDog(this); // Notifies GameManager of the selected dog
        DisplayDogInfo();
    }

    private void DisplayDogInfo()
    {
        if (dogData != null)
        {
            portraitUI.sprite = dogData.breed.portrait;
            breedText.text = $"Breed: {dogData.breed.breedName}\nDescription: {dogData.breed.description}";
            personalityText.text = $"Personality: {assignedPersonality}";
            priceText.text = "Price: $" + dogData.breed.price;
        }
    }

    public Personality GetAssignedPersonality()
    {
        return assignedPersonality;
    }

}
