using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Shift : MonoBehaviour
{
    public GameObject PastWorld;
    public GameObject FutureWorld;
    private bool playerInRange = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within range and presses the 'E' key
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed");

            // Toggle the PastWorld's active state
            if (!PastWorld.activeSelf)
            {
                PastWorld.SetActive(true);
                Debug.Log("Go to past");
            }
            else
            {
                PastWorld.SetActive(false);
            }

            // Toggle the FutureWorld's active state
            if (!FutureWorld.activeSelf)
            {
                FutureWorld.SetActive(true);
                Debug.Log("Go to future");
            }
            else
            {
                FutureWorld.SetActive(false);
            }
        }
    }

    // Trigger detection when the player enters the object's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the object has a "Player" tag
        {
            playerInRange = true;
            Debug.Log("Player entered range");
        }
    }

    // Trigger detection when the player exits the object's collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range");
        }
    }
}
