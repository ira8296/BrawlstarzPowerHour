using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy:MonoBehaviour
{
    //Potentially change from public
    public int hp;
    public bool isDead;
    public bool isMoving;
    public Transform trans;
    public bool isColliding;
    public GameManager man;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isDead = false;
        isColliding = false;
        hp = 1;
    }

    public void TakeDamage()
    {
        hp--;
        if(hp<=0)
        {
            isDead = true;
        }
    }

    void OnMouseDown()
    {
        TakeDamage();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("Hit");
            man.TakeDamage();
        }
    }
}
