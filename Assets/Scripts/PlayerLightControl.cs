using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightControl : MonoBehaviour
{
    public Light2D centerLight;
    public Light2D directionalLight;
    public float fadeDelay = 0.007f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (centerLight.intensity > 0)
            {
                centerLight.intensity -= fadeDelay;
                directionalLight.intensity += fadeDelay;
            }
        }
        else
        {
            if (centerLight.intensity < 1)
            {
                centerLight.intensity += fadeDelay;
                directionalLight.intensity -= fadeDelay;
            }
        }
    }
}
