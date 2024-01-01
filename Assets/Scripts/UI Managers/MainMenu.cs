using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class MainMenu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenTab(string url);

    [Header("GAME DATA")]

    private int[] daily_rewards = { 1000, 2000, 3000, 5000, 8000, 13000, 21000 };

    private bool claim_once = false;

    private int achievement_value = 0;

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private AchievementsData achievements_data;

    [Header("UI")]

    [Header("DAILY LOGIN")]

    [SerializeField]
    private GameObject daily_login_pop_up;

    [SerializeField]
    private Text[] daily_rewards_text;

    [SerializeField]
    private Image[] daily_login_check;
    [SerializeField]
    private Sprite arrow_sprite;
    [SerializeField]
    private Text today_text;

    [SerializeField]
    private Text claim_text;

    [Header("SIGN OUT")]

    [SerializeField]
    private GameObject sign_out_pop_up;

    void Start()
    {
        Time.timeScale = 1f;

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

        daily_login();
    }

    // public

    public void play()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Select Character");
    }

    public void invest()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_chosen_risk_level() == -1)
        {
            SceneManager.LoadScene("Set Investment Risk");
        }
        else
        {
            SceneManager.LoadScene("Investment");
        }
    }

    public void guide()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Guide");
    }

    public void credits()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Credits");
    }

    public void settings()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Settings");
    }

    public void welma()
    {
        audio_manager.play_sound("Button Sound");

        OpenTab("https://www.bca.co.id/id/Individu/layanan/e-banking/Welma");
    }

    public void shop()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Shop");
    }

    public void achievements()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Achievements");
    }

    public void leaderboards()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Leaderboards");
    }

    public void sign_out()
    {
        audio_manager.play_sound("Button Sound");

        sign_out_pop_up.SetActive(true);
    }

    public void sign_out_pop_up_yes()
    {
        audio_manager.play_sound("Button Sound");

        SceneManager.LoadScene("Sign In");
    }

    public void sign_out_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        sign_out_pop_up.SetActive(false);
    }

    public void claim()
    {
        audio_manager.play_sound("Button Sound");

        if (claim_once == false)
        {
            claim_once = true;

            switch (game_data.get_login_streak())
            {
                case 0:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[0]);

                        achievement_value = 1;

                        break;
                    }
                case 1:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[1]);

                        achievement_value = 2;

                        break;
                    }
                case 2:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[2]);

                        achievement_value = 3;

                        break;
                    }
                case 3:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[3]);

                        achievement_value = 4;

                        break;
                    }
                case 4:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[4]);

                        achievement_value = 5;

                        break;
                    }
                case 5:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[5]);

                        achievement_value = 6;

                        break;
                    }
                case 6:
                    {
                        game_data.set_money(game_data.get_money() + daily_rewards[6]);

                        achievement_value = 7;

                        break;
                    }
            }

            game_data.set_last_seen(System.DateTime.Today.ToString());

            if (game_data.get_login_streak() < 7)
            {
                game_data.set_login_streak(game_data.get_login_streak() + 1);
            }
            else
            {
                game_data.set_login_streak(0);
            }

            get_data();
        }
        else
        {
            daily_login_pop_up.SetActive(false);
        }
    }

    // private

    void daily_login()
    {
        if (game_data.get_first_main_menu() == false)
        {
            game_data.set_first_main_menu(true);

            var today = System.DateTime.Today;
            var last_seen = System.DateTime.Parse(game_data.get_last_seen());

            if (today.AddDays(-1) == last_seen)
            {
                if (game_data.get_login_streak() >= 7)
                {
                    game_data.set_login_streak(0);
                }

                int i = 0;

                for (i = 0; i < game_data.get_login_streak(); i++)
                {
                    daily_login_check[i].gameObject.SetActive(true);
                }

                daily_login_check[i].gameObject.SetActive(true);
                daily_login_check[i].sprite = arrow_sprite;
                daily_login_check[i].gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);

                today_text.gameObject.transform.position = new Vector3(daily_login_check[i].gameObject.transform.position.x, today_text.gameObject.transform.position.y, today_text.gameObject.transform.position.z);
            }
            else if (today == last_seen)
            {
                int i = 0;

                for (i = 0; i < game_data.get_login_streak(); i++)
                {
                    daily_login_check[i].gameObject.SetActive(true);
                }

                today_text.gameObject.transform.position = new Vector3(daily_login_check[i - 1].gameObject.transform.position.x, today_text.gameObject.transform.position.y, today_text.gameObject.transform.position.z);

                claim_once = true;

                claim_text.text = "Lanjut";
            }
            else
            {
                game_data.set_login_streak(0);

                today_text.gameObject.transform.position = new Vector3(daily_login_check[0].gameObject.transform.position.x, today_text.gameObject.transform.position.y, today_text.gameObject.transform.position.z);
            }

            for (int i = 0; i < 7; i++)
            {
                daily_rewards_text[i].text = daily_rewards[i].ToString();
            }

            daily_login_pop_up.SetActive(true);
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

        player_profile.money = game_data.get_money();
        player_profile.login_streak = game_data.get_login_streak();
        player_profile.last_seen = game_data.get_last_seen();

        achievements_data = JsonConvert.DeserializeObject<AchievementsData>(result.Data["Achievements"].Value);

        if (achievement_value > achievements_data.achievements_data[8, 0])
        {
            achievements_data.achievements_data[8, 0] = achievement_value;
        }

        set_data();
    }

    void set_data()
    {
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
        daily_login_pop_up.SetActive(false);
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
