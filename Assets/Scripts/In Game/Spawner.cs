using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("GAME DATA")]

    private float timer = 0;
    private float game_timer = 0;
    private float game_timer_2 = 0;

    private int spawn_rate_coin = 0;
    private int spawn_rate_cone = 0;
    private int spawn_rate_car = 0;
    private int spawn_rate_shield = 0;
    private int spawn_rate_magnet = 0;

    private int max_cone = 0;
    private int max_car = 0;
    private int max_shield = 0;
    private int max_magnet = 0;

    private float spawn_interval = 1;

    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;

    [Header("PREFAB")]

    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject cone;
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject magnet;

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        timer = Random.Range(6, 10);

        level_tuning(game_data.get_level());
    }
    
    void Update()
    {
        if(game_manager.get_game_start() == true && game_manager.get_game_over_status() == false)
        {
            timer = timer + Time.deltaTime;

            if(timer >= spawn_interval)
            {
                max_cone = 0;
                max_car = Random.Range(0, 3);
                max_shield = 0;
                max_magnet = 0;

                int temp = Random.Range(1, 4);

                if (temp == 1)
                {
                    spawn(1);
                    spawn(2);
                    spawn(3);
                }
                else if (temp == 2)
                {
                    spawn(2);
                    spawn(3);
                    spawn(1);
                }
                else if (temp == 3)
                {
                    spawn(3);
                    spawn(1);
                    spawn(2);
                }

                timer = 0;
            }
        }

        game_timer = game_timer + Time.deltaTime;

        if(game_timer >= 10)
        {
            game_timer = 0;

            spawn_rate_car = spawn_rate_car + 1;
        }

        game_timer_2 = game_timer_2 + Time.deltaTime;

        if (game_timer_2 >= 30)
        {
            game_timer_2 = 0;

            spawn_interval = spawn_interval - 0.1f;
        }
    }

    // private

    void spawn(int location)
    {
        float initial_y = 0;

        switch(location)
        {
            case 1:
                {
                    initial_y = 2.25f;

                    break;
                }
            case 2:
                {
                    initial_y = 0f;

                    break;
                }
            case 3:
                {
                    initial_y = -2.25f;

                    break;
                }
        }

        int random_number_coin = Random.Range(1, 101);
        int random_number_bomb = Random.Range(1, 101 - spawn_rate_coin);
        int random_number_car = Random.Range(1, 101 - spawn_rate_coin - spawn_rate_cone);
        int random_number_shield = Random.Range(1, 101 - spawn_rate_coin - spawn_rate_cone - spawn_rate_car);
        int random_number_magnet = Random.Range(1, 101 - spawn_rate_coin - spawn_rate_cone - spawn_rate_car - spawn_rate_shield);

        float x = Random.Range(0f, 3f);
        float y = Random.Range(-0.5f, 0.5f);

        if (random_number_coin <= spawn_rate_coin)
        {
            Instantiate(coin, new Vector3(transform.position.x + x, initial_y + y, transform.position.z), transform.rotation);
        }
        else if (random_number_bomb <= spawn_rate_cone)
        {
            if(max_cone < 2)
            {
                Instantiate(cone, new Vector3(transform.position.x + x, initial_y + y / 2, transform.position.z), transform.rotation);

                max_cone++;
            }
        }
        else if (random_number_car <= spawn_rate_car)
        {
            if(max_car < 2)
            {
                Instantiate(car, new Vector3(transform.position.x + x, initial_y, transform.position.z), transform.rotation);

                max_car++;
            }
        }
        else if (random_number_shield <= spawn_rate_shield)
        {
            if(max_shield < 1)
            {
                Instantiate(shield, new Vector3(transform.position.x + x, initial_y + y, transform.position.z), transform.rotation);

                max_shield++;
            }
        }
        else if (random_number_magnet <= spawn_rate_magnet)
        {
            if (max_magnet < 1)
            {
                Instantiate(magnet, new Vector3(transform.position.x + x, initial_y + y, transform.position.z), transform.rotation);

                max_magnet++;
            }
        }
    }

    void level_tuning(int level)
    {
        switch (level)
        {
            /*case 0:
                {
                    adjust_value(10, 10, 1, 1, 1);
                    spawn_interval = 1.3f;

                    break;
                }*/
            case 0:
                {
                    adjust_value(15, 15, 3, 1, 1);
                    spawn_interval = 1.2f;

                    break;
                }
            case 1:
                {
                    adjust_value(20, 20, 5, 1, 1);
                    spawn_interval = 1.1f;

                    break;
                }
            case 2:
                {
                    adjust_value(25, 25, 7, 1, 1);
                    spawn_interval = 1;

                    break;
                }
        }
    }

    void adjust_value(int spawn_rate_coin, int spawn_rate_cone, int spawn_rate_car, int spawn_rate_shield, int spawn_rate_magnet)
    {
        this.spawn_rate_coin = spawn_rate_coin;
        this.spawn_rate_cone = spawn_rate_cone;
        this.spawn_rate_car = spawn_rate_car;
        this.spawn_rate_shield = spawn_rate_shield;
        this.spawn_rate_magnet = spawn_rate_magnet;
    }
}
