//using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TMPro.TMP_Text textMesh;
    public int score = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = score.ToString();
    }
}
