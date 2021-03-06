using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBottom : MonoBehaviour
{
    public GameManager gm;
    [SerializeField] enemyControl ec;
    private Animator animator;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.chkBossStage)
        {
            if (!gm.gameClear)
            {
                if (ec.e_isAttack)
                {
                    StartCoroutine(ec.Blink(rend));
                }
            }
            if (gm.gameClear)
            {
                animator.SetBool("isDestroyed", true);
            }
        }
    }
}
