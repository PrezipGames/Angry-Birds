using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Library hinzufügen, um mit dem SceneManager arbeiten zu können
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Variablen deklarieren
    public GameObject menuPanel;
    public GameObject levelsPanel;

    public LevelButton[] levelButtons;

    public GameObject[] lockButtons;

    private void Start()
    {
        // Befehl um alle PlayerPrefs zu löschen
        //PlayerPrefs.DeleteAll();

        // Sterne der levelButtons gelb färben
        SetMenuStars();

        // LockButtons deaktivieren
        DeactivateLockButtons();
    }

    // Funktion, die die LockButtons deaktiviert
    private void DeactivateLockButtons()
    {
        // Anzahl der absolvierten Level wird aus PlayerPrefs geholt und in levelsSolved gespeichert
        int levelsSolved = PlayerPrefs.GetInt("levelsSolved", 0);

        // mit einer for Schleife werden alle LockButton bis zum Wert von levelsSolved - 1 deaktiviert
        for(int i = 0; i < levelsSolved; i++)
        {
            if(i < lockButtons.Length)
            {
                lockButtons[i].SetActive(false);
            }
        }
    }

    // Funktion, die die Sterne im levelsPanel gelb färbt
    private void SetMenuStars()
    {
        // die for-Schleife geht alle levelButton durch, holt sich die Anzahl der gespeicherten Sterne
        // aus den PlayerPrefs und speichert sie im integer levelStars ab.
        // Mit diesem Wert levelStars wird in das LevelButton Skript des jeweiligen Buttons gesprungen
        // und die SetStars Funktion aufgerufen
        for(int i = 0; i < levelButtons.Length; i++)
        {
            int levelStars = PlayerPrefs.GetInt("level" + (i + 1) + "stars", 0);
            levelButtons[i].SetStars(levelStars);
        }
    }

    // Funktion, die einem beim Klick auf den Play Button das menuPanel deaktiviert
    // und das levelsPanel aktiviert
    public void MenuToLevel()
    {
        menuPanel.gameObject.SetActive(false);
        levelsPanel.gameObject.SetActive(true);
    }

    // Funktion, die einem beim Klick auf den jeweiligen LevelButton das Level lädt
    public void LoadLevels(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
