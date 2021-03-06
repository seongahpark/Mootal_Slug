using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOEnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] public enemyControl ec;
    private Animator animator;
    private SpriteRenderer rend;
    private Collider2D collision;

    [SerializeField] private int miniEnemyHp = 10;
    private static float attackMaxTime = 3.0f;
    private float attackTime = attackMaxTime;
    [SerializeField] private static float posMaxTime = 1.5f;
    [SerializeField] private float posTime = posMaxTime / 2.0f;

    private int pos = 1;
    private bool dropChk = false;
    // Start is called before the first frame update
    void Start()
    {
        ec = GameObject.Find("Enemy").GetComponent<enemyControl>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collision = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        attackTime -= Time.deltaTime;
        if(attackTime <= 0)
        {
            attackTime = attackMaxTime;
            Attack();
        }

        if (miniEnemyHp <= 0)
        {
            if(!dropChk) DropItem(); // 일정 확률로 아이템 드롭
            collision.isTrigger = true;
            animator.SetBool("isDestroyed", true);
            Destroy(this.gameObject, 2.0f);
        }
        else Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "P_Bullet")
        {
            StartCoroutine(ec.Blink(rend));
            miniEnemyHp--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "P_Bomb")
        {
            StartCoroutine(ec.Blink(rend));
            miniEnemyHp -= 3;
        }
    }
    private void Move()
    {
        posTime -= Time.deltaTime;
        if (posTime > 0) pos = -1;
        if (posTime < 0) pos = 1;
        if (posTime < -posMaxTime) posTime += posMaxTime*2;
        transform.Translate(Vector3.down * Time.deltaTime * pos * 0.5f);
    }
    private void Attack()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.3f;
        pos.z = -1.0f;
        Instantiate(bulletPrefab, pos, Quaternion.identity);
    }

    private void DropItem()
    {
        dropChk = true;
        int rand = Random.Range(0, 100);
        Debug.Log("rand " + rand);
        if(rand <= 40)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
