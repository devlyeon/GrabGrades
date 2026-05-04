using UnityEngine;

public class LeftArmController : MonoBehaviour
{
    private Animator leftArmAnimator;
    
    void Start()
    {
        leftArmAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 애니메이션만 실행합니다.
            leftArmAnimator.SetTrigger("Pressed");
        }
    }

    // ★ 애니메이션 이벤트가 부를 함수 (Inspector 리스트에 나타납니다)
    public void OnHitTarget()
    {
        // GameManager에게 점수를 계산하라고 신호를 보냅니다.
        // 이때 매개변수로 true(왼쪽)를 넘겨줍니다.
        if (GameManager.instance != null)
        {
            GameManager.instance.ProcessScore(true);
        }
    }
}