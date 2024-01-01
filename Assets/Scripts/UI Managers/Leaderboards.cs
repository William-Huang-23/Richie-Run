using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Leaderboards : MonoBehaviour
{
    [Header("GAME DATA")]
    
    private string[] level_username = new string[20];
    private string[] level_score = new string[20];

    private int my_score_page = 0;
    private int current_page = 0;
    private int last_page = 0;

    private bool retry_indicator = false;
    private bool exit_indicator = false;

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [Header("LEADERBOARDS")]

    [SerializeField]
    private Text ranking_list;
    [SerializeField]
    private Text display_name_list;
    [SerializeField]
    private Text score_list;

    [SerializeField]
    private Button my_score_button;

    [Header("ARE YOU SURE POP UP")]

    [SerializeField]
    private GameObject are_you_sure_pop_up;

    [SerializeField]
    private Text are_you_sure_pop_up_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        if (audio_manager.is_playing("Forest Ambience") == false)
        {
            audio_manager.play_sound_loop("Forest Ambience");
        }

        if (audio_manager.is_playing("River Sound") == false)
        {
            audio_manager.play_sound_loop("River Sound");
        }

        get_data();
    }

    // public

    public void first()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            current_page = 0;

            show_leaderboard(current_page);
        }
    }

    public void prev()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if(current_page > 0)
            {
                current_page--;

                show_leaderboard(current_page);
            }
        }
    }

    public void next()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if(current_page < last_page)
            {
                current_page++;

                show_leaderboard(current_page);
            }
        }
    }

    public void last()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            current_page = last_page;

            show_leaderboard(current_page);
        }
    }

    public void my_score()
    {
        audio_manager.play_sound("Button Sound");

        if (my_score_page == -1)
        {
            
        }
        else
        {
            if (game_data.get_data_ready() == true)
            {
                current_page = my_score_page;

                show_leaderboard(current_page);
            }
        }
    }

    // private

    void show_leaderboard(int index)
    {
        if (game_data.get_data_ready() == true)
        {
            ranking_list.text = "";

            for (int i = (index + 1) * 5 - 5; i < (index + 1) * 5; i++)
            {
                ranking_list.text = ranking_list.text + (i + 1) + "\n";
            }

            display_name_list.text = level_username[index];
            score_list.text = level_score[index];
        }
    }

    // playfab

    void get_data()
    {
        game_data.set_data_ready(false);

        var request = new GetLeaderboardRequest
        {
            StatisticName = "Leaderboards",
            StartPosition = 0,
            MaxResultsCount = 100
        };

        PlayFabClientAPI.GetLeaderboard(request, get_data_success, error);
    }

    void get_data_success(GetLeaderboardResult result)
    {
        int i = 0;
        int j = 0;

        foreach (var item in result.Leaderboard)
        {
            if (string.Equals(game_data.get_display_name(), item.DisplayName))
            {
                level_username[i] = level_username[i] + "( " + item.DisplayName + " )";

                my_score_page = i;
            }
            else
            {
                level_username[i] = level_username[i] + item.DisplayName;
            }

            level_username[i] = level_username[i] + "\n";

            level_score[i] = level_score[i] + item.StatValue;
            level_score[i] = level_score[i] + "\n";

            if ((j + 1) % 5 == 0 && j > 0)
            {
                i++;
            }

            j++;
        }

        last_page = i;

        game_data.set_data_ready(true);

        if (my_score_page == -1)
        {
            current_page = 0;

            show_leaderboard(current_page);
        }
        else
        {
            current_page = my_score_page;

            show_leaderboard(current_page);
        }
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
