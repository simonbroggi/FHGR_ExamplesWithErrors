using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowMenuTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<Collider> collidersInside = new List<Collider>();
    private Color defaultColor;
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger entered by " + other.gameObject.name);
        GetComponent<Renderer>().material.color = Color.red;
        collidersInside.Add(other);
        Menue menue = other.GetComponent<Menue>();
        if (menue != null)
        {
            menue.ShowIt();
        }
    }
    void OnTriggerExit(Collider other)
    {
        collidersInside.Remove(other);
        if (collidersInside.Count == 0)
        {
            GetComponent<Renderer>().material.color = defaultColor;
        }
    }
}
