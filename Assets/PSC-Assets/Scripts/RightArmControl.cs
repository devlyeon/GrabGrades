using UnityEngine;

public class RightArmControl : MonoBehaviour
{
    private Animator rightArmAnimator;
    private bool hasHit = false;

    void Start()
    {
        rightArmAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // "Base Layer.Idle" 상태일 때만 새로운 입력을 허용
            if (rightArmAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                hasHit = false;
                rightArmAnimator.SetTrigger("Pressed");
            }
        }
    }

    public void OnHitTarget()
    {
        if (hasHit) return;

        if (GameManager.instance != null)
        {
            hasHit = true;
            GameManager.instance.ProcessScore(false);
        }
    }
    
}
