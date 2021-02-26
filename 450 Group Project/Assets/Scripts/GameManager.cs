using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wave;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int hp;
    public bool isDead;
    public bool waveRunning;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hp = 1;
        isDead = false;
        wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !waveRunning)
        {
            waveRunning = true;
            GenerateWave();
        }
        if (!isDead && waveRunning)
        {
            enemies[0].GetComponent<Enemy>().isMoving = true;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetComponent<Enemy>().isDead)
                {
                    if (enemies.Count == 1)
                    {
                        waveRunning = false;
                    }
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

    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            isDead = true;
            Debug.Log("Game Over");
        }
    }

    public void GenerateWave()
    {
        wave++;
        enemies = new List<GameObject>();
        for (int i = 0; i < wave; i++)
        {
            enemies.Add(Instantiate(enemy));
        }
        enemies.Add(Instantiate(enemy, new Vector3(-8f, 5f, 0), Quaternion.identity));
    }
}
