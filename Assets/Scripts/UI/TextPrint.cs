using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPrint : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private List<string> strings;
    
    private int current = 0;

    public int Current => current;
    public int Count => strings.Count;

    public void NextPage()
    {
        if (current >= strings.Count) return;
        text.text = strings[current++];
    }
}