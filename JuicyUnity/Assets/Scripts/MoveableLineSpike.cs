using UnityEngine;
using System.Collections;

public class MoveableLineSpike : Spike {

    public float moveSpeed = 17.5f;
    public float moveDistance = 2.5f;

    private float offset;
    private bool invert;

    public override void Start()
    {
        base.Start();
        transform.position = dot.transform.position;
        transform.rotation = dot.transform.rotation;
        transform.parent = dot.transform;
    }

    public override void Update()
    {
        base.Update();
        
        if (hiding)
            return;

        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (Player.Instance.healthState == Player.HealthStates.Death)
            return;

        if (invert)
            offset += moveSpeed * Time.deltaTime;
        else
            offset -= moveSpeed * Time.deltaTime;
        
        if (Mathf.Abs(offset) > moveDistance)
        {
            offset = invert ? moveDistance : moveDistance * -1f;
            invert = !invert;
        }
        transform.localPosition = new Vector3(
            offset,
            transform.localPosition.y,
            transform.localPosition.z
            );
    }
}
