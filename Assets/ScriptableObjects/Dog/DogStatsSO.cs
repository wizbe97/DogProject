using UnityEngine;

[CreateAssetMenu(fileName = "New Dog Stats", menuName = "Dog/Dog Stats")]
public class DogStatsSO : ScriptableObject
{
    [Header("Stats")]
    [Range(0, 100)] public float hunger;
    [Range(0, 100)] public float thirst;
    [Range(0, 100)] public float energy;
    [Range(0, 100)] public float happiness;
    [Range(0, 100)] public float hygiene;

    /// <summary>
    /// Resets all stats to default values.
    /// </summary>
    public void ResetStats()
    {
        hunger = 100;
        thirst = 100;
        energy = 100;
        happiness = 100;
        hygiene = 100;
    }

    /// <summary>
    /// Decreases a stat over time, clamped to 0.
    /// </summary>
    /// <param name="statName">The stat to decrease.</param>
    /// <param name="amount">The amount to decrease by.</param>
    public void DecreaseStat(string statName, float amount)
    {
        switch (statName)
        {
            case "hunger":
                hunger = Mathf.Max(0, hunger - amount);
                break;
            case "thirst":
                thirst = Mathf.Max(0, thirst - amount);
                break;
            case "energy":
                energy = Mathf.Max(0, energy - amount);
                break;
            case "happiness":
                happiness = Mathf.Max(0, happiness - amount);
                break;
            case "hygiene":
                hygiene = Mathf.Max(0, hygiene - amount);
                break;
            default:
                Debug.LogWarning($"Invalid stat name: {statName}");
                break;
        }
    }

    /// <summary>
    /// Increases a stat, clamped to 100.
    /// </summary>
    /// <param name="statName">The stat to increase.</param>
    /// <param name="amount">The amount to increase by.</param>
    public void IncreaseStat(string statName, float amount)
    {
        switch (statName)
        {
            case "hunger":
                hunger = Mathf.Min(100, hunger + amount);
                break;
            case "thirst":
                thirst = Mathf.Min(100, thirst + amount);
                break;
            case "energy":
                energy = Mathf.Min(100, energy + amount);
                break;
            case "happiness":
                happiness = Mathf.Min(100, happiness + amount);
                break;
            case "hygiene":
                hygiene = Mathf.Min(100, hygiene + amount);
                break;
            default:
                Debug.LogWarning($"Invalid stat name: {statName}");
                break;
        }
    }
}
