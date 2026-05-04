using TMPro;
using UnityEngine;

public class SliderDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;

    public void OnValueChanged(float value)
    {
        displayText.text = (value * 10).ToString("0.#");
    }
}
