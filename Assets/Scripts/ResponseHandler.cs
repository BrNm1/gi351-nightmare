using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialougeUI dialougeUI;

    List<GameObject> tempDialougeButtons = new List<GameObject>();

    private void Start()
    {
        dialougeUI = GetComponent<DialougeUI>();
    }
    public void ShowResponse(Response[] responses) 
    {
        float responseBoxHeight = 0;

        foreach (Response response in responses) 
        { 
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject,responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text= response.ResponseTxt;
            responseButton.GetComponent<Button>().onClick.AddListener(() => onPickedResponse(response));

            tempDialougeButtons.Add(responseButton);
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x,responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }
    private void onPickedResponse(Response response) 
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button  in tempDialougeButtons) 
        {
            Destroy(button);
        }
        tempDialougeButtons.Clear();

        dialougeUI.ShowDialoge(response.DialougeObject);
    }
}
