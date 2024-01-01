using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [Header("CLASS")]

    private GameData game_data;

    [Header("ANIMATOR")]

    [SerializeField]
    private Animator animator;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();

        if (game_data.get_dam() == 1)
        {
            animator.SetBool("Dam", true);
        }
    }
}
