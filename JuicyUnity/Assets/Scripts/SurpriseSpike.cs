using UnityEngine;
using System.Collections;

public class SurpriseSpike : Spike {
    public SurpriseDot surpriseDot;
    public Spike surprise;
    public float moveSpeed = 3.5f;

    private float sDotToSupDir;
    private bool allowSurpise = false;

    public override void Start()
    {
        surprise.dot = dot;
        base.Start();
        SurpriseAlignToSDot();
        sDotToSupDir = AngleDir(
            transform.forward,
            surpriseDot.transform.position - dot.transform.position,
            transform.up);
    }

    public override void Update()
    {
        base.Update();
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (Player.Instance.healthState == Player.HealthStates.Death)
            return;
        if (allowSurpise)
        {
            surprise.transform.localPosition = new Vector3(
                surprise.transform.localPosition.x + (moveSpeed * Time.deltaTime * (sDotToSupDir < 0 ? 1f : -1f)),
                surprise.transform.localPosition.y,
                surprise.transform.localPosition.z
                );
        }
    }

    /// <summary>
    /// Выпустить зверя на свободу ;p
    /// </summary>
    public void SurpriseMotherFucker()
    {
        allowSurpise = true;
        Renderer sDotRen = surpriseDot.GetComponent<Renderer>();
        sDotRen.material.color = new Color(
            sDotRen.material.color.r, sDotRen.material.color.g, sDotRen.material.color.b,
            1f
            );
    }

    /// <summary>
    /// Слева или справа от объекта
    /// </summary>
    /// <param name="fwd"></param>
    /// <param name="targetDir"></param>
    /// <param name="up"></param>
    /// <returns></returns>
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    /// <summary>
    /// Выравниваем Y у палки к точке события
    /// </summary>
    void SurpriseAlignToSDot()
    {
        surprise.transform.position = new Vector3(
            surprise.transform.position.x,
            surpriseDot.transform.position.y,
            surprise.transform.position.z
            );
    }
}
