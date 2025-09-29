using System;
using UnityEngine;

//[ExecuteAlways] // So wird Update auch im Editormodus ausgeführt. Vorsicht!
public class ScoreDisplay : MonoBehaviour
{
    public int score = 0; // Punktezhal die gesetzt werden kann. Ingeger: ganze Zahl 0, 1, 2, 3, ...
    public float speed = 1; // Animationsgeschwindigkeit: Punkte pro Sekunde
    private float _score = 0; // Interne Punktzahl, die animiert wird. (Flisskomazahl, die zwischenzeitlich 1.02, 1.5, 1.99, ... sein kann, für die Animation)
    private TMPro.TMP_Text textMesh; // Referenz auf das TextMeshPro-Komponente, die den Text anzeigt

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxDistance = Time.deltaTime * speed; // maximae Distanz in Punkten die zurückgelegt werden kann.    distanz (punkte) = zeit (sekunden) * geschwindigkeit (punkte pro sekunde)
        _score = Mathf.MoveTowards(_score, score, maxDistance); // Punktzahl animieren ohne über das Ziel hinauszuschiessen https://docs.unity3d.com/6000.2/Documentation/ScriptReference/Mathf.MoveTowards.html
        int scoreInt = Mathf.FloorToInt(_score); // Ganze Zahl aus der Flisskomazahl machen (abrunden) z.B. 1.99 -> 1
        textMesh.text = scoreInt.ToString(); // Punktzahl als Text anzeigen
    }

    [ContextMenu("Add 1000 Points")]
    public void Add1000Points()
    {
        score += 1000;
    }
}
