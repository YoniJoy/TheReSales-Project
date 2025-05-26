using UnityEngine;
using UnityEngine.UI;

public class ColorButtonManager : MonoBehaviour
{
    public GameObject[] buttons; // Assign your buttons in Inspector

    private void Start()
    {
        DeselectAll();
    }

    public void SelectColor(int index)
    {
        if (index < 0 || index >= buttons.Length)
            return;

        DeselectAll();

        Transform border = buttons[index].transform.Find("Border");
        if (border != null)
        {
            border.gameObject.SetActive(true);
        }
    }

    private void DeselectAll()
    {
        foreach (GameObject button in buttons)
        {
            Transform border = button.transform.Find("Border");
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }
        }
    }
}
