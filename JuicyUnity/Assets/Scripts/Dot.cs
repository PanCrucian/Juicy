using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour, IMeasured {
    public delegate void OnHidedDgt();
    public OnHidedDgt OnHided;
    public OnHidedDgt OnStartHide;
    public float hideTime = 0.75f;
    public float heatTime = 5f;
    public GameObject graphics;
    [HideInInspector]
    public bool isHiding;

    public void StartHeat()
    {
        StartCoroutine(HeatNumerator());
    }

    IEnumerator HeatNumerator()
    {
        Material mat = graphics.GetComponent<MeshRenderer>().material;
        Color c = mat.color;
        for(float t = 0; t <= heatTime; t +=Time.deltaTime) {
            if (isHiding)
                break;
            if (Game.Instance.state != Game.GameStates.Game)
                t = 0f;
            mat.color = new Color(
                Mathf.Lerp(c.r, Color.red.r, t/heatTime),
                Mathf.Lerp(c.g, Color.red.g, t/heatTime),
                Mathf.Lerp(c.b, Color.red.b, t/heatTime)
                );
            yield return new WaitForEndOfFrame();
        }
        if (!isHiding)
            Player.Instance.Die();
        Hide();
    }

    /// <summary>
    /// Спрячем точку
    /// </summary>
    public void Hide()
    {
        isHiding = true;
        graphics.GetComponent<Collider>().enabled = false;
        StartCoroutine(HideNumerator());
    }

    IEnumerator HideNumerator()
    {
        if (OnStartHide != null)
            OnStartHide();
        Vector3 currentScale = transform.localScale;
        for (float t = 0; t <= hideTime; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, t / hideTime);
            yield return new WaitForEndOfFrame();
        }
        if (OnHided != null)
            OnHided();
        gameObject.SetActive(false);
    }
}
