using UnityEngine;
using System.Collections;

public class GetoutSpike : Spike {

    public SpikeInsideTrigger spike;
    public float growSpeed = 3.5f;
    public float maxGrow = 2f;

    public override void Start()
    {
        spike.dot = dot;
        ResetScale();
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (Player.Instance.healthState == Player.HealthStates.Death)
            return;
        Grow();
        if (transform.localScale.y >= maxGrow)
            ChangeAngle();
    }

    void Grow()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y + (growSpeed * Time.deltaTime),
            transform.localScale.z
            );
    }

    void ChangeAngle()
    {
        ResetScale();
        transform.localRotation = Quaternion.Euler(
            transform.localRotation.eulerAngles.x,
            transform.localRotation.eulerAngles.y,
            transform.localRotation.eulerAngles.z - 90f
            );
    }

    void ResetScale()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            0,
            transform.localScale.z
            );
    }
}
