using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [Header("LEVEL SELECT")]

    [SerializeField]
    private Text level_select_text;
    [SerializeField]
    private Text level_select_description;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        switch(game_data.get_level())
        {
            /*case 0:
                {
                    sangat_konservatif();

                    break;
                }*/
            case 0:
                {
                    konservatif();

                    break;
                }
            case 1:
                {
                    moderat();

                    break;
                }
            case 2:
                {
                    agresif();

                    break;
                }
        }
    }

    // public

    /*public void sangat_konservatif()
    {
        audio_manager.play_sound("Button Sound");

        game_data.set_level(0);

        level_select_text.text = "Tingkat Kesulitan: Sangat Konservatif";
        level_select_description.text = "Bermain dengan tingkat kesulitan sangat mudah.\n\nTingkat keberhasilan sangat tinggi namun keuntungan yang diperoleh sangat sedikit.\n\nUntung: +10 / Rugi: -10\n\nKecepatan: Lambat";
    }*/

    public void konservatif()
    {
        audio_manager.play_sound("Button Sound");

        game_data.set_level(0);

        level_select_text.text = "Tingkat Kesulitan: Konservatif";
        level_select_description.text = "Bermain dengan tingkat kesulitan mudah.\n\nTingkat keberhasilan tinggi namun keuntungan yang diperoleh lebih kecil.\n\nUntung: +10 / Rugi: -10\n\nKecepatan: Pelan";
    }

    public void moderat()
    {
        audio_manager.play_sound("Button Sound");

        game_data.set_level(1);

        level_select_text.text = "Tingkat Kesulitan: Moderat";
        level_select_description.text = "Bermain dengan tingkat kesulitan sedang.\n\nTingkat keberhasilan sedang namun keuntungan yang diperoleh sedang.\n\nUntung: +20 / Rugi: -20\n\nKecepatan: Sedang";
    }

    public void agresif()
    {
        audio_manager.play_sound("Button Sound");

        game_data.set_level(2);

        level_select_text.text = "Tingkat Kesulitan: Agresif";
        level_select_description.text = "Bermain dengan tingkat kesulitan sulit.\n\nTingkat keberhasilan rendah namun keuntungan yang diperoleh lebih besar.\n\nUntung: +40 / Rugi: -40\n\nKecepatan: Cepat";
    }

    public void play()
    {
        audio_manager.play_sound("Button Sound");

        audio_manager.stop_sound("Forest Ambience");
        audio_manager.stop_sound("River Sound");

        SceneManager.LoadScene("In Game");
    }
}
