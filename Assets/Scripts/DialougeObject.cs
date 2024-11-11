using UnityEngine;

[CreateAssetMenu(menuName ="Dialouge/DialougeObject")]
public class DialougeObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialouge;

    public string[] Dialouge => dialouge;
}
