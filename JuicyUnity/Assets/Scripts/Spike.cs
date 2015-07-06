using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour, IMeasured
{

    public Dot dot;
    public bool allowHide = true;
    public float hideTime = 0.75f;
    public bool hideOnDotEnd;

    [HideInInspector]
    public bool hiding;

    public virtual void Start()
    {
        if (dot == null)
            return;
        dot.OnHided += OnDotHided;
        dot.OnStartHide += OnDotStartHide;
    }

    public virtual void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("Player"))
            Player.Instance.Die();
    }

    public virtual void Update()
    {
        if (dot != null)
            if (!dot.gameObject.activeSelf)
                if (!hiding && hideOnDotEnd)
                    Hide();
    }

    /// <summary>
    /// Спрячем
    /// </summary>
    public void Hide()
    {
        try
        {
            if (!allowHide)
                return;
            hiding = true;
            if (GetComponent<Collider>() != null)
                GetComponent<Collider>().enabled = false;
            StartCoroutine(HideNumerator());
        }
        catch
        {
            Debug.LogWarning("Объект уже уничтожен");
        }
    }

    IEnumerator HideNumerator()
    {
        Vector3 currentScale = transform.localScale;
        for (float t = 0; t <= hideTime; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, t / hideTime);
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Когда точка исчезла
    /// </summary>
    public virtual void OnDotHided()
    {
        
    }

    /// <summary>
    /// Когда точка начинает исчезать
    /// </summary>
    public virtual void OnDotStartHide()
    {
        if (!hideOnDotEnd)
            Hide();
    }
}
