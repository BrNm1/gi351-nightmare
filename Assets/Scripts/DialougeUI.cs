using System.Collections;
using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text txtLabel;
    [SerializeField] private DialougeObject testD;
    private TypeWriter typeWriter;
    private void Start()
    {
        typeWriter = GetComponent<TypeWriter>();
        ShowDialoge(testD);
    }
    public void ShowDialoge(DialougeObject dialougeObject) 
    {
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
    }
}
