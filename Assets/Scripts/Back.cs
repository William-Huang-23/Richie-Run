using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();
    }

    public void back(string scene_name)
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            SceneManager.LoadScene(scene_name);
        }
    }
}
