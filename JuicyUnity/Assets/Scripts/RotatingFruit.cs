using UnityEngine;
using System.Collections;

public class RotatingFruit : Fruit {

    public float rotateSpeed = 90f;
    public float selfRotateSpeed = 90f;

    public override void Update()
    {
        base.Update();
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (!hiding)
        {
            transform.RotateAround(dot.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.forward, selfRotateSpeed * Time.deltaTime);
        }
    }
}
