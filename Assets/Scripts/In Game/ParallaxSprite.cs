using UnityEngine;

public class ParallaxSprite : MonoBehaviour
{
    [Header("GAME DATA")]

    [SerializeField]
    private int total;

    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject head;

    [Header("UI")]

    [SerializeField]
    private Sprite[] sprite;

    [SerializeField]
    private SpriteRenderer sprite_renderer;

    void Start()
    {
        sprite_renderer.sprite = sprite[Random.Range(0, total)];
    }

    void Update()
    {
        if (transform.position.x < -19.04762f)
        {
            transform.position = new Vector3(head.transform.position.x + 19.04762f, transform.position.y, transform.position.z);

            change_sprite();
        }
    }

    void change_sprite()
    {
        sprite_renderer.sprite = sprite[Random.Range(0, total)];
    }
}
