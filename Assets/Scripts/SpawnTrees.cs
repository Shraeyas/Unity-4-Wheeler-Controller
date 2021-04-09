using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    [SerializeField] private Transform treePrefab;
    [SerializeField] private int noOfTrees = 1000;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < noOfTrees; i++)
        {
            Instantiate(treePrefab, new Vector3(Random.Range(0, 1000), -2.590001f, Random.Range(0, 1000)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
