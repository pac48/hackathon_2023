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

    
    public GameObject slider1;
    public float spawnRate = .05f;
    public float offset1;
    public Vector3 SpawnLocation;
    public int numToSpawn;

    public struct ChildData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public bool taken;
        public bool ticketed;
    }
    public struct SliderStruct
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public GameObject slider;
        public Transform transform;
        public float finTicketTime;
    
    }
    

    // Array to store data for each child empty
    private ChildData[] childData;
    private SliderStruct[] Sliderstruct;
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
        Sliderstruct = new SliderStruct[childCount];
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            childData[i].localPosition = child.localPosition;
            childData[i].localRotation = child.localRotation;
            childData[i].taken = false;
            SpawnLocation = child.TransformDirection(Vector3.up) * offset1;
            Sliderstruct[i].slider = Instantiate(slider1, child.transform.position + SpawnLocation, child.transform.rotation);
            Sliderstruct[i].slider.transform.rotation *= Quaternion.Euler(0f,180f,0);
            Sliderstruct[i].slider.SetActive(false);
            
        }
        // Instantiate the prefab at a random position within the specified percentage of child transforms
        numToSpawn = Mathf.RoundToInt(childCount * (spawnPercentage / 100f));
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

    public void SetChildTicketed(int ticketedSpotIndex, bool ticketed) {
        childData[ticketedSpotIndex].ticketed = ticketed;
    }

    public float GetFinTicketTime(int childIndex) {
        return Sliderstruct[childIndex].finTicketTime;
    }
    public void SetFinTicketTime(int childIndex, float time) {
        Sliderstruct[childIndex].finTicketTime = time;
    }
    public void Setscore(int score1) {
        score = score1;
    }

    

    private void Update()
    {
        float spawnChance = spawnRate * Time.deltaTime;
    
        if (Random.value < spawnChance)
        {
            
            int randIndex = Random.Range(0, numToSpawn);
            while(!childData[randIndex].taken) {
                randIndex = Random.Range(0, numToSpawn);

            }

            Transform spot = transform.GetChild(randIndex);
            
            Debug.Log("new spawn:" + spot);
            if (!GetChildData(randIndex).ticketed && randIndex != ticketedSpotIndex)
            {
                // Start ticketing the spot if not already taken or ticketed
                ticketedSpotIndex = randIndex;
                ticketTimeRemaining = ticketTime;
                Sliderstruct[ticketedSpotIndex].slider.SetActive(true);
                SetFinTicketTime(ticketedSpotIndex,10);

            }
        }

        for(int i = 0; i < transform.childCount; i++) {
            Transform spot = transform.GetChild(i);

            if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(player.transform.position, spot.position) <= ticketRange && GetFinTicketTime(i) > 0)
            {
                // Ticket the spot
                SetChildTicketed(i, true);
                score += 50;

                // Reset ticketing state
                ticketTimeRemaining = 0f;
                SetFinTicketTime(i,0);
                Sliderstruct[i].slider.SetActive(false);
                break;
                
            }
        }


        for(int i = 0; i < numToSpawn; i++) {
            float time1 = GetFinTicketTime(i);
            if(time1 > 0) {

                float time2 = Mathf.Clamp(time1 - Time.deltaTime, 0, 10);
                SetFinTicketTime(i, time2);
                

                Sliderstruct[i].slider.transform.localScale = new Vector3(1.25f * (time2 / 10), 1f, 1f);

            } else {
                Sliderstruct[i].slider.SetActive(false);
            }
        }
    }
    
}





