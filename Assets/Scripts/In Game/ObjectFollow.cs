using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [Header("GAME DATA")]

    [SerializeField]
    private float extra_x;
    [SerializeField]
    private float extra_y;

    [Header("GAME OBJECT")]

    [SerializeField]
    private GameObject target;
    
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + extra_x, target.transform.position.y + extra_y, -1);
    }
}
