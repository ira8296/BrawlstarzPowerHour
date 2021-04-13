using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameManager man;
    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (man.hp < man.hpMax)
            {
                man.hp++;
            }
            Destroy(gameObject);
        }
    }
}
