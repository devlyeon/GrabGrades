using TMPro;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected bool isActive = false;

    protected virtual void Awake()
    {
        text.text = isActive ? "켜짐" : "꺼짐";
    }

    public virtual void Toggle()
    {
        isActive = !isActive;
        text.text = isActive ? "켜짐" : "꺼짐";
    }
}