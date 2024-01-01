using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;

    [Header("GAME DATA")]
    
    private float speed = 0;

    [SerializeField]
    private float extra_speed = 0;
    

    void Start()
    {
        game_manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        level_tuning(game_data.get_level());
    }
    
    void Update()
    {
        if (game_manager.get_game_start() == true)
        {
            move();
        }
    }

    void move()
    {
        transform.position = transform.position + Vector3.left * (speed + extra_speed + game_manager.get_extra_speed()) * Time.deltaTime;
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
