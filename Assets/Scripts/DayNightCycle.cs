using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public float timeOfDay = 0f;
    public float timeSpeed = 1f;
    public float dayDuration = 60f;
    
    public Light2D sceneLight;
    public Color morningColor = Color.yellow;
    public Color noonColor = Color.white;
    public Color sunsetColor = Color.red;
    public Color nightColor = Color.blue;
    
    public float morningIntensity = 1f;
    public float noonIntensity = 1.5f;
    public float sunsetIntensity = 0.7f;
    public float nightIntensity = 0.2f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        timeOfDay += Time.deltaTime * timeSpeed / dayDuration;

        if (timeOfDay > 1)
        {
            timeOfDay = 0;
        }
        
        UpdateLighting();
    }

    void UpdateLighting()
    {
        if (timeOfDay < 0.25f)
        {
            sceneLight.color = Color.Lerp(morningColor, noonColor, timeOfDay * 4);
            sceneLight.intensity = Mathf.Lerp(morningIntensity, noonIntensity, timeOfDay * 4);
        }
        else if (timeOfDay < 0.5f)
        {
            sceneLight.color = Color.Lerp(noonColor, sunsetColor, (timeOfDay - 0.25f) * 4);
            sceneLight.intensity = Mathf.Lerp(noonIntensity, sunsetIntensity, (timeOfDay - 0.25f) * 4);
        }
        else if (timeOfDay < 0.75f)
        {
            sceneLight.color = Color.Lerp(sunsetColor, nightColor, (timeOfDay - 0.5f) * 4);
            sceneLight.intensity = Mathf.Lerp(sunsetIntensity, nightIntensity, (timeOfDay - 0.5f) * 4);
        }
        else
        {
            sceneLight.color = Color.Lerp(nightColor, morningColor, (timeOfDay - 0.75f) * 4);
            sceneLight.intensity = Mathf.Lerp(nightIntensity, morningIntensity, (timeOfDay - 0.75f) * 4);
        }
    }
}
