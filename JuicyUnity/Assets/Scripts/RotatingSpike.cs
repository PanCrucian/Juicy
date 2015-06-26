using UnityEngine;
using System.Collections;

public class RotatingSpike : Spike {

    public float rotateSpeed = 90f;
    public float selfRotateSpeed = 90f;
    public bool hideOnDotEnd;
    LineRenderer lRenderer;

    void Start()
    {
        lRenderer = GetComponent<LineRenderer>();
        dot.OnHided += OnDotHided;
        dot.OnStartHide += OnDotStartHided;
    }

    public override void Update()
    {        
        if (Game.Instance.state != Game.GameStates.Game || Player.Instance.healthState == Player.HealthStates.Death)
            return;
        if (!hiding)
        {
            transform.RotateAround(dot.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.forward, selfRotateSpeed * Time.deltaTime);
        }
        lRenderer.SetPosition(0, transform.position);
        lRenderer.SetPosition(1, dot.transform.position);
    }

    void OnDotHided()
    {
        lRenderer.enabled = false;
    }

    void OnDotStartHided()
    {
        Hide();
    }
}
