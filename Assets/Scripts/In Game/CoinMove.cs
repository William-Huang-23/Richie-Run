using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [Header("GAME DATA")]

    private float speed = 0;

    private float acceleration = 0.75f;

    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;

    private RichieHealth richie_health;

    [Header("GAME OBJECT")]

    private GameObject richie_object;

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        richie_health = GameObject.Find("Richie").GetComponent<RichieHealth>();

        richie_object = GameObject.Find("Richie");

        level_tuning(game_data.get_level());
    }

    void Update()
    {
        if (game_manager.get_game_start() == true)
        {
            move();
        }
    }

    // private
    
    void move()
    {
        if(richie_health.get_magnet_status() == false || transform.position.x - richie_object.transform.position.x > 6 || transform.position.x - richie_object.transform.position.x < -6)
        {
            transform.position = transform.position + Vector3.left * (speed + game_manager.get_extra_speed()) * Time.deltaTime;
        }
        else
        {
            acceleration = acceleration + 0.25f;

            if (transform.position.x < richie_object.transform.position.x)
            {
                transform.position = transform.position + Vector3.right * -(transform.position.x - richie_object.transform.position.x) * acceleration * Time.deltaTime;
            }
            else if (transform.position.x > richie_object.transform.position.x)
            {
                transform.position = transform.position + Vector3.left * (transform.position.x - richie_object.transform.position.x) * acceleration * Time.deltaTime;
            }

            if (transform.position.y < richie_object.transform.position.y)
            {
                transform.position = transform.position + Vector3.up * -(transform.position.y - richie_object.transform.position.y) * acceleration * Time.deltaTime;
            }
            else if (transform.position.y > richie_object.transform.position.y)
            {
                transform.position = transform.position + Vector3.down * (transform.position.y - richie_object.transform.position.y) * acceleration * Time.deltaTime;
            }
        }
    }

    void level_tuning(int level)
    {
        switch (level)
        {
            /*case 0:
                {
                    speed = 5; // -225

                    break;
                }*/
            case 0:
                {
                    speed = 6; // -250

                    break;
                }
            case 1:
                {
                    speed = 7; // -275

                    break;
                }
            case 2:
                {
                    speed = 8; // -300

                    break;
                }
        }
    }
}
