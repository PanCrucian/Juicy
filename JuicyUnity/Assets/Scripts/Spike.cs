﻿using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour, IMeasured
{

    public Dot dot;
    public bool allowHide = true;
    public float hideTime = 0.75f;

    [HideInInspector]
    public bool hiding;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("Player"))
            Player.Instance.Die();
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
        if (!allowHide)
            return;
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
