using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStop : MonoBehaviour
{
    public GameManager man;

    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Vector3 temp = man.player.transform.position;
            if (this.tag == "Left Wall")
            {
                temp.x+=.5f;
            }
            else
            {
                temp.x-=.5f;
            }
            man.player.transform.position = temp;
        }
    }
}
