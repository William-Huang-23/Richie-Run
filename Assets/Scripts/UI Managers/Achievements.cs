using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class Achievements : MonoBehaviour
{
    [Header("GAME DATA")]

    private int[] reward = { 500, 500, 1000, 1000, 500, 500, 500, 2000, 1000, 5000 };
    private int[] progress = { 1, 1, 15000, 3500, 1, 1, 1, 5, 7, 1 };

    [Header("CLASS")]

    [SerializeField]
    private Data data;

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private AchievementsData achievements_data;

    [Header("UI")]

    [SerializeField]
    private Text money_text;

    [SerializeField]
    private Scrollbar scrollbar;

    [Header("ACHIEVEMENTS")]

    [SerializeField]
    private Text[] description_text;
    [SerializeField]
    private Text[] reward_text;
    [SerializeField]
    private Text[] progress_text;

    [SerializeField]
    private Image[] locked_sign;
    [SerializeField]
    private Sprite[] locked_sign_sprite;

    [SerializeField]
    private Text[] claim_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        scrollbar.value = 1;

        set_UI();

        get_data();
    }

    // public

    public void claim(int index)
    {
        audio_manager.play_sound("Button Sound");
        
        if (game_data.get_data_ready() == true)
        {
            if (achievements_data.achievements_data[index, 0] >= progress[index])
            {
                game_data.set_data_ready(false);

                game_data.set_money(game_data.get_money() + reward[index]);

                player_profile.money = game_data.get_money();
                achievements_data.achievements_data[index, 1] = 1;

                set_data();
            }
        }
    }

    // private

    void set_UI()
    {
        money_text.text = "Rp" + game_data.get_money().ToString();

        description_text[0].text = "Buat akun dan login ke dalam game";
        description_text[1].text = "Mainkan play mode sebanyak x1";
        description_text[2].text = "Kumpulkan uang sebanyak Rp" + progress[2] + " lewat mode Play";
        description_text[3].text = "Kumpulkan uang sebanyak Rp" + progress[3] + " lewat mode Play dalam 1 sesi permainan";
        description_text[4].text = "Berinvestasi di Reksa Dana sebanyak x1";
        description_text[5].text = "Jual Reksa Dana yang pernah dibeli sebanyak x1";
        description_text[6].text = "Beli upgrade atau karakter dari toko sebanyak x1";
        description_text[7].text = "Unlock semua upgrade dan karakter yang tersedia di toko";
        description_text[8].text = "Ambil hadiah hari ke-7 dari login harian";
        description_text[9].text = "Beli bahan bangunan yang diperlukan untuk membetulkan bendungan di toko";

        for (int i = 0; i < description_text.Length ; i++)
        {
            reward_text[i].text = "Hadiah: Rp"+ reward[i];
            progress_text[i].text = "Progres: 0/" + progress[i];
        }
    }

    void reset_UI()
    {
        money_text.text = "Rp" + game_data.get_money().ToString();

        for (int i = 0; i < description_text.Length; i++)
        {
            if (achievements_data.achievements_data[i, 0] > progress[i])
            {
                progress_text[i].text = "Progres: " + progress[i] + "/" + progress[i];
            }
            else
            {
                progress_text[i].text = "Progres: " + achievements_data.achievements_data[i, 0] + "/" + progress[i];
            }

            if (achievements_data.achievements_data[i, 0] >= progress[i] && achievements_data.achievements_data[i, 1] == 0)
            {
                locked_sign[i].sprite = locked_sign_sprite[2];
                claim_text[i].text = "Klaim\nHadiah";
            }
            else
            {
                locked_sign[i].sprite = locked_sign_sprite[achievements_data.achievements_data[i, 1]];
                claim_text[i].text = "";
            }
        }
    }

    // playfab

    void get_data()
    {
        game_data.set_data_ready(false);

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        player_profile = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player Profile"].Value);
        achievements_data = JsonConvert.DeserializeObject<AchievementsData>(result.Data["Achievements"].Value);

        game_data.set_data_ready(true);

        reset_UI();
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
        game_data.set_data_ready(true);

        reset_UI();
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
