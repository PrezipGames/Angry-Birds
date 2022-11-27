using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Librarys hinzufügen, um mit UI Elementen und dem SceneManager arbeiten zu können
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variablen deklarieren
    public GameObject gameOverPanel;
    public Image[] stars;
    public Sprite yellowStar;
    public bool gameIsOver;

    // Funktion, die speichert wie viele Sterne man in einem Level erreicht hat
    private void LevelStarsCount(int levelIndex, int stars)
    {
        // Wenn die Anzahl der erreichten Sterne größer ist als der Wert, der vorher abgespeichert war,
        // wird der neue Wert in dem key "level + levelIndex + "stars" abgespeichert,
        // wobei levelIndex 0,1,2,3,... sein kann z.B (level1Index, level2Index...)
        if(PlayerPrefs.GetInt("level" + levelIndex + "stars", 0) < stars)
        {
            PlayerPrefs.SetInt("level" + levelIndex + "stars", stars);
        }
    }
    
    public void GameOver()
    {
        // Beendet ein Level und aktiviert gameOverPanel
        gameIsOver = true;
        gameOverPanel.gameObject.SetActive(true);

        // index des levels in currentLevel speichern
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Wenn ein drittel der Gegner getötet wurden, wird der erste Stern gelb
        if(Enemy.enemiesAlive <= Enemy.maxEnemies * 2 / 3)
        {
            stars[0].sprite = yellowStar;

            // einen erreichten Stern abspeichern
            LevelStarsCount(currentLevel, 1);
        }

        // Wenn zwei drittel der Gegner getötet wurden, wird der zweite Stern gelb
        if (Enemy.enemiesAlive <= Enemy.maxEnemies * 0.5f)
        {
            stars[1].sprite = yellowStar;

            // zwei erreichte Sterne abspeichern
            LevelStarsCount(currentLevel, 2);
        }

        // Wenn alle Gegner getötet wurden, wird der dritte Stern gelb
        if (Enemy.enemiesAlive == 0)
        {
            stars[2].sprite = yellowStar;

            // drei erreichte Sterne abspeichern
            LevelStarsCount(currentLevel, 3);

            // wenn drei Sterne erreicht wurden, wird unter dem key "levelsSolved" der Index des Levels abgespeichert.
            // Diesem integer brauchen wir um zu gucken, ob das nächste Level freigeschalten werden kann
            if(currentLevel > PlayerPrefs.GetInt("levelsSolved", 0))
            {
                PlayerPrefs.SetInt("levelsSolved", currentLevel);
            }
            
        }
    }

    void Update()
    {
        // Wenn alle Gegner getötet wurde, wird das Spiel beendet
        if(Enemy.enemiesAlive == 0 && gameIsOver == false)
        {
            GameOver();
        }
    }

    // Funktion, die das Level bei Klick auf den Button neu startet und
    // enemiesAlive und maxEnemies wieder auf 0 setzt
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Enemy.enemiesAlive = 0;
        Enemy.maxEnemies = 0;
    }

    // Funktion die bei Klick auf den Button die Menu Szene lädt und enemiesAlive
    // und maxEnemies wieder auf 0 setzt
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Enemy.enemiesAlive = 0;
        Enemy.maxEnemies = 0;
    }
}
