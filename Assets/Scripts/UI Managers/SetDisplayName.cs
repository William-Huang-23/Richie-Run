using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class SetDisplayName : MonoBehaviour
{
    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;

    [Header("UI")]

    [SerializeField]
    private Text error_text;

    [SerializeField]
    private GameObject empty;

    [Header("DISPLAY NAME")]

    [SerializeField]
    private InputField display_name_field;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        get_data();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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

        set_data();
    }

    void set_data()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(player_profile)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        error_text.color = Color.green;
        error_text.text = "Setel nama sukses!";

        game_data.set_display_name(display_name_field.text);

        game_data.set_data_ready(true);
        empty.SetActive(false);

        SceneManager.LoadScene("Main Menu");
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
