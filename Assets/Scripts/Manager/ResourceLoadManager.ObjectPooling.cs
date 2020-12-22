using System.Collections.Generic;
using UnityEngine;

public partial class ResourceLoadManager
{
    [SerializeField] private GameObject bloodPrefab;
    private List<GameObject> bloods = new List<GameObject>();

    private void LoadPrefabs()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(bloodPrefab);
            obj.transform.SetParent(transform);
            bloods.Add(obj);
            obj.SetActive(false);
        }
    }

    public void ShowBlood(Transform t)
    {
        for(int i = 0; i < bloods.Count; i++)
        {
            if (!bloods[i].activeSelf)
            {
                bloods[i].transform.position = t.position;
                bloods[i].SetActive(true);
                break;
            }
        }
    }
}
