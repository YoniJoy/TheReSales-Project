using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    // These are your paint cans
    public Material RedPaint;
    public Material BluePaint;
    public Material GreenPaint;

    void Start()
    {
        // Red button uses red paint
        GameObject.Find("RedButton").GetComponent<Button>().onClick.AddListener(() =>
            GetComponentInChildren<MeshRenderer>().material = RedPaint);

        // Blue button uses blue paint
        GameObject.Find("BlueButton").GetComponent<Button>().onClick.AddListener(() =>
            GetComponentInChildren<MeshRenderer>().material = BluePaint);

        // Green button uses green paint
        GameObject.Find("GreenButton").GetComponent<Button>().onClick.AddListener(() =>
            GetComponentInChildren<MeshRenderer>().material = GreenPaint);
    }
}