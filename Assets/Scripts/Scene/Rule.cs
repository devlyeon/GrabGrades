using UnityEngine;
using UnityEngine.InputSystem;

public class Rule : MonoBehaviour
{
    [SerializeField] private TextPrint textPrint;
    [SerializeField] private ImageSlide imageSlide;

    void Awake()
    {
        textPrint.NextPage();
        imageSlide.NextPage();
    }

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (textPrint.Finished && imageSlide.Finished)
                SceneLoader.loader.LoadScene("InGame");
            else
            {
                textPrint.NextPage();
                imageSlide.NextPage();
            }
        }
    }
}