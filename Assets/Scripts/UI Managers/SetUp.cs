using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUp : MonoBehaviour
{
    [Header("CLASS")]

    [SerializeField]
    private GameObject prefab_game_data;
    private GameData game_data;

    [SerializeField]
    private GameObject prefab_audio_manager;
    private AudioManager audio_manager;

    void Start()
    {
        Instantiate(prefab_game_data);
        Instantiate(prefab_audio_manager);

        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        if (Application.isMobilePlatform == true)
        {
            game_data.set_on_PC(false);
        }
        else
        {
            game_data.set_on_PC(true);
        }
    }

    void Update()
    {
        if (GameObject.Find("Game Data(Clone)") != null && GameObject.Find("Audio Manager(Clone)") != null)
        {
            SceneManager.LoadScene("Cutscene");
        }
    }

    // C:/Users/William Huang/AppData/LocalLow/DefaultCompany/Richie Run Unity
}
