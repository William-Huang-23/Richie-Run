using UnityEngine;

public class RichieMove : MonoBehaviour
{
    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;

    [Header("GAME DATA")]

    private float x_speed = 0;
    private float y_speed = 0;

    private float base_speed = 0;

    [Header("UI")]

    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private RectTransform joystick_rect;

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        if (game_data.get_character() == 1)
        {
            base_speed = 225;
        }
        else
        {
            base_speed = 175;
        }

        if(game_data.get_on_PC() == true)
        {
            joystick.gameObject.SetActive(false);
        }
        else
        {
            joystick.gameObject.SetActive(true);
        }

        if(game_data.get_kidal() == 1 && game_data.get_on_PC() == false)
        {
            joystick_rect.anchoredPosition = new Vector3(-joystick_rect.anchoredPosition.x, joystick_rect.anchoredPosition.y);
        }
    }
    
    void Update()
    {
        if (game_manager.get_game_start() == true)
        {
            if (game_data.get_on_PC() == true)
            {
                move_keyboard();
            }
            else
            {
                move_joystick();
            }

            GetComponent<Rigidbody2D>().velocity = new Vector3(x_speed, y_speed, 0) * Time.fixedDeltaTime;
        }
    }

    // private

    void move_keyboard()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            y_speed = base_speed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            y_speed = -base_speed;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) 
        {
            y_speed = 0;
        }

        if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) 
        {
            y_speed = 0;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x_speed = -base_speed * 1.75f;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x_speed = base_speed / 1.25f;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            x_speed = 0;
        }

        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            x_speed = 0;
        }
    }

    void move_joystick()
    {
        if (joystick.Horizontal >= 0.3)
        {
            x_speed = base_speed / 1.25f;
        }
        else if (joystick.Horizontal >= 0.15) 
        {
            x_speed = base_speed / 2.5f;
        }

        if (joystick.Vertical >= 0.3)
        {
            y_speed = base_speed;
        }
        else if (joystick.Vertical >= 0.15)
        {
            y_speed = base_speed / 2;
        }

        if (joystick.Horizontal <= -0.3)
        {
            x_speed = -base_speed * 1.75f;
        }
        else if (joystick.Horizontal <= -0.15)
        {
            x_speed = -base_speed * 0.875f;
        }

        if (joystick.Vertical <= -0.3)
        {
            y_speed = -base_speed;
        }
        else if (joystick.Vertical <= -0.15)
        {
            y_speed = -base_speed / 2;
        }

        if (joystick.Horizontal < 0.15 && joystick.Horizontal > -0.15)
        {
            x_speed = 0;
        }

        if (joystick.Vertical < 0.15 && joystick.Vertical > -0.15)
        {
            y_speed = 0;
        }
    }
}
