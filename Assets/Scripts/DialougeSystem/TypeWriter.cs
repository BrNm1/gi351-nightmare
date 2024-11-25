using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField]private float writerSpeed =50f;
    public Coroutine Run(string txtToType,TMP_Text txtLabel) 
    {
       return StartCoroutine(TypeTxt(txtToType,txtLabel));
    }
    private IEnumerator TypeTxt(string txtToType, TMP_Text txtLabel) 
    {
        txtLabel.text = string.Empty;
        
        float t=0f;
        int charIndex = 0;
        while(charIndex < txtToType.Length) 
        {
            t += Time.deltaTime*writerSpeed;
            charIndex= Mathf.FloorToInt(t);
            charIndex=Mathf.Clamp(charIndex, 0, txtToType.Length);
            txtLabel.text = txtToType.Substring(0,charIndex);

            yield return null;
        }
    }
    
}
