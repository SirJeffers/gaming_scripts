using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour {

    private Scene main;
    public Hearts hearts;

    public int maxHP { get; set; }

    public int currentHP;
    public int getCurrentHP()
    {
        return currentHP;
    }
    public void setCurrentHP(int currentHP)
    {
        this.currentHP = currentHP;
        if (currentHP >= 5)
            SceneManager.LoadScene(main.name);
        if (currentHP < 5)
        hearts.damage(currentHP);
    }


    private int damage { get; set; }

    private bool alive { get; set; }



    // Use this for initialization
    void Start () {
        maxHP = 5;
        currentHP = 0;
        damage = 20;
        alive = true;
        main = SceneManager.GetActiveScene();
	}
	




}
