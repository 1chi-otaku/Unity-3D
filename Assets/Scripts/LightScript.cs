using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private List<Light> nightLights;
    private List<Light> dayLights;
    private bool isNight = true;
    void Start()
    {
        nightLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Night Light"))
        {
            nightLights.Add(g.GetComponent<Light>());

        }
        dayLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Day Light"))
        {
            dayLights.Add(g.GetComponent<Light>());
        }



    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            
            foreach (Light nightLight in nightLights)
            {
                nightLight.enabled = isNight;
            }
            foreach (Light dayLight in dayLights)
            {
                dayLight.enabled = !isNight;
            }
            isNight = !isNight;
        }
    }
}
