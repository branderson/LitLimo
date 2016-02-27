using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BloodSplatPooler : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplatObject;
    private static List<GameObject> bloodSplatPool;
    [SerializeField] private static int startingPoolSize;

	// Use this for initialization
	void Start () {
        // Initialize pool
	    bloodSplatPool = new List<GameObject>();
	    for (int i = 0; i < startingPoolSize; i++)
	    {
	        GameObject bloodSplat = (GameObject) Instantiate(bloodSplatObject);
            bloodSplat.SetActive(false);
            bloodSplatPool.Add(bloodSplat);
	    }
	}

    public GameObject GetBloodSplat()
    {
        foreach (GameObject splat in bloodSplatPool.Where(splat => !splat.activeInHierarchy))
        {
            return splat;
        }

        // If everything is in use, expand pool
        GameObject bloodSplat = (GameObject) Instantiate(bloodSplatObject);
        bloodSplatPool.Add(bloodSplat);
        return bloodSplat;
    }

    public List<GameObject> GetBloodSplats(int count)
    {
        List<GameObject> splats = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            splats.Add(GetBloodSplat());
        }
        return splats;
    }
}
