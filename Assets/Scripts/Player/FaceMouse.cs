using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FaceMouse : MonoBehaviour
{
    public Light2D light;
    public bool isLightOn = false;
    public Animator animator;
    
    public float batteryLife = 100f;
    public float batteryMaxLife = 100f;
    public float batteryDrainRate = 10f;
    public Slider batterySlider;
    
    private PlayerController player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        light.enabled = isLightOn;
        batterySlider.maxValue = batteryMaxLife;
        batterySlider.gameObject.SetActive(false);
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (batteryLife > 0)
            {
                isLightOn = !isLightOn;
                if (light != null)
                {
                    light.enabled = isLightOn;
                }
            }
        }
        
        if (isLightOn && batteryLife > 0)
        {
            batteryLife -= batteryDrainRate * Time.deltaTime;

            if (batteryLife <= 0)
            {
                batteryLife = 0;
                isLightOn = false;
                light.enabled = false;
            }
        }
        
        UpdateBatteryUI();
        
        if (batterySlider != null)
        {
            batterySlider.value = batteryLife;
        }
        
        float speed = player.moveDirection.magnitude;
        animator.SetFloat("Move", speed);
        
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector2 direction = (mousePosition - transform.position).normalized;
        
        UpdateAnimation(direction);
        
        RotateCharacter(direction);
    }
    
    void UpdateAnimation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        animator.SetFloat("Angle", angle);
    }
    
    void RotateCharacter(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    
    void UpdateBatteryUI()
    {
        if (batterySlider != null)
        {
            batterySlider.value = batteryLife;

            if (isLightOn || batteryLife <= 0)
            {
                batterySlider.gameObject.SetActive(true);
            }
            else
            {
                batterySlider.gameObject.SetActive(false);
            }
        }
    }
    
    public void RechargeBattery(float amount)
    {
        batteryLife += amount;
        if (batteryLife > batteryMaxLife)
        {
            batteryLife = batteryMaxLife;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Battery"))
        {
            Debug.Log(collision.gameObject.name);
            RechargeBattery(20f);
            Destroy(collision.gameObject);
        }
    }
}
