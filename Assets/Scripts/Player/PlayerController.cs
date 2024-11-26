using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 100f;
    public float healthMax = 100f;
    
    public Slider healthBar;
    
    [SerializeField] private DialougeUI dialougeUI;
    
    private Rigidbody2D rb;
    public Vector2 moveDirection;

    public DialougeUI DialougeUI => dialougeUI;

    public IInteractable interactable { get; set; }
    public bool warp = false;
    
    private Coroutine resetHealthCoroutine;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.maxValue = healthMax;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }
    
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            if(interactable != null) 
            {
                interactable.Interact(this);
            }
        }

        if (!dialougeUI.IsOpen)
        {
            warp = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        healthBar.gameObject.SetActive(true);
        
        if (health < 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
        healthBar.value = health;
        
        if (resetHealthCoroutine != null)
        {
            StopCoroutine(resetHealthCoroutine);
        }
        
        resetHealthCoroutine = StartCoroutine(ResetHealthAfterDelay(5f));
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(2);
    }
    
    private IEnumerator ResetHealthAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < healthMax; i++)
        {
            health++;
            healthBar.value = health;
            if (health == healthMax)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        yield return new WaitForSeconds(1f);

        healthBar.gameObject.SetActive(false);

        resetHealthCoroutine = null;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
