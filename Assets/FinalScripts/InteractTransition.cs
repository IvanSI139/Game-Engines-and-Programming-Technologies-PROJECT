using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InteractableObject : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject FinishScreen;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the trigger
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision");
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player has exited the trigger
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        // Check if the player is in range and presses the "F" key
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Get the current active scene's build index and load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load. End of build.");
            FinishScreen.SetActive(true);

        }
    }
}
