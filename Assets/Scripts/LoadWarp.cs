using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWarp : MonoBehaviour
{
    public Image fadeImage;
    public GameObject warpPosition;
    public float fadeDuration = 1.5f;
    public float currentGroundSoundCount;
    
    private GameObject player;
    private PlayerController playerController;
    private void Start()
    {
        //virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerController = other.GetComponent<PlayerController>();
            if (playerController.interactable == null && playerController.warp)
            {
                StartCoroutine(WarpPlayer());
            }
        }
    }
    
    private IEnumerator WarpPlayer()
    {
        
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1);

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        player.transform.position = warpPosition.transform.position;
        Time.timeScale = 0;
        
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1;
        
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(targetColor, startColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }
}
