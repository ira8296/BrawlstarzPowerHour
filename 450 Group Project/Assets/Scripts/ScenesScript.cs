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
        if (currentScene.name == "Title") { //set up title events
            //search for button
            Button toMain = GameObject.Find("Button").GetComponent<Button>();
            toMain.onClick.AddListener(delegate { SwitchScene("Main"); });
        }
        if (currentScene.name == "Main") {
            //intialize events
            GameObject PauseUI = GameObject.Find("Pause");
            GameObject GameOverUI = GameObject.Find("GameOver");

            GameObject[] toTitle = GameObject.FindGameObjectsWithTag("BackToTitle");
            foreach (GameObject b in toTitle) {
                b.GetComponent<Button>().onClick.AddListener(delegate { SwitchScene("Title"); });
            }

            //just instantiating some buttons events
            GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(delegate {
                GameOverUI.SetActive(false);
            });
            GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(delegate {
                PauseUI.SetActive(false);
            });
            PauseUI.SetActive(false);
            GameOverUI.SetActive(false);
        }
    }

    IEnumerator waitForSceneToLoad()
    {
        Debug.Log(GameObject.Find("Canvas"));
        while (GameObject.Find("Canvas")==null)
        {
            yield return null;
        }
    }
}
