using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    private Transform player;

    private Vector2 targetPos;
    Animator anim;
    public int damage=10;
    public float speed = 3;
    private Rigidbody2D rigid;
    BoxCollider2D boxCol;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        GameManager.Instance.enemyList.Add(this);
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        rigid.MovePosition(Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime));
	}
    public void Move()
    {
        Vector2 dir = player.position - transform.position;
        if (dir.magnitude < 1.1)
        {
            anim.SetTrigger("Attack");
            player.SendMessage("TakeDamage", damage);
        }
        else
        {
            float x=0, y=0;
            if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
            {
                if (dir.y < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
            }
            else
            {
                if (dir.x < 0)
                {
                    x = -1;
                }
                else
                {
                    x = 1;
                }
            }
            boxCol.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(x, y));
            boxCol.enabled = true;

            if (hit.transform == null)
            {
                targetPos += new Vector2(x, y);
            }
            else
            {
                if (hit.transform.tag == "soda" || hit.transform.tag == "food")
                {
                    targetPos += new Vector2(x, y);
                }
            }
        }

    }
}
