using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wave;
    public List<GameObject> enemies;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        enemies = new List<GameObject>();
        enemies.Add(Instantiate(enemy));
        enemies.Add(Instantiate(enemy, new Vector3(-8f,5f,0), Quaternion.identity));
        enemies[0].GetComponent<Enemy>().isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<enemies.Count; i++)
        {
            if(enemies[i].GetComponent<Enemy>().isDead)
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
                continue;
            }

            if (enemies[i].GetComponent<Enemy>().isMoving)
            {
                Vector2 temp = enemies[i].transform.position;
                temp.x += .001f;
                enemies[i].transform.position = temp;
            }
        }
    }
}
