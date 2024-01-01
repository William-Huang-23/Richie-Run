using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class Settings : MonoBehaviour
{
    [Header("GAME DATA")]

    private int index = 0;

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private InvestmentData investment_data;

    [Header("UI")]

    [SerializeField]
    private Text error_text;

    [SerializeField]
    private GameObject empty;

    [Header("DISPLAY NAME")]

    [SerializeField]
    private InputField display_name_field;
    [SerializeField]
    private Text old_display_name;

    [Header("RISK LEVEL")]
    
    [SerializeField]
    private Button konservatif_button;
    [SerializeField]
    private Button moderat_button;
    [SerializeField]
    private Button agresif_button;

    [SerializeField]
    private GameObject risk_level_highlight;

    [Header("WARNING POP UP")]

    [SerializeField]
    private GameObject warning_pop_up;

    [SerializeField]
    private Text warning_pop_up_text;

    [Header("JOYSTICK")]

    [SerializeField]
    private Button left_button;
    [SerializeField]
    private Button right_button;

    [SerializeField]
    private GameObject joystick_highlight;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        if (game_data.get_on_PC() == false)
        {
            left_button.gameObject.SetActive(true);
            right_button.gameObject.SetActive(true);
        }

        get_data();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && warning_pop_up.active == false && game_data.get_data_ready() == true)
        {
            enter();
        }
    }

    // public

    public void enter()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (display_name_field.text == "")
            {
                error_text.color = Color.red;
                error_text.text = "Kolom Input Kosong!";
            }
            else
            {
                set_display_name();
            }
        }
    }

    /*public void sangat_konservatif()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (investment_data.chosen_risk_level != 0 && check_risk_level_status() == true && game_data.get_data_ready() == true)
            {
                index = 0;

                error_text.text = "";

                warning_pop_up_text.text = "Level Risiko: Sangat Konservatif";
                warning_pop_up.SetActive(true);
            }
            else if (investment_data.chosen_risk_level == 0 && game_data.get_data_ready() == true)
            {
                error_text.text = "";
            }
        }
    }*/

    public void konservatif()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (investment_data.chosen_risk_level != 0 && check_risk_level_status() == true && game_data.get_data_ready() == true)
            {
                index = 0;

                error_text.text = "";

                warning_pop_up_text.text = "Level Risiko: Konservatif";
                warning_pop_up.SetActive(true);
            }
            else if (investment_data.chosen_risk_level == 0 && game_data.get_data_ready() == true)
            {
                error_text.text = "";
            }
        }
    }

    public void moderat()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (investment_data.chosen_risk_level != 1 && check_risk_level_status() == true && game_data.get_data_ready() == true)
            {
                index = 1;

                error_text.text = "";

                warning_pop_up_text.text = "Level Risiko: Moderat";
                warning_pop_up.SetActive(true);
            }
            else if (investment_data.chosen_risk_level == 1 && game_data.get_data_ready() == true)
            {
                error_text.text = "";
            }
        }
    }

    public void agresif()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (investment_data.chosen_risk_level != 2 && check_risk_level_status() == true && game_data.get_data_ready() == true)
            {
                index = 2;

                error_text.text = "";

                warning_pop_up_text.text = "Level Risiko: Agresif";
                warning_pop_up.SetActive(true);
            }
            else if (investment_data.chosen_risk_level == 2 && game_data.get_data_ready() == true)
            {
                error_text.text = "";
            }
        }
    }

    public void warning_pop_up_yes()
    {
        if (game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);
            empty.SetActive(true);

            game_data.set_chosen_risk_level(index);

            investment_data.chosen_risk_level = index;
            investment_data.last_changed = System.DateTime.Today.ToString();

            set_data();
        }
    }

    public void warning_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            warning_pop_up.SetActive(false);
        }
    }

    public void left()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_kidal() != 0 && game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);
            empty.SetActive(true);

            game_data.set_kidal(0);
            player_profile.kidal = game_data.get_kidal();

            set_data();
        }
        else if (game_data.get_kidal() == 0 && game_data.get_data_ready() == true)
        {
            error_text.text = "";
        }
    }

    public void right()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_kidal() != 1 && game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);
            empty.SetActive(true);

            game_data.set_kidal(1);
            player_profile.kidal = game_data.get_kidal();

            set_data();
        }
        else if (game_data.get_kidal() == 1 && game_data.get_data_ready() == true)
        {
            error_text.text = "";
        }
    }

    // private

    bool check_risk_level_status()
    {
        if (investment_data.chosen_risk_level == -1)
        {
            error_text.color = Color.red;
            error_text.text = "Mohon pilih level risiko melalui menu Investasi terlebih dahulu!";

            return false;
        }

        var today = System.DateTime.Today;
        var last_changed = System.DateTime.Parse(investment_data.last_changed);

        if ((int)((today - last_changed).TotalDays) <= 0)
        {
            error_text.color = Color.red;
            error_text.text = "Level risiko baru diganti! Mohon coba lagi di besok hari!";

            return false;
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (investment_data.unit_count[i, index, j] > 0)
                {
                    error_text.color = Color.red;
                    error_text.text = "Jual semua aset sebelum mengganti level risiko!";

                    return false;
                }
            }
        }

        return true;
    }

    void reset_UI()
    {
        old_display_name.text = game_data.get_display_name();
        display_name_field.text = "";

        index = investment_data.chosen_risk_level;

        switch (investment_data.chosen_risk_level)
        {
            /*case 0:
                {
                    risk_level_highlight.transform.position = sangat_konservatif_button.gameObject.transform.position;

                    break;
                }*/
            case 0:
                {
                    risk_level_highlight.transform.position = konservatif_button.gameObject.transform.position;

                    break;
                }
            case 1:
                {
                    risk_level_highlight.transform.position = moderat_button.gameObject.transform.position;

                    break;
                }
            case 2:
                {
                    risk_level_highlight.transform.position = agresif_button.gameObject.transform.position;

                    break;
                }
        }

        if (game_data.get_kidal() == 1)
        {
            joystick_highlight.transform.position = right_button.gameObject.transform.position;
        }
        else if (game_data.get_kidal() == 0)
        {
            joystick_highlight.transform.position = left_button.gameObject.transform.position;
        }
    }

    // playfab

    void get_data()
    {
        game_data.set_data_ready(false);
        empty.SetActive(true);

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        player_profile = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player Profile"].Value);
        investment_data = JsonConvert.DeserializeObject<InvestmentData>(result.Data["Investment"].Value);

        reset_UI();

        game_data.set_data_ready(true);
        empty.SetActive(false);
    }

    void set_display_name()
    {
        game_data.set_data_ready(false);
        empty.SetActive(true);

        error_text.color = Color.yellow;
        error_text.text = "Loading...";

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = display_name_field.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, set_display_name_success, error);
    }

    void set_display_name_success(UpdateUserTitleDisplayNameResult result)
    {
        player_profile.display_name = display_name_field.text;

        game_data.set_display_name(display_name_field.text);

        set_data();
    }

    void set_data()
    {
        error_text.color = Color.yellow;
        error_text.text = "Loading...";

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(player_profile)},
                {"Investment", JsonConvert.SerializeObject(investment_data)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        reset_UI();

        error_text.color = Color.green;
        error_text.text = "Pengaturan sukses!";

        warning_pop_up.SetActive(false);

        game_data.set_data_ready(true);
        empty.SetActive(false);
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());

        error_text.color = Color.red;

        if (display_name_field.text.Length < 3)
        {
            error_text.text = "Nama terlalu pendek!";
        }
        else
        {
            error_text.text = "Pengguna dengan nama tampilan tersebut sudah ada!";
        }

        game_data.set_data_ready(true);
        empty.SetActive(false);
    }
}
