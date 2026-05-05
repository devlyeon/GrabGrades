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
            // "Idle" 상태이고, 전이 중이 아닐 때만 새로운 입력을 허용
            if (rightArmAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && 
                !rightArmAnimator.IsInTransition(0))
            {
                hasHit = false;
                rightArmAnimator.ResetTrigger("Pressed");
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
