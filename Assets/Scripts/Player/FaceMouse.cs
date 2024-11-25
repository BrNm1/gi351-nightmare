using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FaceMouse : MonoBehaviour
{
    public Light2D light;
    public bool isLightOn = false;
    
    void Start()
    {
        light.enabled = isLightOn;
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isLightOn = !isLightOn;
            if (light != null)
            {
                light.enabled = isLightOn;
            }
        }
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = mousePos - (Vector2)transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
