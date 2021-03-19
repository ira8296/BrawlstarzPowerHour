using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameManager man;
    public Text wave;
    public Text power;
    public Text enemies;
    public Slider hp;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        wave.text = "Wave: " + man.wave;
        power.text = "Power: " + man.power;
        enemies.text = "Enemies: " + man.enemies.Count;
        hp.maxValue = man.hpMax;
        hp.value = man.hp;
    }
}
