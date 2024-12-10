using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ran Over Potion");

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.IsFull[i] == false)
                {
                    inventory.IsFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Debug.Log($"Item placed in slot");
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
