using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Vector2 targetPos = new Vector2(1, 1);
    Rigidbody2D rigid2d;
    public float speed=1;
    public float restTime = 1;
    public float restTimer=0;
    BoxCollider2D boxCol;
    Animator anim;

	// Use this for initialization
	void Start () {
        rigid2d = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        rigid2d.MovePosition(Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime));
        if (GameManager.Instance.foodCount <= 0||GameManager.Instance.isEnd==true)
        {
            return;
        }
        
        restTimer += Time.deltaTime;
        if (restTimer < restTime) return;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (h > 0)
        {
            v = 0;
        }
        if(h!=0||v!=0)
        {
            GameManager.Instance.ReduceFood(1);
            boxCol.enabled = false;
            RaycastHit2D hit= Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            boxCol.enabled = true;
            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case "OutWall":
                            break;
                    case "Wall":
                        hit.collider.SendMessage("TakeDamage");
                        anim.SetTrigger("Attack");
                        break;
                    case "food":
                        GameManager.Instance.AddFood(10);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "soda":
                        GameManager.Instance.AddFood(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "enemy":
                        break;
                }
            }
            GameManager.Instance.OnPlayerMove();
            //Debug.Log("0"+targetPos);
            restTimer = 0;

        }
        
    }
    public void TakeDamage(int damage)
    {
        GameManager.Instance.ReduceFood(damage);

        anim.SetTrigger("Damage");
    }
}
