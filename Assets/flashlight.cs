using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class flashlight : MonoBehaviour
{
    public GameObject ON;
    public GameObject OFF;
    private bool isON;

    void Start()
    {
    ON.SetActive(false);
    OFF.SetActive(true);
    isON = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        if(isON)
        {
            ON.SetActive(false);
            OFF.SetActive(true);
        }
         if(!isON)
         {
            ON.SetActive(true);
            OFF.SetActive(false);
        }


            isON = !isON;

        }
    }
}
