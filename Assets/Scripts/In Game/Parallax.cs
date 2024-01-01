using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject head;

    void Update()
    {
        if (transform.position.x < -19.04762f)
        {
            transform.position = new Vector3(head.transform.position.x + 19.04762f, transform.position.y, transform.position.z);
        }
    }
}
