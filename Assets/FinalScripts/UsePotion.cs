using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class UsePotion : MonoBehaviour
{
    private Inventory inventory;
    private Health playerHealth;
    private int count;
    private bool healed;
    private float lastPotionUseTime = 0f;
    private float potionCooldown = 1f;

    [SerializeField] private float healAmount = 2f; // Amount of health restored by the potion. 

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()


    {
        lastPotionUseTime += Time.deltaTime;
        // Check for key presses (1, 2, 3, etc.) 
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Key 1 for Slot 0 
        {
            UseHealthPotion(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Key 2 for Slot 1 
        {
            UseHealthPotion(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Key 3 for Slot 2 
        {
            UseHealthPotion(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // Key 4 for Slot 3 
        {
            UseHealthPotion(3);
        }
        // Add more keys if you have more slots (Alpha5, Alpha6, etc.) 
        if (Input.GetKeyDown(KeyCode.Q) ) // Key 1 for Slot 0 
        {




            if (3 >= 0 && 3 < inventory.IsFull.Length && inventory.IsFull[3])
            {
                UseHealthPotion(3);
            }
            else if (2 >= 0 && 2 < inventory.IsFull.Length && inventory.IsFull[2]) 
            {
                UseHealthPotion(2);
            }
            else if (1 >= 0 && 1 < inventory.IsFull.Length && inventory.IsFull[2])
            {
                UseHealthPotion(1);
            }
            else if (1 >= 0 && 1 < inventory.IsFull.Length && inventory.IsFull[2])
            {
                UseHealthPotion(0);
            }
            else
            {
                Debug.Log("No potions");
            }

        }
            
    }

    public void UseHealthPotion(int slotIndex)
    {


        // Check if the specific slot contains a potion. 
        if (slotIndex >= 0 && slotIndex < inventory.IsFull.Length && inventory.IsFull[slotIndex])
        {
            // Mark the slot as empty after using the potion. 
            inventory.IsFull[slotIndex] = false;

            // Destroy the potion image (button) from the slot (its child object). 
            Transform slotTransform = inventory.slots[slotIndex].transform;
            if (slotTransform.childCount > 0)
            {
                GameObject potionImage = slotTransform.GetChild(0).gameObject; // Get the potion image. 
                Destroy(potionImage); // Destroy the potion image. 
                Debug.Log($"Potion used from slot {slotIndex}. Image destroyed.");
            }
            else
            {
                Debug.LogWarning($"Slot {slotIndex} was full, but has no child to destroy.");
            }

            // Heal the player by the specified amount. 
            playerHealth.Heal(healAmount);
            Debug.Log($"Player healed by {healAmount}.");
        }
        else
        {
            Debug.Log($"Slot {slotIndex} is empty or invalid.");
        }

    }

    private void UseHealthPotiontest()
    {

        healed = true;
        // Ensure count is within the valid range.
        if (count >= 0 && count < inventory.IsFull.Length)
        {
            if (inventory.IsFull[count]) // Check if the current slot has a potion.
            {
                 // Mark the slot as empty.
                inventory.IsFull[count] = false;
                 // Destroy the potion image (button) from the slot (its child object).
                Transform slotTransform = inventory.slots[count].transform; 
                if (slotTransform.childCount > 0)
                {
                    GameObject potionImage = slotTransform.GetChild(0).gameObject; // Get the potion image.
                    Destroy(potionImage); // Destroy the potion image.
                    Debug.Log($"Potion used from slot {count}. Image destroyed.");
                }
                else
                {
                    Debug.LogWarning($"Slot {count} was marked full, but no child was found.");
                }
                // Heal the player by the specified amount.
                playerHealth.Heal(healAmount);
                Debug.Log($"Player healed by {healAmount}.");
                // Exit the method after successfully using one potion.
                return;
            }        
            else
            {
                Debug.Log($"Slot {count} is empty.");
            }
        }
        else    
        {
            Debug.LogWarning($"Invalid slot index: {count}. No action taken.");    
        }
    // If no potion was used, decrement count and try again.
     count--; if (count >= 0) // Ensure count remains within valid bounds.
        {
          UseHealthPotiontest(); // Recursively call the method for the next slot.
        }
        else
        {
         Debug.LogWarning("No potions available in any slot.");
        }
    }

    //&& count < inventory.IsFull.Length
}