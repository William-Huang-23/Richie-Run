using UnityEngine;

public class RichieCatch : MonoBehaviour
{
    [Header("GAME DATA")]

    private int bonus = 0;

    private int buffer_time_coin = 0;
    private int buffer_time_cone = 0;
    private int buffer_time_car = 0;
    private int buffer_time_shield = 0;
    private int buffer_time_magnet = 0;

    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;
    private AudioManager audio_manager;
    
    private RichieHealth richie_health;

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

        richie_health = GameObject.Find("Richie").GetComponent<RichieHealth>();

        if (game_data.get_character() == 0)
        {
            bonus = game_manager.get_addition() / 10;
        }
        else
        {
            bonus = 0;
        }
    }
    
    void Update()
    {
        if(buffer_time_coin < 1)
        {
            buffer_time_coin++;
        }

        if(buffer_time_cone < 1)
        {
            buffer_time_cone++;
        }

        if (buffer_time_car < 1)
        {
            buffer_time_car++;
        }

        if (buffer_time_shield < 1)
        {
            buffer_time_shield++;
        }

        if (buffer_time_magnet < 1)
        {
            buffer_time_magnet++;
        }
    }

    // misc

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin" && buffer_time_coin >= 1)
        {
            audio_manager.play_sound("Coin Sound");

            game_manager.add_score(bonus);

            Destroy(collision.gameObject);

            buffer_time_coin = 0;
        }

        if(collision.gameObject.tag == "Cone" && buffer_time_cone >= 1)
        {
            if (richie_health.get_shield_status() == false)
            {
                audio_manager.play_sound("Cone Sound");

                game_manager.subtract_score(bonus);
            }
            else
            {
                audio_manager.play_sound("Shield Sound");

                richie_health.set_shield_health();
            }

            Instantiate(broken_cone, new Vector3(collision.transform.position.x, collision.transform.position.y, 5), collision.transform.rotation);
            Destroy(collision.gameObject);

            buffer_time_cone = 0;
        }

        if (collision.gameObject.tag == "Car" && buffer_time_car >= 1)
        {
            audio_manager.play_sound("Car 1 Sound");

            if (richie_health.get_shield_status() == false)
            {
                richie_health.take_damage();

                if (richie_health.get_magnet_status() == true)
                {
                    richie_health.deactivate_magnet();

                    richie_health.hurt();
                }
            }
            else
            {
                audio_manager.play_sound("Shield Sound");

                richie_health.set_shield_health();
            }
            
            Instantiate(broken_car, new Vector3(collision.transform.position.x, collision.transform.position.y, 5), collision.transform.rotation);

            Destroy(collision.gameObject);

            buffer_time_car = 0;
        }

        if (collision.gameObject.tag == "Shield" && buffer_time_shield >= 1)
        {
            audio_manager.play_sound("Shield Sound");

            Destroy(collision.gameObject);

            richie_health.deactivate_magnet();
            richie_health.activate_shield();

            buffer_time_shield = 0;
        }

        if (collision.gameObject.tag == "Magnet" && buffer_time_magnet >= 1)
        {
            audio_manager.play_sound("Magnet Sound");

            Destroy(collision.gameObject);

            richie_health.deactivate_shield();
            richie_health.activate_magnet();

            buffer_time_magnet = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin" && buffer_time_coin >= 1)
        {
            audio_manager.play_sound("Coin Sound");

            game_manager.add_score(bonus);

            Destroy(collision.gameObject);

            buffer_time_coin = 0;
        }

        if (collision.gameObject.tag == "Cone" && buffer_time_cone >= 1)
        {
            if (richie_health.get_shield_status() == false)
            {
                audio_manager.play_sound("Cone Sound");

                game_manager.subtract_score(bonus);
            }
            else
            {
                audio_manager.play_sound("Shield Sound");

                richie_health.set_shield_health();
            }

            Instantiate(broken_cone, new Vector3(collision.transform.position.x, collision.transform.position.y, 5), collision.transform.rotation);
            Destroy(collision.gameObject);

            buffer_time_cone = 0;
        }

        if (collision.gameObject.tag == "Car" && buffer_time_car >= 1)
        {
            audio_manager.play_sound("Car 1 Sound");

            if (richie_health.get_shield_status() == false)
            {
                richie_health.take_damage();

                if (richie_health.get_magnet_status() == true)
                {
                    richie_health.deactivate_magnet();

                    richie_health.hurt();
                }
            }
            else
            {
                audio_manager.play_sound("Shield Sound");

                richie_health.set_shield_health();
            }

            Instantiate(broken_car, new Vector3(collision.transform.position.x, collision.transform.position.y, 5), collision.transform.rotation);
            
            Destroy(collision.gameObject);

            buffer_time_car = 0;
        }

        if (collision.gameObject.tag == "Shield" && buffer_time_shield >= 1)
        {
            audio_manager.play_sound("Shield Sound");

            Destroy(collision.gameObject);

            richie_health.deactivate_magnet();
            richie_health.activate_shield();

            buffer_time_shield = 0;
        }

        if (collision.gameObject.tag == "Magnet" && buffer_time_magnet >= 1)
        {
            audio_manager.play_sound("Magnet Sound");

            Destroy(collision.gameObject);

            richie_health.deactivate_shield();
            richie_health.activate_magnet();

            buffer_time_magnet = 0;
        }
    }
}
