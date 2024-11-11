using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public KennelListener kennelListener;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectDog(DogInfo dogInfo)
    {
        kennelListener.SetSelectedDog(dogInfo); // Pass selected dog to KennelListener
    }

    public void PurchaseSelectedDog()
    {
        kennelListener.PurchaseSelectedDog();  // Delegate purchasing to KennelListener
    }
}
