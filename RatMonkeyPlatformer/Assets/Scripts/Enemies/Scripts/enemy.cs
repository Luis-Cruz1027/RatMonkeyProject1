using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    public scriptableEnemies Enemy;
    private int health;
    private int maxHealth;
    void Start(){
        health = maxHealth = Enemy.maxHealth;
        Debug.Log(health);
    }

    void Update(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    public void DealDamage(int damage){
        health = health - damage;
        //Debug.Log(health);
    }

    

    
}
