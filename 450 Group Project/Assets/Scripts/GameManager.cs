using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int wave;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int hp;//Current hp
    public int hpMax;//Max hp
    public int power;//Attack power (how much damage done per click)
    public int points;//Points for upgrades
    public int hpPoints;//Points for upgrading hp
    public bool isDead;
    public bool waveRunning;
    public bool canMove;
    public bool invincible;
    public GameObject player;
    public GameObject particles;
    private GameObject particleTemp;
    public GameObject projectile;

    public GameObject resetDialogue;

    //Add in things for increasing health


    //UI on pause and and on gameover
    private GameObject PauseUI;
    private GameObject GameOverUI;
    private GameObject NextWaveUI;
    public GameObject StartWaveUI;
    // Start is called before the first frame update
    void Start()
    {
        hp = 2;
        hpMax = 2;
        power = 1;
        isDead = false;
        canMove = true;
        wave = 0;
        resetDialogue.SetActive(false);
        PauseUI = GameObject.Find("Pause");
        GameOverUI = GameObject.Find("GameOver");
        NextWaveUI = GameObject.Find("NextWave");
        StartWaveUI = GameObject.Find("Start");
    }

    public void initUI() 
    {
        PauseUI.SetActive(false);
        GameOverUI.SetActive(false);
        NextWaveUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !waveRunning && !PauseUI.activeSelf)
        {
            waveRunning = true;
            GenerateWave();
            return;
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {//pause
            waveRunning = false;
            canMove = false;
            PauseUI.SetActive(true);
            return;
        }
        if (isDead)
        {///reset
            foreach (GameObject a in enemies)
            {
                Destroy(a);
            }
            wave = 0;
            waveRunning = false;

            //button events are already set up in Scenes Script merely requires GamOverUI activation
            GameOverUI.SetActive(true);
            return;
        }
        else if (waveRunning)
        {//during wave
            enemies[0].GetComponent<Enemy>().isMoving = true;
            enemies[0].GetComponent<Rigidbody2D>().simulated = true;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetComponent<Enemy>().isDead)
                {
                    if (enemies.Count == 1)
                    {
                        //Where Wave Ends
                        waveRunning = false;
                        FindObjectOfType<AudioManager>().Play("WaveFinish");
                        //hp++;
                    }
                    Destroy(particleTemp);
                    particleTemp = Instantiate(particles, enemies[i].transform.position, Quaternion.identity);
                    Destroy(enemies[i]);
                    enemies.RemoveAt(i);
                    continue;
                }

                if (enemies[i].GetComponent<Enemy>().isMoving)
                {
                    /*Vector2 temp = enemies[i].transform.position;
                    temp.x += enemies[i].GetComponent<Enemy>().speed;
                    enemies[i].transform.position = temp;*/
                    enemies[i].GetComponent<Enemy>().Move();
                }
            }
        }
        else {//no wave running
            canMove = false;
            if (wave == 0) StartWaveUI.SetActive(true);
            else NextWaveUI.SetActive(true);
        }
    }

    public void TakeDamage()
    {
        if (!invincible)
        {
            StartCoroutine(Invincible());
            hp--;
            if (hp <= 0)
            {
                isDead = true;
                Debug.Log("Game Over");

                FindObjectOfType<AudioManager>().Play("GameOver");

                GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");
                foreach(GameObject s in shields)
                {
                    Destroy(s);
                }
                //reset dialogue
                //resetDialogue.SetActive(true);
            }
        }
    }

    public IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(.5f);
        invincible = false;
    }

    public void GenerateWave()
    {
        wave++;

        //change background
        if (wave%2==0){
            int newBackgroundInteger = Random.Range(0, 2);
            string imgName = "island";//default
            switch(newBackgroundInteger)
            {
                case 0://will stay at default
                    break;
                case 1:
                    imgName = "docks";
                    break;
                case 2:
                    imgName = "plains";
                    break;
            }
            GameObject.Find("background").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(imgName);//will only work if imgs are in resources folder
        }

        Debug.Log("Wave: " + wave);
        enemies = new List<GameObject>();
        for (int i = 0; i < Random.Range(wave, wave + 3); i++)
        {
            enemies.Add(Instantiate(enemy));
            enemies[i].GetComponent<Enemy>().hp = Random.Range(wave, wave + 1);
            enemies[i].GetComponent<Enemy>().hpMax = enemies[i].GetComponent<Enemy>().hp;
            enemies[i].GetComponent<Enemy>().setSpeed(wave);
            enemies[i].GetComponent<Enemy>().isLeft = Random.Range(0, 2) != 0;
            enemies[i].GetComponent<Enemy>().type = (EnemyType)Random.Range(0, 3);
            if (enemies[i].GetComponent<Enemy>().type == EnemyType.Air)
            {
                enemies[i].GetComponent<Enemy>().setPosition(new Vector3(-5 + i, 10, 0));
            }
            else
            {
                if (enemies[i].GetComponent<Enemy>().isLeft)
                {
                    enemies[i].GetComponent<Enemy>().setPosition(new Vector3(-11, -2 + i, 0));
                }
                else
                {
                    enemies[i].GetComponent<Enemy>().setPosition(new Vector3(13, -2 + i, 0));
                }
            }
        }
    }
}