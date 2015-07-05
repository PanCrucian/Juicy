using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotatingPoopSpike : RotatingSpike {

    public Spike poopSpike;
    /// <summary>
    /// Интервал спавна спайков
    /// </summary>
    public float poopInterval = 0.35f;
    /// <summary>
    /// Время через которое будем вращаться в другую сторону
    /// </summary>
    public float directionTime = 1f;

    private List<Spike> poops = new List<Spike>();
    private float lastDirectionTime;
    private float lastPoopTime;

    public override void Start()
    {
        base.Start();
        Game.Instance.OnGamePlay += OnGamePlay;
    }

    void OnGamePlay()
    {
        lastDirectionTime = Time.time;
        lastPoopTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(lastDirectionTime - Time.time) >= directionTime)
        {
            ChangeDirection();
            lastDirectionTime = Time.time;
        }
        if (Mathf.Abs(lastPoopTime - Time.time) >= poopInterval)
            Poop();
    }

    void Poop()
    {
        Spike newPoop = (Spike)Instantiate(poopSpike);
        newPoop.transform.position = transform.position;
        newPoop.transform.parent = dot.transform;
        newPoop.dot = dot;
        poops.Add(newPoop);
        lastPoopTime = Time.time;
    }

    void ChangeDirection()
    {
        rotateSpeed *= -1f;
        while (poops.Count > 0)
        {
            Spike poop = poops[0];
            poops.Remove(poop);
            Destroy(poop.gameObject);
        }
    }
}
