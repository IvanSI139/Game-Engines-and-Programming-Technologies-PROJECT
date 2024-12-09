using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingingtrap : MonoBehaviour
{
    [Header("Swinging")]
    [SerializeField] private float leftSwing;
    [SerializeField] private float rightSwing;
    [SerializeField] private float speed;
    private float change;
    private float initRotation;
    private bool swingingLeft;


    private void Awake()
    {
        swingingLeft = false;
        change = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (swingingLeft == true)
        {

            if (change >= leftSwing)
            {
                //SwingInDirection(-1);
                //transform.localEulerAngles.z = transform.rotation.z;
                transform.Rotate(0,0, -1 * Time.deltaTime * speed);
                change += -1 * Time.deltaTime * speed;
            }
            else
            {
                swingingLeft = false;
            }

        }
        else
        {
            if (change <= rightSwing)
            {
                transform.Rotate(0, 0, 1 * Time.deltaTime *speed);
                change += 1 * Time.deltaTime * speed;

            }
            else
            {
                swingingLeft = true;
            }
        }


    }


}
