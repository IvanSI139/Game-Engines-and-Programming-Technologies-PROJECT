using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingingtrap : MonoBehaviour
{
    [Header("Swinging")]
    [SerializeField] private float leftSwing;
    [SerializeField] private float rightSwing;
    [SerializeField] private float speed;
    private float initRotation;
    [SerializeField] private bool swingingLeft;
    void Awake()
    {
        initRotation = transform.localEulerAngles.z; ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
