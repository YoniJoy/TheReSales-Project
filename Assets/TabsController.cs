using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TabController : MonoBehaviour
{
    // An array of all content panels for the tabs
    public GameObject[] allTabContents;

    // This function is called when a tab button is clicked
    // selectedContent is the tab content that should expand
    public void ToggleTab(GameObject selectedContent)
    {
        // Loop through all tab content panels
        foreach (GameObject tab in allTabContents)
        {
            // If this is the selected tab, expand it
            if (tab == selectedContent)
                StartCoroutine(AnimateTab(tab, true));
            else // Otherwise, collapse it
                StartCoroutine(AnimateTab(tab, false));
        }
    }

    // Coroutine to animate the height of the content panel
    IEnumerator AnimateTab(GameObject content, bool expand)
    {
        float targetHeight = expand ? 200f : 0f; // Final height: 200 if expanding, 0 if collapsing (adjust as needed)
        float duration = 0.3f;                   // How long the animation takes
        float elapsed = 0f;                      // Tracks how much time has passed

        // Get the RectTransform so we can adjust height
        RectTransform rt = content.GetComponent<RectTransform>();
        float startHeight = rt.sizeDelta.y; // Record current height

        // Animate from current height to targetHeight over time
        while (elapsed < duration)
        {
            float newHeight = Mathf.Lerp(startHeight, targetHeight, elapsed / duration); // Smooth transition
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, newHeight); // Apply new height
            elapsed += Time.deltaTime; // Increase elapsed time
            yield return null; // Wait for next frame
        }

        // Ensure final height is set
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, targetHeight);
    }
}
