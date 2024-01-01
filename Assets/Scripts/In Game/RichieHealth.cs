using UnityEngine;

public class RichieHealth : MonoBehaviour
{
    [Header("GAME DATA")]

    private int health = 3;

    private float buffer_time = 0;

    private bool take_damage_recently = false;

    private int shield_duration = 0;

    private float shield_timer = 0;
    private int shield_health = 3;
    private bool shield_status = false;

    private float magnet_timer = 0;
    private bool magnet_status = false;

    [Header("CLASS")]
    
    private GameManager game_manager;
    
    private GameData game_data;

    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject[] health_icon;
    [SerializeField]
    private RectTransform[] health_icon_rect;

    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject magnet;

    [Header("PREFAB")]

    [SerializeField]
    private GameObject[] lose;

    [Header("ANIMATOR")]

    [SerializeField]
    private Animator animator;

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        if (game_data.get_character() == 2)
        {
            shield_duration = 10;
        }
        else
        {
            shield_duration = 5;
        }

        if (game_data.get_kidal() == 1 && game_data.get_on_PC() == false)
        {
            health_icon_rect[0].anchoredPosition = new Vector3(-health_icon_rect[0].anchoredPosition.x, health_icon_rect[0].anchoredPosition.y);
            health_icon_rect[1].anchoredPosition = new Vector3(-health_icon_rect[1].anchoredPosition.x, health_icon_rect[1].anchoredPosition.y);
            health_icon_rect[2].anchoredPosition = new Vector3(-health_icon_rect[2].anchoredPosition.x, health_icon_rect[2].anchoredPosition.y);
        }
    }
    
    void Update()
    {
        if(shield_status == true)
        {
            shield_timer = shield_timer - Time.deltaTime;

            if (shield_timer <= 0)
            {
                deactivate_shield();
            }
        }

        if (magnet_status == true)
        {
            magnet_timer = magnet_timer - Time.deltaTime;

            if (magnet_timer <= 0)
            {
                deactivate_magnet();
            }
        }

        if (take_damage_recently == true)
        {
            buffer_time = buffer_time + Time.deltaTime;

            if(buffer_time > 3)
            {
                buffer_time = 0;

                take_damage_recently = false;

                reset_hurt();

                animator.SetBool("Immune", false);
            }
        }

        if(health <= 0)
        {
            game_manager.game_over();

            Instantiate(lose[game_data.get_character()], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            
            Destroy(this.gameObject);
        }
    }

    // public

    // getter setter

    public void take_damage()
    {
        if(take_damage_recently == false)
        {
            health--;

            take_damage_recently = true;

            animator.SetBool("Immune", true);

            health_icon[health].SetActive(false);
        }
    }

    public void set_shield_health()
    {
        shield_health--;

        if (shield_health <= 0)
        {
            deactivate_shield();
        }
    }

    public float get_shield_timer()
    {
        return shield_timer;
    }

    public bool get_shield_status()
    {
        return shield_status;
    }

    public float get_magnet_timer()
    {
        return magnet_timer;
    }

    public bool get_magnet_status()
    {
        return magnet_status;
    }

    public void activate_shield()
    {
        deactivate_magnet();

        shield_timer = shield_duration;
        shield_health = 3 + game_data.get_shield();
        shield_status = true;

        animator.SetBool("Hold", true);

        shield.SetActive(true);
    }

    public void deactivate_shield()
    {
        shield_timer = shield_duration;
        shield_status = false;

        animator.SetBool("Hold", false);

        shield.SetActive(false);
    }

    public void activate_magnet()
    {
        deactivate_shield();

        magnet_timer = 5 + game_data.get_magnet();
        magnet_status = true;

        animator.SetBool("Hold", true);

        magnet.SetActive(true);
    }

    public void deactivate_magnet()
    {
        magnet_timer = 5 + game_data.get_magnet();
        magnet_status = false;

        animator.SetBool("Hold", false);

        magnet.SetActive(false);
    }

    public void hurt()
    {
        animator.SetTrigger("Hurt");
    }

    void reset_hurt()
    {
        animator.ResetTrigger("Hurt");
    }
}
