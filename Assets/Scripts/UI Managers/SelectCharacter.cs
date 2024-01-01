using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [Header("CHARACTER SELECT")]

    [SerializeField]
    private Text character_select_text;
    [SerializeField]
    private Text character_select_description;

    [SerializeField]
    private GameObject[] image;
    [SerializeField]
    private Text locked_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        change_sprite();
    }

    // public

    public void next()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_character() == 2)
        {
            game_data.set_character(0);
        }
        else
        {
            game_data.set_character(game_data.get_character() + 1);
        }

        change_sprite();
    }

    public void prev()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_character() == 0)
        {
            game_data.set_character(2);
        }
        else
        {
            game_data.set_character(game_data.get_character() - 1);
        }

        change_sprite();
    }

    public void select()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_character() == 0 || (game_data.get_character() == 1 && game_data.get_atlet() == 1) || (game_data.get_character() == 2 && game_data.get_dokter() == 1))
        {
            SceneManager.LoadScene("Select Level");
        }
    }

    // private

    void change_sprite()
    {
        if (game_data.get_character() == 0)
        {
            character_select_text.text = "Richie Pengusaha";
            character_select_description.text = "Koin +10% extra poin / Kerucut -10% extra poin";

            image[0].SetActive(true);
            image[1].SetActive(false);
            image[2].SetActive(false);
            image[3].SetActive(false);
            image[4].SetActive(false);

            locked_text.gameObject.SetActive(false);
        }
        else if (game_data.get_character() == 1)
        {
            character_select_text.text = "Richie Atlet";
            character_select_description.text = "Lari Richie +50 extra kecepatan";

            if (game_data.get_atlet() == 1)
            {
                image[0].SetActive(false);
                image[1].SetActive(true);
                image[2].SetActive(false);
                image[3].SetActive(false);
                image[4].SetActive(false);

                locked_text.gameObject.SetActive(false);
            }
            else
            {
                image[0].SetActive(false);
                image[1].SetActive(false);
                image[2].SetActive(false);
                image[3].SetActive(true);
                image[4].SetActive(false);

                locked_text.gameObject.SetActive(true);
            }
            
        }
        else if (game_data.get_character() == 2)
        {
            character_select_text.text = "Richie Dokter";
            character_select_description.text = "Durasi Perisai +5 extra detik";

            if (game_data.get_dokter() == 1)
            {
                image[0].SetActive(false);
                image[1].SetActive(false);
                image[2].SetActive(true);
                image[3].SetActive(false);
                image[4].SetActive(false);

                locked_text.gameObject.SetActive(false);
            }
            else
            {
                image[0].SetActive(false);
                image[1].SetActive(false);
                image[2].SetActive(false);
                image[3].SetActive(false);
                image[4].SetActive(true);

                locked_text.gameObject.SetActive(true);
            }
        }
    }
}
