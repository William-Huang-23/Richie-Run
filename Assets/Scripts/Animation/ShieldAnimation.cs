using UnityEngine;

public class ShieldAnimation : MonoBehaviour
{
    [Header("CLASS")]
    
    private RichieHealth richie_health;

    [Header("ANIMATOR")]

    [SerializeField]
    private Animator animator;

    void Start()
    {
        richie_health = GameObject.Find("Richie").GetComponent<RichieHealth>();
    }
    
    void Update()
    {
        animator.SetFloat("Time", richie_health.get_shield_timer());
    }
}
