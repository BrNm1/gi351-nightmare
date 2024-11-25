
using UnityEngine;

public class DialougeActivator : MonoBehaviour , IInteractable
{
    [SerializeField] DialougeObject dialougeObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player)) 
        {
            player.interactable=this;
        }   
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
            if (player.interactable is DialougeActivator dialougeActivator && dialougeActivator == this) 
            {
                player.interactable=null;
            }
        }
    }

    public void Interact(PlayerController player) 
    {
        player.DialougeUI.ShowDialoge(dialougeObject);
    }
}
