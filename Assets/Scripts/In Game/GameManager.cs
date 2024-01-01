using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [Header("GAME DATA")]

    private int score = 0;

    private float game_timer = 0;
    private int game_timer_score_indicator = 1;
    private float game_timer_escalate = 0;

    private int addition = 0;
    private float extra_speed = 0;

    private float game_start_timer = 6;
    private bool game_start = false;
    
    private bool game_over_status = false;
    private bool save_data = false;

    private bool retry_indicator = false;
    private bool exit_indicator = false;

    [Header("CLASS")]

    [SerializeField]
    private Data data;

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private AchievementsData achievements_data;

    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject richie;

    [SerializeField]
    private GameObject lose;

    [Header("UI")]

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private Text countdown_text;

    [SerializeField]
    private Text score_text;

    [SerializeField]
    private Text timer_text;

    [Header("PAUSE")]

    [SerializeField]
    private GameObject pause_UI;

    [Header("ARE YOU SURE POP UP")]

    [SerializeField]
    private GameObject are_you_sure_pop_up;

    [SerializeField]
    private Text are_you_sure_text;

    [Header("GAME OVER")]

    [SerializeField]
    private GameObject game_over_UI;

    [SerializeField]
    private Text total_money_text;

    [Header("ANIMATOR")]

    [SerializeField]
    private Animator animator;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        audio_manager.play_sound("Countdown");
        audio_manager.play_sound_loop("In Game BGM");

        level_tuning(game_data.get_level());

        get_data();
    }
    
    void Update()
    {
        if(game_start == false && game_over_status == false)
        {
            if(game_start_timer == 6)
            {
                countdown_text.gameObject.SetActive(true);
            }

            game_start_timer = game_start_timer - Time.deltaTime;

            if((int)game_start_timer <= 2)
            {
                countdown_text.text = "Mulai!";
            }
            else
            {
                countdown_text.text = "" + ((int)game_start_timer - 2).ToString();
            }

            if(game_start_timer <= 0.5)
            {
                game_start_timer = 6;
                game_start = true;

                animator.SetBool("Start", true);

                countdown_text.gameObject.SetActive(false);
            }
        }
        else
        {
            if (game_over_status == false)
            {
                game_timer = game_timer + Time.deltaTime;

                timer_text.text = "" + ((int)game_timer).ToString();

                if (game_timer > game_timer_score_indicator)
                {
                    score++;
                    game_timer_score_indicator++;
                }
            }

            game_timer_escalate = game_timer_escalate + Time.deltaTime;

            if (game_timer_escalate >= 1)
            {
                game_timer_escalate = 0;

                extra_speed = extra_speed + 0.03f;
            }
        }

        score_text.text = "Rp" + score.ToString();
    }

    // public

    public void pause()
    {
        audio_manager.play_sound("Button Sound");

        if (game_start == true && Time.timeScale == 1f)
        {
            Time.timeScale = 0f;

            audio_manager.pause_sound("In Game BGM");

            pause_UI.SetActive(true);
        }
    }

    public void resume()
    {
        audio_manager.play_sound("Button Sound");

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;

            audio_manager.unpause_sound("In Game BGM");

            pause_UI.SetActive(false);
        }
    }

    public void invest()
    {
        audio_manager.play_sound("Button Sound");

        if (game_over_status == true && game_data.get_data_ready() == true)
        {
            Time.timeScale = 1f;

            if (game_data.get_chosen_risk_level() == -1)
            {
                SceneManager.LoadScene("Set Investment Risk");
            }
            else
            {
                SceneManager.LoadScene("Investment");
            }
        }
    }

    public void shop()
    {
        audio_manager.play_sound("Button Sound");

        if (game_over_status == true && game_data.get_data_ready() == true)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("Shop");
        }
    }

    public void retry()
    {
        audio_manager.play_sound("Button Sound");

        if (game_over_status == true && game_data.get_data_ready() == true)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("In Game");
        }
        else if (game_over_status == false)
        {
            retry_indicator = true;
            exit_indicator = false;

            are_you_sure_text.text = "Apakah anda yakin ingin mengulangi permainan?";

            are_you_sure_pop_up.SetActive(true);
        }
    }

    public void exit()
    {
        audio_manager.play_sound("Button Sound");

        if (game_over_status == true && game_data.get_data_ready() == true)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene("Main Menu");
        }
        else if (game_over_status == false)
        {
            retry_indicator = false;
            exit_indicator = true;

            are_you_sure_text.text = "Apakah anda yakin ingin kembali ke Main Menu?";

            are_you_sure_pop_up.SetActive(true);
        }
    }

    public void are_you_sure_yes()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            audio_manager.stop_sound("In Game BGM");

            Time.timeScale = 1f;

            if (retry_indicator == true)
            {
                SceneManager.LoadScene("In Game");
            }
            else if (exit_indicator == true)
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    public void are_you_sure_no()
    {
        audio_manager.play_sound("Button Sound");
        
        retry_indicator = false;
        exit_indicator = false;

        are_you_sure_pop_up.SetActive(false);
    }

    // getter setter

    public void add_score(int bonus)
    {
        score = score + addition + bonus + ((addition / 10) * game_data.get_income());
    }

    public void subtract_score(int bonus)
    {
        score = score - addition - bonus;
    
        if(score < 0)
        {
            score = 0;
        }
    }

    public float get_extra_speed()
    {
        return extra_speed;
    }

    public int get_addition()
    {
        return addition;
    }

    public bool get_game_start()
    {
        return game_start;
    }

    public bool get_game_over_status()
    {
        return game_over_status;
    }

    public void game_over()
    {
        game_data.set_data_ready(false);

        Time.timeScale = 0.5f;

        audio_manager.play_sound("Game Over Sound");
        audio_manager.stop_sound("In Game BGM");

        game_over_status = true;

        game_over_UI.SetActive(true);

        joystick.gameObject.SetActive(false);

        total_money_text.text = "Total Uang: Rp" + (score + game_data.get_money());

        if (save_data == false)
        {
            save_data = true;

            set_data();
        }
    }

    // private

    void level_tuning(int level)
    {
        switch (level)
        {
            /*case 0:
                {
                    addition = 10;

                    break;
                }*/
            case 0:
                {
                    addition = 10;

                    break;
                }
            case 1:
                {
                    addition = 20;

                    break;
                }
            case 2:
                {
                    addition = 40;

                    break;
                }
        }
    }

    // playfab

    void get_data()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        player_profile = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player Profile"].Value);
        achievements_data = JsonConvert.DeserializeObject<AchievementsData>(result.Data["Achievements"].Value);
    }

    void set_data()
    {
        game_data.set_money(game_data.get_money() + score);

        player_profile.money = game_data.get_money();

        achievements_data.achievements_data[1, 0] = 1;

        if (achievements_data.achievements_data[2, 1] == 0)
        {
            achievements_data.achievements_data[2, 0] = achievements_data.achievements_data[2, 0] + score;
        }

        if (achievements_data.achievements_data[3, 1] == 0 && achievements_data.achievements_data[3, 0] < score)
        {
            achievements_data.achievements_data[3, 0] = score;
        }

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(player_profile)},
                {"Achievements", JsonConvert.SerializeObject(achievements_data)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        set_leaderboard();
    }

    void set_leaderboard()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Leaderboards",
                    Value = score   // high score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, set_leaderboard_success, error);
    }

    void set_leaderboard_success(UpdatePlayerStatisticsResult result)
    {
        game_data.set_data_ready(true);
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
