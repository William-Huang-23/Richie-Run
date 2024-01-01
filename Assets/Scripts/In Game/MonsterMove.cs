using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [Header("GAME DATA")]

    private float x_speed = 0;
    private float y_speed = 0;

    private float game_timer = 0;

    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("PREFAB")]

    [SerializeField]
    private GameObject broken_cone;
    [SerializeField]
    private GameObject broken_car;

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        level_tuning(game_data.get_level());

        x_speed = x_speed + Random.Range(0, 3);
        y_speed = Random.Range(-y_speed, y_speed);
    }
    
    void Update()
    {
        if(game_manager.get_game_start() == true)
        {
            if (game_timer >= 2)
            {
                game_timer = 0;

                y_speed = y_speed + 1;
            }

            move();
        }
    }

    // private

    void move()
    {
        transform.position = transform.position + Vector3.left * (x_speed + game_manager.get_extra_speed()) * Time.deltaTime;

        GetComponent<Rigidbody2D>().velocity = new Vector3(0, y_speed, 0) * Time.fixedDeltaTime;
    }

    void level_tuning(int level)
    {
        switch (level)
        {
            /*case 0:
                {
                    x_speed = 7; // -225
                    y_speed = 60;

                    break;
                }*/
            case 0:
                {
                    x_speed = 8; // -250
                    y_speed = 70;

                    break;
                }
            case 1:
                {
                    x_speed = 9; // -275
                    y_speed = 80;

                    break;
                }
            case 2:
                {
                    x_speed = 10; // -300
                    y_speed = 90;

                    break;
                }
        }
    }

    // misc

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border Top")
        {
            if(y_speed > 0)
            {
                y_speed = -y_speed;
            }
        }

        if (collision.gameObject.tag == "Border Bottom")
        {
            if (y_speed < 0)
            {
                y_speed = -y_speed;
            }
        }

        if (collision.gameObject.tag == "Car")
        {
            if (transform.position.y > collision.transform.position.y)
            {
                if(y_speed < 0)
                {
                    y_speed = -y_speed;
                }
            }
            else
            {
                if (y_speed > 0)
                {
                    y_speed = -y_speed;
                }
            }
        }

        if (collision.gameObject.tag == "Coin")
        {
            audio_manager.play_sound("Car 2 Sound");

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Cone")
        {
            audio_manager.play_sound("Car 2 Sound");

            Instantiate(broken_cone, new Vector3(collision.transform.position.x, collision.transform.position.y, 5), collision.transform.rotation);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            audio_manager.play_sound("Car 1 Sound");

            Instantiate(broken_car, new Vector3(transform.position.x, transform.position.y, 5), transform.rotation);

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Magnet")
        {
            audio_manager.play_sound("Car 2 Sound");
            
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Clock")
        {
            audio_manager.play_sound("Car 2 Sound");

            Destroy(collision.gameObject);
        }
    }
}
