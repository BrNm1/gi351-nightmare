using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FaceMouse : MonoBehaviour
{
    public Light2D light;
    public bool isLightOn = false;
    public Animator animator;
    
    private Vector2 lastDirection = Vector2.zero;
    private PlayerController player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
}
