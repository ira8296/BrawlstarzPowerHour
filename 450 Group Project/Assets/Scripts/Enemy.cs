using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Zigzag,
    Air
}

public class Enemy : MonoBehaviour
{
    //Potentially change from public
    public float hp;
    public float hpMax;
    public bool isDead;
    public bool isMoving;
    public Transform trans;
    public bool isColliding;
    public GameManager man;
    public bool isLeft;
    public float speed;
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isDead = false;
        isColliding = false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
        }
    }

    void OnMouseDown()
    {
        if (!man.isDead)
        {
            TakeDamage(man.power);
        }
    }

    public void setPosition(Vector3 vec)
    {
        transform.position = vec;
    }

    public void setSpeed(int wave)
    {
        speed = Random.Range(1.0f * wave, 3.0f * wave);
        if (speed > 9)
        {
            speed = 9;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Hit");
            man.TakeDamage();
            StartCoroutine(KillEnemy());
        }
    }

    public IEnumerator KillEnemy()
    {
        yield return new WaitForSecondsRealtime(.1f);
        isDead = true;
    }

    public void Attack()
    {
        //Add code here for ranged attack for zigzag
    }

    public void Move()
    {
        if (man != null)
        {
            if (type == EnemyType.Normal)
            {
                transform.position = Vector2.MoveTowards(transform.position, man.player.transform.position, speed * Time.deltaTime);
            }
            else if (type == EnemyType.Air)
            {
                if (transform.position.y <= man.player.transform.position.y + 1)
                {
                    transform.position = Vector2.MoveTowards(transform.position, man.player.transform.position, speed * Time.deltaTime);
                }
            }
            else if (type == EnemyType.Zigzag)
            {
                if (speed > 1)
                {
                    if (Vector2.Distance(transform.position, man.player.transform.position) < 2f)
                    {
                        speed *= -1;
                        Attack();
                    }
                }
                else if (Vector2.Distance(transform.position, man.player.transform.position) > 3f)
                {
                    speed *= -1;
                }
                transform.position = Vector2.MoveTowards(transform.position, man.player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}