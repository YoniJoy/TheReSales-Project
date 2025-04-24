using UnityEngine;

public class SimpleTabs : MonoBehaviour
{
    public GameObject[] tabs;  // Assign in Inspector
    public GameObject[] contents; // Assign in Inspector

    void Start()
    {
        ShowTab(0); // Show first tab by default
    }

    public void ShowTab(int tabNumber)
    {
        // Hide all contents
        foreach (GameObject content in contents)
        {
            content.SetActive(false);
        }

        // Show selected content
        if (tabNumber < contents.Length)
        {
            contents[tabNumber].SetActive(true);
        }
    }
}