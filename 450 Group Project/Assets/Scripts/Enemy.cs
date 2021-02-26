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

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
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
}
