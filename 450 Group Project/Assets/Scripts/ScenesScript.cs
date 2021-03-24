using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScenesScript : MonoBehaviour
{
    //get a current Scene
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Manager();
    }

    void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
        currentScene = SceneManager.GetSceneByName(name);
    }

    //set up events that when triggered will call switchScene and load another scene
    void Manager()
    {
        //StartCoroutine("waitForSceneToLoad");//must wait for scene to be completly loaded in order to run manager
        if (currentScene.name == "Title")
        { //set up title events
            //search for button
            Button toMain = GameObject.Find("Button").GetComponent<Button>();
            toMain.onClick.AddListener(delegate { SwitchScene("Main"); });
        }
        if (currentScene.name == "Main")
        {
            //intialize events
            GameManager man = GameObject.Find("Game Manager").GetComponent<GameManager>();

            //back to title screen stuff////////////////////////////////////////////////
            GameObject[] toTitle = GameObject.FindGameObjectsWithTag("BackToTitle");
            foreach (GameObject b in toTitle)
            {
                b.GetComponent<Button>().onClick.AddListener(delegate { SwitchScene("Title"); });
            }

            //gameover event stuff/////////////////////////////////////////////////////////////
            GameObject GameOverUI = GameObject.Find("GameOver");
            GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate {
                man.isDead = false;
                GameOverUI.SetActive(false);
            });
            GameOverUI.SetActive(false);
            //pause event stuff/////////////////////////////////////////////////////////////
            GameObject PauseUI = GameObject.Find("Pause");
            GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(delegate {
                if (man.enemies.Count!=0)
                {
                    man.waveRunning = true;
                }
                man.canMove = true;
                PauseUI.SetActive(false);
            });
            PauseUI.SetActive(false);

            //next wave stuff////////////////////////////////////////////////////////////////
            GameObject NextWaveUI = GameObject.Find("NextWave");
            GameObject.Find("Health").GetComponent<Button>().onClick.AddListener(delegate {
                man.hpMax++;
                man.hp= man.hpMax;
                man.waveRunning = true;
                man.GenerateWave();
                NextWaveUI.SetActive(false);
                man.canMove=true;
            });
            GameObject.Find("PowerButton").GetComponent<Button>().onClick.AddListener(delegate {
                man.power++;
                man.waveRunning = true;
                man.GenerateWave();
                NextWaveUI.SetActive(false);
                man.canMove = true;
            });
            NextWaveUI.SetActive(false);

        }
    }

    IEnumerator waitForSceneToLoad()
    {
        Debug.Log(GameObject.Find("Canvas"));
        while (GameObject.Find("Canvas") == null)
        {
            yield return null;
        }
    }
}