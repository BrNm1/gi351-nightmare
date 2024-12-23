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
    
    public AudioSource audio;
    public AudioClip openSound;
    public AudioClip closeSound;
    private bool isLight = false;
    
    private float soundCont;
    private GameObject battery;
    
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
        if (Input.GetKeyDown(KeyCode.F) && isLight)
        {
            if (batteryLife > 0)
            {
                isLightOn = !isLightOn;
                if (light != null)
                {
                    light.enabled = isLightOn;
                    PlayOpenSound();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && battery != null)
        {
            RechargeBattery(batteryMaxLife);
            Destroy(battery);
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
    
    private void PlayOpenSound()
    {
        soundCont = (soundCont % 2) + 1;
        
        switch (soundCont)
        {
            case 1:
                audio.PlayOneShot(openSound);
                break;
            case 2:
                audio.PlayOneShot(closeSound);
                break;
        }
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
            battery = collision.gameObject;
        }

        if (collision.CompareTag("isLight"))
        {
            isLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Battery"))
        {
            battery = null;
        }
    }
}
