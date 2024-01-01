using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [Header("CLASS")]
    
    private GameData game_data;
    private AudioManager audio_manager;

    [Header("UI")]

    [SerializeField]
    private Text title_text;

    [SerializeField]
    private Text team_text;
    [SerializeField]
    private GameObject sounds_text;
    [SerializeField]
    private GameObject fonts_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        team();
    }

    // public
    
    public void team()
    {
        audio_manager.play_sound("Button Sound");

        team_text.gameObject.SetActive(true);
        sounds_text.SetActive(false);
        fonts_text.SetActive(false);

        title_text.text = "Tim";
    }

    public void sound()
    {
        audio_manager.play_sound("Button Sound");

        team_text.gameObject.SetActive(false);
        sounds_text.SetActive(true);
        fonts_text.SetActive(false);

        title_text.text = "Suara";
    }

    public void font()
    {
        audio_manager.play_sound("Button Sound");

        team_text.gameObject.SetActive(false);
        sounds_text.SetActive(false);
        fonts_text.SetActive(true);

        title_text.text = "Font";
    }
}
