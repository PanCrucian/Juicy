using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {

    public Dot dot;
    public float hideTime = 0.75f;
    public int addCoins = 1;

    [HideInInspector]
    public bool hiding;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("Player"))
        {
            Game.Instance.IncCoins(addCoins);
            Hide();
        }
    }

    public virtual void Update()
    {
        if (dot != null)
            if (!dot.gameObject.activeSelf)
                if (!hiding)
                    Hide();
    }

    /// <summary>
    /// Спрячем
    /// </summary>
    public void Hide()
    {
        hiding = true;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(HideNumerator());
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
}
