using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private int numOfCollectables;

    [SerializeField] private Vector3 range;

    private void Start() => SpawnCollectables();

    private void SpawnCollectables()
    {
        for (int i = 0; i < numOfCollectables; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-range.x, range.x), applePrefab.transform.position.y,
                Random.Range(-range.z, range.z));
            GameObject apple = Instantiate(applePrefab, randPos, Quaternion.identity, this.transform);
        }
    }
}