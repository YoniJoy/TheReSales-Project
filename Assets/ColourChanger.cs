using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // Drag the materials in Inspector
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;

    // Reference to the SINGLE cube I want to modify
    private GameObject targetCube;

    void Start()
    {
        // Finds the cube automatically
        targetCube = GameObject.Find("Chair"); // Change "Chair" to your cube's exact name

        // Safety check
        if (targetCube == null)
        {
            Debug.LogError("No cube named 'Chair' found!");
        }
    }

    // Button functions
    public void SetRed() { SetMaterial(redMaterial); }
    public void SetBlue() { SetMaterial(blueMaterial); }
    public void SetGreen() { SetMaterial(greenMaterial); }

    void SetMaterial(Material mat)
    {
        if (targetCube != null && mat != null)
        {
            targetCube.GetComponent<MeshRenderer>().material = mat;
        }
    }
}