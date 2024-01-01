using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [Header("GAME DATA")]

    private int page = 0;
    private int max_page = 5;

    private string[] story = new string[6];

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [SerializeField]
    private Text story_text;

    [SerializeField]
    private GameObject[] cutscene;

    [SerializeField]
    private Button prev_button;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        audio_manager.play_sound("Cutscene BGM");

        set_value();

        story_text.text = story[0];
    }

    // public

    public void skip()
    {
        audio_manager.play_sound("Button Sound");

        sign_in();
    }

    public void next()
    {
        audio_manager.play_sound("Button Sound");

        cutscene[page].SetActive(false);

        if (page != max_page)
        {
            if (page == 0)
            {
                prev_button.gameObject.SetActive(true);
            }

            page++;
        }
        else
        {
            sign_in();
        }

        story_text.text = story[page];

        cutscene[page].SetActive(true);
    }

    public void prev()
    {
        audio_manager.play_sound("Button Sound");

        cutscene[page].SetActive(false);

        if (page != 0)
        {
            page--;

            if (page == 0)
            {
                prev_button.gameObject.SetActive(false);
            }
        }

        story_text.text = story[page];

        cutscene[page].SetActive(true);
    }

    // private

    void set_value()
    {
        story[0] = "Pada suatu hari di tengah hutan tropis Indonesia, terjadi banjir bandang yang menghancurkan bendungan para beaver...";
        story[1] = "Richie sebagai seorang beaver yang 'Responsible, Innovative, Caring, dan Helpful' bersedia membantu teman-temannya membangun bendungan kembali!";
        story[2] = "Namun untuk membangun bendungan kembali, mereka memerlukan uang untuk membeli bahan-bahan...";
        story[3] = "Oleh karena itu, Richie berinisiatif untuk pergi ke kota untuk mengumpulkan uang yang diperlukan!";
        story[4] = "Di kota, Richie berencana untuk mengumpulkan uang dengan cara bekerja di berbagai macam bidang profesi!";
        story[5] = "Yuk bantu Richie mengumpulkan uang agar dia dapat membangun bendungan kembali!";
    }

    void sign_in()
    {
        audio_manager.stop_sound("Cutscene BGM");

        SceneManager.LoadScene("Sign In");
    }
}
