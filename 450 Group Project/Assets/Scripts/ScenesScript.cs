using System.Collections;
using System.Collections.Generic;
using System;
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
        if (currentScene.name == "Title")
        { //set up title events
            //search for button
            Button toMain = GameObject.Find("Button").GetComponent<Button>();
            toMain.onClick.AddListener(delegate { SwitchScene("Main"); });
        }
        if (currentScene.name == "Main")
        {
            //get manager
            GameManager man = GameObject.Find("Game Manager").GetComponent<GameManager>();




            //back to title screen stuff////////////////////////////////////////////////
            GameObject[] toTitle = GameObject.FindGameObjectsWithTag("BackToTitle");
            foreach (GameObject b in toTitle)
            {
                b.GetComponent<Button>().onClick.AddListener(delegate { SwitchScene("Title"); });
            }
            // UI button hp hpMax power isDead waveRunning canMove generateWave setActive
            //EventMaker calls////////////////////////////////////////////////////////////////////////////
            //gameover event stuff/////////////////////////////////////////////////////////////
            MainButtonEventMaker("GameOver", "Restart", null, null, null, false, null, null, null, false);
            //pause event stuff/////////////////////////////////////////////////////////////
            MainButtonEventMaker("Pause", "Play", null, null, null, null, null, true, null, false);
            //start event stuff///////////////////////////////////////////////////////////////
            MainButtonEventMaker("Start", "StartButton",2,2,1, null, true,true,true,false);
            //next wave stuff////////////////////////////////////////////////////////////////
            MainButtonEventMaker("NextWave", "Health", man.hpMax+1, man.hpMax+1, null, null, true, true, true, false);
            MainButtonEventMaker("NextWave", "PowerButton", null, null, man.power+1, null, true, true, true, false);
            man.initUI();
        }
    }
    //int? is a nullable int ? makes it nullable makes it easier to read what is and isnt added to handler
    void MainButtonEventMaker(string ui,string button,int? hp,int? hpMax,int? power, bool? isDead, bool? waveRunning, bool? canMove,bool? generateWave, bool? setActive) 
    {
        GameObject UI = GameObject.Find(ui);
        GameManager man = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //want to fill event with only stuff pertinant to it
        //generate functions to add to event
        List<Action> functions = new List<Action>();//essentially holds delegates ()=> is a shorthand
        if (hp != null) { functions.Add(() => man.hp = (int)hp); }
        if (hpMax != null) { functions.Add(() =>man.hpMax = (int)hpMax); }
        if (power != null) { functions.Add(() =>man.power = (int)power); }
        if (isDead != null) { functions.Add(() => man.isDead = (bool)isDead); }
        if (waveRunning != null) { functions.Add(() =>man.waveRunning = (bool)waveRunning); }
        if (canMove != null) { functions.Add(() =>man.canMove = (bool)canMove); }
        if (generateWave != null) { functions.Add(() => man.GenerateWave()); }
        if (setActive != null) { functions.Add(()=>UI.SetActive((bool)setActive));}
        if (button=="Play") {//unique to play as of yet
            functions.Add(() =>{
                if (man.enemies.Count != 0){
                    man.waveRunning = true;
                }
            });
        }
        GameObject.Find(button).GetComponent<Button>().onClick.AddListener(delegate {
            foreach(Action function in functions) 
            {
                function();
            }
        });
    }   
}