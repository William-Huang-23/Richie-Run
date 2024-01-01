using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class SignIn : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenTab(string url);

    /*private const string email_pattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
              + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
    
    private const string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSerc0-4nZvh2K8svl2WXvbm6ylLmE_y5LUMNnyUR7eaN4-OXg/formResponse";  // ctrl + f form action*/   // EMAIL STUFFS

    [Header("GAME DATA")]

    private int[,,] unit_count = new int[2, 3, 2];
    private int[,,] unit_price = new int[2, 3, 2];
    private float[,,,] history = new float[2, 3, 2, 24];

    private int[,] achievements_data = new int[10, 2];

    private bool remember_me = false;
    private float afk = 0;

    private string password = "monztr5123welm4coolz";

    [Header("CLASS")]

    [SerializeField]
    private Data data;

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private InvestmentData investment_data;
    private ShopData shop_data;

    [Header("UI")]

    [SerializeField]
    private Button welma_button;

    [SerializeField]
    private Button cutscene_button;

    [SerializeField]
    private Text error_text;

    [SerializeField]
    private GameObject empty;

    [Header("USERNAME")]

    [SerializeField]
    private GameObject username_panel;

    [SerializeField]
    private InputField username_field;

    [SerializeField]
    private Toggle remember_me_toggle;

    [Header("PASSWORD")]

    [SerializeField]
    private GameObject password_panel;

    [SerializeField]
    private InputField password_field;

    [Header("REGISTER POP UP")]

    [SerializeField]
    private GameObject register_pop_up;

    [SerializeField]
    private Text register_pop_up_text;
    [SerializeField]
    private Text register_pop_up_description;

    [Header("PASSWORD POP UP")]

    [SerializeField]
    private GameObject password_pop_up;

    [SerializeField]
    private Text password_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        audio_manager.play_sound("Button Sound");

        if (audio_manager.is_playing("Forest Ambience") == false)
        {
            audio_manager.play_sound_loop("Forest Ambience");
        }

        if (audio_manager.is_playing("River Sound") == false)
        {
            audio_manager.play_sound_loop("River Sound");
        }

        if (File.Exists(Application.persistentDataPath + "/Richie Data.txt"))
        {
            get_richie_data();
        }
        else
        {
            set_richie_data();
        }
        
        reset_game_data();
    }

    void Update()
    {
        afk = afk + Time.deltaTime;

        if(Input.anyKeyDown)
        {
            afk = 0;
        }

        if(afk >= 60 && register_pop_up.active == false && game_data.get_data_ready() == true)
        {
            cutscene();
        }

        if(Input.GetKeyDown(KeyCode.Return) && register_pop_up.active == false)
        {
            login();
        }
    }

    // public

    public void enter()
    {
        audio_manager.play_sound("Button Sound");

        if (password_panel.active == true)
        {
            password = password_field.text;
        }

        if (game_data.get_data_ready() == true)
        {
            login();
        }
    }

    public void back()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            username_field.text = "";
            password_field.text = "";
            error_text.text = "";

            password = "monztr5123welm4coolz";

            username_panel.SetActive(true);
            remember_me_toggle.gameObject.SetActive(true);
            welma_button.gameObject.SetActive(true);
            cutscene_button.gameObject.SetActive(true);

            password_panel.SetActive(false);
        }
    }

    public void toggle()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            if (remember_me_toggle.isOn == true)
            {
                remember_me = true;
            }
            else
            {
                remember_me = false;
            }
        }
    }

    public void welma()
    {
        audio_manager.play_sound("Button Sound");

        OpenTab("https://www.bca.co.id/id/Individu/layanan/e-banking/Welma");
    }

    public void cutscene()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            audio_manager.stop_sound("Forest Ambience");
            audio_manager.stop_sound("River Sound");

            SceneManager.LoadScene("Cutscene");
        }
    }

    public void register_pop_up_yes()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            register();
        }
    }

    public void register_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            register_pop_up.SetActive(false);

            username_field.text = "";

            error_text.text = "";
        }
    }

    // private

    /*void send_data_to_gform(string email)
    {
        WWWForm form = new WWWForm();

        form.AddField("entry.1589522249", email);

        byte[] rawdata = form.data;

        WWW www = new WWW(BASE_URL, rawdata);
    }

    bool check_email(string email)
    {
        if (email != null)
        {
            return Regex.IsMatch(email, email_pattern);
        }
        else
        {
            return false;
        }
    }*/

    void set_richie_data()
    {
        string path = Application.persistentDataPath + "/Richie Data.txt";

        StreamWriter writer = new StreamWriter(path, false);

        writer.WriteLine(",");

        writer.Close();
    }

    void get_richie_data()
    {
        string path = Application.persistentDataPath + "/Richie Data.txt";

        StreamReader reader = new StreamReader(path);

        string[] temp_array = reader.ReadToEnd().Split(char.Parse(","));

        username_field.text = temp_array[0];
        remember_me = temp_array[1].Contains("True");
        remember_me_toggle.isOn = remember_me;

        reader.Close();
    }

    void reset_game_data()
    {
        // local

        game_data.set_data_ready(true);
        empty.SetActive(false);

        game_data.set_character(0);
        game_data.set_level(0);

        game_data.set_first_main_menu(false);

        // player profile

        game_data.set_display_name("");
        game_data.set_money(0);
        game_data.set_kidal(0);
        game_data.set_login_streak(0);
        game_data.set_last_seen("");

        // investment data

        game_data.set_chosen_risk_level(0);

        // shop data

        game_data.set_income(0);
        game_data.set_shield(0);
        game_data.set_magnet(0);
        game_data.set_atlet(0);
        game_data.set_dokter(0);
        game_data.set_dam(0);
    }

    // playfab

    void login()
    {
        if (username_field.text == "")
        {
            error_text.color = Color.red;
            error_text.text = "Kolom Input Kosong!";
        }
        else if (username_field.text.Length < 3)
        {
            error_text.color = Color.red;
            error_text.text = "Username Invalid!";
        }
        else
        {
            game_data.set_data_ready(false);
            empty.SetActive(true);

            error_text.color = Color.yellow;
            error_text.text = "Loading...";

            var request = new LoginWithPlayFabRequest
            {
                Username = username_field.text,
                Password = password,
            };

            PlayFabClientAPI.LoginWithPlayFab(request, login_success, error);
        }
    }

    void login_success(LoginResult result)
    {
        if (remember_me == true)
        {
            string path = Application.persistentDataPath + "/Richie Data.txt";

            StreamWriter writer = new StreamWriter(path, false);

            writer.WriteLine(username_field.text + "," + remember_me);

            writer.Close();
        }
        else
        {
            set_richie_data();
        }

        error_text.color = Color.green;
        error_text.text = "Login sukses!";

        get_data();
    }

    void register()
    {
        game_data.set_data_ready(false);
        empty.SetActive(true);

        error_text.color = Color.yellow;
        error_text.text = "Loading...";

        password = Random.Range(10000000, 100000000).ToString();

        var request = new RegisterPlayFabUserRequest
        {
            Username = username_field.text,
            Password = password,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, register_success, error);
    }

    void register_success(RegisterPlayFabUserResult result)
    {
        //send_data_to_gform(email_field.text);

        set_data();
    }

    void set_data()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    unit_count[i, j, k] = 0;
                    unit_price[i, j, k] = 1500;

                    for (int l = 0; l < 24; l++)
                    {
                        history[i, j, k, l] = 0;
                    }
                }
            }
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                achievements_data[i, j] = 0;
            }
        }

        achievements_data[0, 0] = 1;

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(data.return_player_profile("null", 0, 0, 0, System.DateTime.Today.AddDays(-1).ToString()))},
                {"Investment", JsonConvert.SerializeObject(data.return_investment_products(-1, System.DateTime.Today.AddDays(-1).ToString(), System.DateTime.Today.AddDays(-1).ToString(), unit_count, unit_price, history))},
                {"Shop", JsonConvert.SerializeObject(data.return_shop_items(0, 0, 0, 0, 0, 0))},
                {"Achievements", JsonConvert.SerializeObject(data.return_achievements_data(achievements_data))},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        password_text.text = "Password: " + password;

        game_data.set_data_ready(true);
        empty.SetActive(false);

        password_pop_up.SetActive(true);

        register_pop_up.SetActive(false);
    }

    void get_data()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        player_profile = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player Profile"].Value);

        game_data.set_money(player_profile.money);
        game_data.set_kidal(player_profile.kidal);
        game_data.set_login_streak(player_profile.login_streak);
        game_data.set_last_seen(player_profile.last_seen.ToString());

        investment_data = JsonConvert.DeserializeObject<InvestmentData>(result.Data["Investment"].Value);

        game_data.set_chosen_risk_level(investment_data.chosen_risk_level);

        shop_data = JsonConvert.DeserializeObject<ShopData>(result.Data["Shop"].Value);

        game_data.set_income(shop_data.income);
        game_data.set_shield(shop_data.shield);
        game_data.set_magnet(shop_data.magnet);
        game_data.set_atlet(shop_data.atlet);
        game_data.set_dokter(shop_data.dokter);
        game_data.set_dam(shop_data.dam);

        game_data.set_data_ready(true);
        empty.SetActive(false);

        if (player_profile.display_name.ToString() == "null")
        {
            SceneManager.LoadScene("Set Display Name");
        }
        else
        {
            game_data.set_display_name(player_profile.display_name);

            SceneManager.LoadScene("Main Menu");
        }
    }
    
    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());

        if (error.Error == PlayFabErrorCode.AccountNotFound)
        {
            error_text.color = Color.red;
            error_text.text = "Akun tidak ditemukan!";

            register_pop_up.SetActive(true);

            register_pop_up_text.text = "Username: " + username_field.text;
            register_pop_up_description.text = "Akun dengan Username " + username_field.text + " belum terdaftar, apakah anda ingin mendaftarkan akun baru dengan Username tersebut?";
        }
        else if (error.Error == PlayFabErrorCode.InvalidUsernameOrPassword && password == "monztr5123welm4coolz")
        {
            error_text.color = Color.green;
            error_text.text = "Username terdeteksi! Mohon masukkan password!";

            password_panel.SetActive(true);

            username_panel.SetActive(false);
            remember_me_toggle.gameObject.SetActive(false);
            welma_button.gameObject.SetActive(false);
            cutscene_button.gameObject.SetActive(false);
        }
        else if (error.Error == PlayFabErrorCode.InvalidUsernameOrPassword)
        {
            error_text.color = Color.red;
            error_text.text = "Password invalid!";
        }

        game_data.set_data_ready(true);
        empty.SetActive(false);
    }
}
