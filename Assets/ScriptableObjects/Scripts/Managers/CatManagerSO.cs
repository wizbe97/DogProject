using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatManager", menuName = "Managers/CatManager")]
public class CatManagerSO : ScriptableObject
{
    public List<CatData> ownedCats = new List<CatData>();

    public void AddDog(CatSO cat)
    {

        CatData data = new CatData
        {
            catName = cat.catName,
            breed = cat.breed,
            personality = cat.personality,
        };
        ownedCats.Add(data);

    }

    public List<CatData> GetAllCatData()
    {
        return new List<CatData>(ownedCats);
    }

    public void ClearCatData()
    {
        ownedCats.Clear();
    }
}