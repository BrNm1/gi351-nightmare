using System.Collections;
using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] private GameObject dialougeBox;
    [SerializeField] private TMP_Text txtLabel;
    [SerializeField] private DialougeObject testD;
    private TypeWriter typeWriter;
    private void Start()
    {
        typeWriter = GetComponent<TypeWriter>();
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
        yield return new WaitForSeconds(2);
        foreach (string dialouge in dialougeObject.Dialouge) 
        { 
            yield return typeWriter.Run(dialouge,txtLabel);
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
        }
        CloseDialougeBox();
    }
    private void CloseDialougeBox()
    {
        dialougeBox.SetActive(false);
        txtLabel.text = string.Empty;
    }
}
