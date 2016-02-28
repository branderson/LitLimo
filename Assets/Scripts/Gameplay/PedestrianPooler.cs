using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PedestrianPooler : MonoBehaviour
{
    [SerializeField] private GameObject pedestrianObject;
    private static List<GameObject> pedestrianPool;
    [SerializeField] private static int startingPoolSize;

	void Start () {
        // Initialize pool
	    pedestrianPool = new List<GameObject>();
	    for (int i = 0; i < startingPoolSize; i++)
	    {
	        GameObject pedestrian = (GameObject) Instantiate(pedestrianObject);
            pedestrian.SetActive(false);
            pedestrianPool.Add(pedestrian);
	    }
	}

    public GameObject GetPedestrian()
    {
        foreach (GameObject splat in pedestrianPool.Where(splat => !splat.activeInHierarchy))
        {
            return splat;
        }

        // If everything is in use, expand pool
        GameObject pedestrian = (GameObject) Instantiate(pedestrianObject);
        pedestrianPool.Add(pedestrian);
        return pedestrian;
    }

    public List<GameObject> GetPedestrians(int count)
    {
        List<GameObject> peds = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            peds.Add(GetPedestrian());
        }
        return peds;
    }
}
