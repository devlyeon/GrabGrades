using UnityEngine;

public class ButtonSfx : MonoBehaviour
{
    public void OnClick()
    {
        AudioManager.audioManager.PlaySfx(1);
    }
}