using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    public Animator animator;

    public Button optionA;
    public Button optionB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Add listener");
        optionA.onClick.AddListener(OnOptionAClick);
        optionB.onClick.AddListener(OnOptionBClick);
    }
    void OnOptionAClick()
    {
        Debug.Log("A");
        animator.SetTrigger("SaySomething"); // must be the same name as in Animator Parameters!
    }
    void OnOptionBClick()
    {
        Debug.Log("B");
        animator.SetTrigger("SaySomething"); // must be the same name as in Animator Parameters!
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
