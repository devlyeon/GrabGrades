using UnityEngine;

public class LeftArmControl : MonoBehaviour
{
    private Animator leftArmAnimator;
    private bool hasHit = false;
    
    void Start()
    {
        leftArmAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && GameManager.instance.isGameOver == false)
        {
            if (leftArmAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && 
                !leftArmAnimator.IsInTransition(0))
            {
                hasHit = false;
                leftArmAnimator.ResetTrigger("Pressed");
                leftArmAnimator.SetTrigger("Pressed");
            }
        }
    }
 
    public void OnHitTarget()
    {
        if (hasHit) return;

        if (GameManager.instance != null)
        {
            hasHit = true;
            GameManager.instance.ProcessScore(true);
        }
    }
}