using UnityEngine;
[System.Serializable]
public class Response 
{
    [SerializeField] private string responseTxt;
    [SerializeField] private DialougeObject dialougeObj;

    public string ResponseTxt => responseTxt;
    public DialougeObject DialougeObject => dialougeObj;
}
