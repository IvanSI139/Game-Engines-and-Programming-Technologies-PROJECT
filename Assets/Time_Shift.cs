using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Time_Shift : MonoBehaviour

{
    public GameObject PastWorld;
    public GameObject FutureWorld;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");

            if (PastWorld.activeSelf == false)
            {
                PastWorld.SetActive(true);
                Debug.Log("go to past");
            }
            else PastWorld.SetActive(false);


            if (FutureWorld.activeSelf == false)
            {
                FutureWorld.SetActive(true);
                Debug.Log("go to future");
            }
            else FutureWorld.SetActive(false);
        }
    }
}
