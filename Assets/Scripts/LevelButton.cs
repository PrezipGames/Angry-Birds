using System.Collections;
using System.Collections.Generic;
// Librarys hinzuf�gen, um mit UI Elementen und dem SceneManager arbeiten zu k�nnen
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Variablen deklarieren
    public Image[] starsImages;
    public Sprite yellowStar;

    // Funktion, die die Sterne an dem jeweiligen Button gelb f�rbt
    public void SetStars(int stars)
    {
        // mit einer for Schleife werden alle Sterne bis zum Wert von stars - 1 gelb gef�rbt
        for (int i=0; i< stars; i++)
        {
            starsImages[i].sprite = yellowStar;
        }
    }
}
