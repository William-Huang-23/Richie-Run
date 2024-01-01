using UnityEngine;

public class RichieSprite : MonoBehaviour
{
    [Header("CLASS")]

    private GameManager game_manager;

    private GameData game_data;

    [Header("UI")]

    [SerializeField]
    private Sprite[] character_sprite;

    [SerializeField]
    private SpriteRenderer sprite_renderer;

    [Header("ANIMATOR")]

    [SerializeField]
    private Animator animator;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        sprite_renderer.sprite = character_sprite[game_data.get_character()];
        animator.SetInteger("Richie", game_data.get_character());
    }
}
