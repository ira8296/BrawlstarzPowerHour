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
    public GameObject proj;
    public GameObject projectile;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        proj = man.projectile;
        isDead = false;
        isColliding = false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            GetComponent<Renderer>().enabled = false;//disable it so can spawn shield if needed
            if (0.2f>=Random.Range(0.0f,1.0f))
            {
                Instantiate(shield,gameObject.transform.position,Quaternion.identity);
            }
            Destroy(projectile);
        }
    }

    void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("PlayerAttack");
        if (!man.isDead && man.canMove)
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
            if(isLeft)
            {
                Vector2 temp = transform.position;
                temp.x -=.5f;
                transform.position = temp;
            }
            else
            {
                Vector2 temp = transform.position;
                temp.x += .5f;
                transform.position = temp;
            }
        }
        if (col.tag == "Shield")
        {
            Physics2D.IgnoreCollision(col,gameObject.GetComponent<Collider2D>());
        }
    }

    public void Attack()
    {
        //Add code here for ranged attack for zigzag
        if (isLeft)
        {
            Destroy(projectile);
            projectile = Instantiate(proj, new Vector2(this.transform.position.x + .5f, this.transform.position.y), Quaternion.identity);
            projectile.GetComponent<Projectile>().speed = speed;
        }
        else
        {
            Destroy(projectile);
            projectile = Instantiate(proj, new Vector2(this.transform.position.x - .5f, this.transform.position.y), Quaternion.identity);
            projectile.GetComponent<Projectile>().speed = speed;
        }
    }

    public void Move()
    {
        if (man != null)
        {
            if (man.player.transform.position.x < transform.position.x)
            {
                SpriteRenderer theSprite = gameObject.GetComponent<SpriteRenderer>();
                theSprite.flipX = true;
            }
            else
            {
                SpriteRenderer theSprite = gameObject.GetComponent<SpriteRenderer>();
                theSprite.flipX = false;
            }

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