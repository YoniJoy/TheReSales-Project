using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // Drag your yellow, orange, and beige materials into these slots in the Inspector
    public Material yellowMaterial;
    public Material orangeMaterial;
    public Material beigeMaterial;

    // Reference to the chair GameObject
    private GameObject targetChair;

    void Start()
    {
        // Find the chair by name (make sure the GameObject in the scene is named exactly "Chair")
        targetChair = GameObject.Find("Chair");

        if (targetChair == null)
        {
            Debug.LogError("No GameObject named 'Chair' found!");
        }
    }

    // These functions can be linked to UI buttons
    public void SetYellow()
    {
        SetMaterial(yellowMaterial);
    }

    public void SetOrange()
    {
        SetMaterial(orangeMaterial);
    }

    public void SetBeige()
    {
        SetMaterial(beigeMaterial);
    }

    // Applies the selected material to the chair
    private void SetMaterial(Material mat)
    {
        if (targetChair != null && mat != null)
        {
            MeshRenderer renderer = targetChair.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = mat;
            }
            else
            {
                Debug.LogError("No MeshRenderer found on the Chair GameObject!");
            }
        }
    }
}