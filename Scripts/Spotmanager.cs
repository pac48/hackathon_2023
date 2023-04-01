using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotmanager : MonoBehaviour
{
    // Store data for each child empty
    public struct ChildData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public bool taken;
    }

    // Array to store data for each child empty
    private ChildData[] childData;

    // The prefab to instantiate
    public GameObject[] prefabsToSpawn;

    // The percentage of child transforms to spawn the prefab at
    [Range(0f, 100f)]
    public float spawnPercentage = 60f;

    void Start()
    {
        // Get the number of child empties and allocate space in the array
        int childCount = transform.childCount;
        childData = new ChildData[childCount];

        // Loop through each child empty and store its position and default taken value
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            childData[i].localPosition = child.localPosition;
            childData[i].localRotation = child.localRotation;
            childData[i].taken = false;
        }

        // Instantiate the prefab at a random position within the specified percentage of child transforms
        int numToSpawn = Mathf.RoundToInt(childCount * (spawnPercentage / 100f));
        for (int i = 0; i < numToSpawn; i++)
        {
            int randIndex = Random.Range(0, childCount);
            while (childData[randIndex].taken)
            {
                randIndex = Random.Range(0, childCount);
            }
            int ranIndex1 = Random.Range(0, prefabsToSpawn.Length);
            GameObject prefab = prefabsToSpawn[ranIndex1];
            childData[randIndex].taken = true;
            Vector3 spawnPos = childData[randIndex].localPosition;
            Quaternion spawnRot = childData[randIndex].localRotation;
            Instantiate(prefab, spawnPos, spawnRot);
        }
    }

    // Example function to get data for a specific child empty
    public ChildData GetChildData(int childIndex)
    {
        return childData[childIndex];
    }

    // Example function to set the taken variable for a specific child empty
    public void SetChildTaken(int childIndex, bool taken)
    {
        childData[childIndex].taken = taken;
    }



}
