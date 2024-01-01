using UnityEngine;

public class HeartAnimation : MonoBehaviour
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
        animator.SetBool("Immune", richie_health.get_shield_status());
        animator.SetFloat("Time", richie_health.get_shield_timer());
    }
}
