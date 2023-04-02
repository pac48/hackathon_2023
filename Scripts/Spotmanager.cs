using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spotmanager : MonoBehaviour
{
    public Transform player;
    // The range in which the character can ticket an empty spot
     public float ticketRange = 2f;
    public int score = 0;
    private int ticketedSpotIndex = -1;
    private float ticketTimeRemaining = 0f;
    public float ticketTime = 5f;

    public float timeToSelect = 10f;
    private Slider[] spotSliders;
    public float spawnRate = .05f;

    public struct ChildData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public bool taken;
        public bool ticketed;
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
            ParkingController parkingController = prefab.GetComponent<ParkingController>();
            parkingController.setTransformTarget(childData[0].localPosition);
            
            childData[randIndex].taken = true;
            Vector3 spawnPos = childData[randIndex].localPosition;
            Quaternion spawnRot = childData[randIndex].localRotation;
            Instantiate(prefab, spawnPos, spawnRot);
        }

        //timers for the sliders behind the spots
        spotSliders = new Slider[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            spotSliders[i] = child.GetComponentInChildren<Slider>();
            spotSliders[i].gameObject.SetActive(false);
            childData[i].localPosition = child.localPosition;
            childData[i].localRotation = child.localRotation;
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

    public void SetChildTicketed(int ticketedSpotIndex, bool ticketed) {
        childData[ticketedSpotIndex].ticketed = ticketed;
    }
    

    private void Update()
    {
        float spawnChance = spawnRate * Time.deltaTime;
    
        if (Random.value < spawnChance)
        {
            int randIndex = Random.Range(0, transform.childCount);
            Transform spot = transform.GetChild(randIndex);
        
            if (!GetChildData(randIndex).ticketed && randIndex != ticketedSpotIndex)
            {
                // Start ticketing the spot if not already taken or ticketed
                ticketedSpotIndex = randIndex;
                ticketTimeRemaining = ticketTime;
                spotSliders[ticketedSpotIndex].gameObject.SetActive(true);
            }
        }

        if (ticketedSpotIndex != -1)
        {
            Transform spot = transform.GetChild(ticketedSpotIndex);

            if (ticketTimeRemaining <= 0f)
            {
                // Ticket time expired
                SetChildTicketed(ticketedSpotIndex, true);
                ticketedSpotIndex = -1;
                ticketTimeRemaining = 0f;
                spotSliders[ticketedSpotIndex].gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.transform.position, spot.position) <= ticketRange)
            {
                // Ticket the spot
                SetChildTicketed(ticketedSpotIndex, true);
                score += 50;

                // Reset ticketing state
                ticketedSpotIndex = -1;
                ticketTimeRemaining = 0f;
                spotSliders[ticketedSpotIndex].gameObject.SetActive(false);
            }
            else
            {
                // Update the ticket time remaining and slider
                ticketTimeRemaining -= Time.deltaTime;
                spotSliders[ticketedSpotIndex].value = ticketTimeRemaining / ticketTime;
            }
        }
    }
    
}





