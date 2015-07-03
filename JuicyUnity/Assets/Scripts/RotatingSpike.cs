using UnityEngine;
using System.Collections;

public class RotatingSpike : Spike {

    public float rotateSpeed = 90f;
    public float selfRotateSpeed = 90f;
    LineRenderer lRenderer;

    public override void Start()
    {
        base.Start();
        lRenderer = GetComponent<LineRenderer>();
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

    public override void OnDotHided()
    {
        lRenderer.enabled = false;
    }
}
