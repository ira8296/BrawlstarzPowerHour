using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wave;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int hp;
    public int points;//Points for upgrades
    public int hpPoints;
    public bool isDead;
    public bool waveRunning;
    public GameObject player;
    //Add in things for increasing health

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
                        //Where Wave Ends
                        waveRunning = false;
                    }
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    continue;
                }

                if (enemies[i].GetComponent<Enemy>().isMoving)
                {
                    Vector2 temp = enemies[i].transform.position;
                    temp.x += enemies[i].GetComponent<Enemy>().speed;
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
        Debug.Log("Wave: " + wave);
        enemies = new List<GameObject>();
        for (int i = 0; i < wave; i++)
        {
            enemies.Add(Instantiate(enemy));
            enemies[i].GetComponent<Enemy>().hp = Random.Range(1, wave + 1);
            enemies[i].GetComponent<Enemy>().hpMax = enemies[i].GetComponent<Enemy>().hp;
            enemies[i].GetComponent<Enemy>().speed = Random.Range(1f, 1f*wave) * Time.deltaTime;
            enemies[i].GetComponent<Enemy>().isLeft = Random.Range(0, 2) != 0;
            if (enemies[i].GetComponent<Enemy>().isLeft)
            {
                enemies[i].GetComponent<Enemy>().setPosition(new Vector3(-11, -2 + i, 0));
            }
            else
            {
                enemies[i].GetComponent<Enemy>().setPosition(new Vector3(13, -2 + i, 0));
                enemies[i].GetComponent<Enemy>().speed = enemies[i].GetComponent<Enemy>().speed * -1;
            }
        }
    }
}
