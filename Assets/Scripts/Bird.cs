using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Variablen deklarieren
    public Rigidbody2D playerRb;
    public Rigidbody2D hookRb;
    public bool isDragging;
    private float maxDragDistance = 2.2f;

    public SpriteRenderer sr;
    public Sprite[] sprites;

    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform defaultPosition;

    public GameObject nextBirdPrefab;
    public bool lastBird;

    void Start()
    {
        // Anzahl der Eckpunkte der beiden LineRenderer festlegen
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;

        // Eckpunkte 0 der beiden LineRenderer an der Schleuder anbringen (stripPosition0 & stripPosition 1)
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        // An den Eckpunkten 1 werden die LineRenderer an der defaultPosition positioniert
        SetStrips(defaultPosition.position);
    }

    // positioniert die Eckpunkte 1 der LinerRenderer an der Position der Maus
    private void SetStrips(Vector2 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
    }

    void Update()
    {
        // Wenn mit der Maus an dem Vogel gezogen wird...
        if(isDragging == true)
        {
            // Position der Maus in mousePos speichern
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Wenn die Distanz zwischen Maus und Hook größer ist als die maxDragDistance
            if(Vector3.Distance(mousePos, hookRb.position) > maxDragDistance)
            {
                // Vogel kann nicht weiter als maxDragDistance von der Schleuder entfernt werden
                playerRb.position = hookRb.position + (mousePos - hookRb.position).normalized * maxDragDistance;

                // Die Eckpunkte 1 der lineRenderer werden hinter dem Vogel positioniert
                SetStrips(hookRb.position + (mousePos - hookRb.position).normalized * 2.6f);
            }
            else
            {
                // positioniert Vogel an der Maus, wenn die Maus innerhalb der maxDragDistance liegt
                playerRb.position = mousePos;

                // Die Eckpunkte 1 der lineRenderer werden hinter dem Vogel positioniert
                SetStrips(mousePos + (mousePos - hookRb.position).normalized * 0.4f);
            }
            
        }
        else
        {
            // wenn nicht am Vogel gezogen wird, werden die Eckpunkte 1 der LinerRenderer
            // an der defaultPosition positioniert
            SetStrips(defaultPosition.position);
        }
    }

    // isDragging auf true setzen wenn am Vogel gezogen wird und Rigidbody auf kinematic setzen,
    // damit er nicht zum Hook gezogen wird
    private void OnMouseDown()
    {
        isDragging = true;
        playerRb.isKinematic = true;
    }

    // isDragging auf false setzen wenn der Vogel losgelassen wurde,
    // den Rigidbody wieder auf Dynamic setzen und die "StartFlying" Coroutine starten
    private void OnMouseUp()
    {
        isDragging = false;
        playerRb.isKinematic = false;
        StartCoroutine("StartFlying");
    }

    private IEnumerator StartFlying()
    {
        // 0.2 Sekunden warten, damit der Vogel in Richtung der Schleuder gezogen wird
        yield return new WaitForSeconds(0.2f);

        // SpringJoint2D Komponente deaktivieren, damit der Vogel nicht mehr mit dem Hook verbunden ist
        GetComponent<SpringJoint2D>().enabled = false;

        // der Vogel kann sich wieder rotieren
        playerRb.constraints = RigidbodyConstraints2D.None;

        // das Sprite des Vogels wird verändert
        sr.sprite = sprites[1];

        // Das Skript wird deaktiviert, damit man den Vogel nicht mehr aufheben kann,
        // wenn man ihn bereits weggeworfen hat
        this.enabled = false;

        // 1.5 Sekunden warten
        yield return new WaitForSeconds(1.5f);

        // Wenn ein nextBirdPrefab übergeben wurde, wird dieses aktiviert
        if(nextBirdPrefab != null)
        {
            nextBirdPrefab.SetActive(true);
        }
        // Wenn keins übergeben wurde, wird lastBird auf true gesetzt
        else
        {
            lastBird = true;
        }

        // "DeleteBirds" Coroutine wird gestartet
        StartCoroutine("DeleteBirds");
    }

    private IEnumerator DeleteBirds()
    {
        // 3 Sekunden warten bevor der Vogel aus dem Spiel gelöscht wird
        yield return new WaitForSeconds(3);
        Destroy(gameObject);

        // Wenn es der letzte Vogel war, wird das Level beendet (GameOver Funktion aufgerufen)
        if(lastBird == true)
        {
            GameManager gm = FindObjectOfType<GameManager>();

            if(gm.gameIsOver == false)
            {
                gm.GameOver();
            }
        }
    }

    // Wenn der Vogel mit irgendetwas kollidiert, wird das Sprite geändert
    private void OnCollisionEnter2D(Collision2D collision)
    {
        sr.sprite = sprites[2];
    }
}
