using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy:MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isDead = false;
        isColliding = false;
    }

    public void TakeDamage(int damage)
    {
        hp-=damage;
        if(hp<=0)
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
        this.transform.position = vec;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
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
}
