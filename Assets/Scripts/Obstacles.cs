using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Variablen deklarieren
    public float obstacleHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Wenn relative Velocity kleiner ist als obstacleHealth,
        // wird relative Velocity von obstacleHealth abgezogen
        if(collision.relativeVelocity.magnitude < obstacleHealth)
        {
            obstacleHealth -= collision.relativeVelocity.magnitude;
        }
        // Wenn relative Velocity größer oder gleich obstacleHealth ist,
        // wird das Objekt aus dem Spiel gelöscht
        else
        {
            Destroy(gameObject);
        }
    }
}
