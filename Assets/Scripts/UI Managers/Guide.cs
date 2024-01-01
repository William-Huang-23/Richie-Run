using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [Header("GAME DATA")]

    private int page = 0;
    private int max_page = 6;

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [SerializeField]
    private Text title_text;
    [SerializeField]
    private Text page_text;

    [Header("HOW TO PLAY")]

    [SerializeField]
    private GameObject how_to_play;

    [SerializeField]
    private GameObject[] how_to_play_guide;

    [SerializeField]
    private GameObject keyboard;
    [SerializeField]
    private GameObject joystick;

    [Header("HOW TO INVEST")]

    [SerializeField]
    private GameObject how_to_invest;

    [SerializeField]
    private GameObject[] how_to_invest_guide;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        if (game_data.get_on_PC() == true)
        {
            keyboard.SetActive(true);
            joystick.SetActive(false);
        }
        else
        {
            keyboard.SetActive(false);
            joystick.SetActive(true);
        }

        page_text.text = "1";

        how_to_play_guide[0].SetActive(true);
        how_to_invest_guide[0].SetActive(true);
    }

    // public

    public void show_how_to_play()
    {
        audio_manager.play_sound("Button Sound");

        how_to_play_guide[page].SetActive(false);
        how_to_invest_guide[page].SetActive(false);

        title_text.text = "Cara Bermain";

        page = 0;
        page_text.text = (page + 1).ToString();

        how_to_play_guide[page].SetActive(true);
        how_to_invest_guide[page].SetActive(true);

        how_to_play.SetActive(true);
        how_to_invest.SetActive(false);
    }

    public void show_how_to_invest()
    {
        audio_manager.play_sound("Button Sound");

        how_to_play_guide[page].SetActive(false);
        how_to_invest_guide[page].SetActive(false);

        title_text.text = "Cara Berinvestasi";

        page = 0;
        page_text.text = (page + 1).ToString();

        how_to_play_guide[page].SetActive(true);
        how_to_invest_guide[page].SetActive(true);

        how_to_play.SetActive(false);
        how_to_invest.SetActive(true);
    }

    public void next()
    {
        audio_manager.play_sound("Button Sound");

        how_to_play_guide[page].SetActive(false);
        how_to_invest_guide[page].SetActive(false);

        if (page == max_page)
        {
            page = 0;
        }
        else
        {
            page++;
        }

        page_text.text = (page + 1).ToString();

        how_to_play_guide[page].SetActive(true);
        how_to_invest_guide[page].SetActive(true);
    }

    public void prev()
    {
        audio_manager.play_sound("Button Sound");

        how_to_play_guide[page].SetActive(false);
        how_to_invest_guide[page].SetActive(false);

        if (page== 0)
        {
            page = max_page;
        }
        else
        {
            page--;
        }

        page_text.text = (page + 1).ToString();

        how_to_play_guide[page].SetActive(true);
        how_to_invest_guide[page].SetActive(true);
    }
}
