using UnityEngine;
using UnityEngine.UI;

public class AccordionTab : MonoBehaviour
{
    public GameObject contentPanel;
    public Button tabButton;
    public AccordionController controller; 
    public bool shown = false;

    void Start()
    {
        tabButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        controller.ToggleTab(this.gameObject, shown); // calling the accordion controller for dropdown tabs
    }

    public void Show() => contentPanel.SetActive(true);
    public void Hide() => contentPanel.SetActive(false);
}
