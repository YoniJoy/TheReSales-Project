using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabContents; // All TabContent_Panel references
    public LayoutElement[] layoutElements; // LayoutElement on each content

    private int openIndex = -1;

    public void ToggleTab(int index)
    {
        for (int i = 0; i < tabContents.Length; i++)
        {
            if (i == index)
            {
                StopAllCoroutines();
                StartCoroutine(AnimateHeight(layoutElements[i], 0, 200)); // expand
                openIndex = i;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(AnimateHeight(layoutElements[i], layoutElements[i].preferredHeight, 0)); // collapse
            }
        }
    }

    IEnumerator AnimateHeight(LayoutElement le, float from, float to)
    {
        float duration = 0.3f;
        float time = 0;
        while (time < duration)
        {
            le.preferredHeight = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        le.preferredHeight = to;
    }
}
