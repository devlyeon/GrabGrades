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
        if (Input.GetKeyDown(KeyCode.A))
        {
            // "Base Layer.Idle" 상태일 때만 새로운 입력을 허용
            if (leftArmAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                hasHit = false;
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