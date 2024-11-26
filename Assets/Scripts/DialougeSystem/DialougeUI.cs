using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] private GameObject dialougeBox;
    [SerializeField] private TMP_Text txtLabel;
    
    public float fadeDuration = 1.5f;
    public Image fadeImage;
    public bool IsOpen { get;private set; }

    private TypeWriter typeWriter;
    private ResponseHandler responseHandler;
    
    private void Start()
    {
        typeWriter = GetComponent<TypeWriter>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialougeBox();
        
    }
    public void ShowDialoge(DialougeObject dialougeObject) 
    {
        dialougeBox.SetActive(true);
        StartCoroutine(StepthroughDialouge(dialougeObject));
    }
    private IEnumerator StepthroughDialouge(DialougeObject dialougeObject) 
    {
        for (int i = 0; i < dialougeObject.Dialouge.Length; i++) 
        { 
            string dialouge = dialougeObject.Dialouge[i];
            yield return typeWriter.Run(dialouge, txtLabel);

            if (i == dialougeObject.Dialouge.Length - 1 && dialougeObject.Hasresponse) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        if (dialougeObject.Hasresponse)
        {
            responseHandler.ShowResponse(dialougeObject.Responses);
        }
        else
        { 
            CloseDialougeBox() ;
            if (dialougeObject.ShouldWarp)
            {
                StartCoroutine(WarpPlayer(dialougeObject.TargetPosition));
            }
        }
    }
    
    private IEnumerator WarpPlayer(Vector3 playerPosition)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1);

        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        player.transform.position = playerPosition;
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
    
    private void CloseDialougeBox()
    {
        IsOpen = false;
        dialougeBox.SetActive(false);
        txtLabel.text = string.Empty;
    }
}
