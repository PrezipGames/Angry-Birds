using System.Collections;
using System.Collections.Generic;
// Librarys hinzufügen, um mit UI Elementen und dem SceneManager arbeiten zu können
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    // Variablen deklarieren
    public Image[] starsImages;
    public Sprite yellowStar;

    // Funktion, die die Sterne an dem jeweiligen Button gelb färbt
    public void SetStars(int stars)
    {
        // mit einer for Schleife werden alle Sterne bis zum Wert von stars - 1 gelb gefärbt
        for (int i=0; i< stars; i++)
        {
            starsImages[i].sprite = yellowStar;
        }
    }
}
