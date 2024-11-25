using System.Collections;
using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] private GameObject dialougeBox;
    [SerializeField] private TMP_Text txtLabel;
    [SerializeField] private DialougeObject testD;
    
    private TypeWriter typeWriter;
    private ResponseHandler responseHandler;
    
    private void Start()
    {
        typeWriter = GetComponent<TypeWriter>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialougeBox();
        ShowDialoge(testD);
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
        }
    }
    private void CloseDialougeBox()
    {
        dialougeBox.SetActive(false);
        txtLabel.text = string.Empty;
    }
}
