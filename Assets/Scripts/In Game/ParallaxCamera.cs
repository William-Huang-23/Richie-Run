using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    [Header("GAME DATA")]

    private float length = 0;

    private float start_position = 0;

    [SerializeField]
    private float parallax_effect;

    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject camera;

    void Start()
    {
        start_position = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallax_effect));
        float distance = (camera.transform.position.x * parallax_effect);

        transform.position = new Vector3(start_position + distance, transform.position.y, transform.position.z);

        if(temp > start_position + length)
        {
            start_position = start_position + length;
        }
        else if(temp < start_position - length)
        {
            start_position = start_position - length;
        }
    }
}
