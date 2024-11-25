using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private DialougeUI dialougeUI;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public DialougeUI DialougeUI => dialougeUI;

    public IInteractable interactable { get; set; }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
