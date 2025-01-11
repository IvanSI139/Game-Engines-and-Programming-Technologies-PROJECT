using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDrop : MonoBehaviour
{

    public gem Gem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drop()
    {
        Debug.Log("drop");
        Gem.Gem();
    }
}
