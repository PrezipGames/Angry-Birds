using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variablen deklarieren
    public int healthPoints;

    // statische Integer, damit der Wert der Variable spiel�bergreifend gleich ist
    public static int enemiesAlive;
    public static int maxEnemies;

    public bool enemyDied = false;
    
    void Start()
    {
        // F�r jede Instanz dieses Skripts wird +1 dazu gerechnet.
        // Man bekommt also die Anzahl der Gegner wenn das Spiel startet
        enemiesAlive++;
        maxEnemies++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Gegner aus dem Spiel l�schen, wenn relative Velocity gr��er war als healthPoints
        if(collision.relativeVelocity.magnitude > healthPoints && enemyDied == false)
        {
            Destroy(gameObject);
            // eins von enemiesAlive abziehen
            enemiesAlive--;
            enemyDied = true;
        }
    }
}
