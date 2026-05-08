using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlide : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> sprites;
    
    private int current = 0;

    public int Current => current;
    public int Count => sprites.Count;
    public bool Finished => current >= sprites.Count;

    public void NextPage()
    {
        if (current >= sprites.Count) return;
        image.sprite = sprites[current++];
    }
}