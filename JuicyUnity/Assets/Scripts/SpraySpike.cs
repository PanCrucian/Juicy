using UnityEngine;
using System.Collections;

public class SpraySpike : Spike {

    public SpraySpikeDot spray;
    /// <summary>
    /// Как часто будет спавниться точка
    /// </summary>
    public float sprayInterval = 1f;
    /// <summary>
    /// Скорость передвижения
    /// </summary>
    public float spraySpeed = 3.5f;
    /// <summary>
    /// Время жизни точки
    /// </summary>
    public float sprayLifeTime = 0.25f;

    private float lastSprayTime;
    private bool upMove = true;

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
        if (Game.Instance.state != Game.GameStates.Game)
            return;
        if (Player.Instance.healthState == Player.HealthStates.Death)
            return;
        if (Mathf.Abs(Time.time - lastSprayTime) >= sprayInterval)
        {
            Spray();
        }
    }

    /// <summary>
    /// Спавним убийственную точку
    /// </summary>
    void Spray()
    {
        StartCoroutine(SprayNumerator(upMove));
        upMove = !upMove;
        lastSprayTime = Time.time;
    }

    IEnumerator SprayNumerator(bool upMove)
    {
        SpraySpikeDot newSpray = (SpraySpikeDot)Instantiate(spray);
        newSpray.transform.position = transform.position;
        newSpray.transform.parent = transform;

        for (float t = 0; t <= sprayLifeTime; t += Time.deltaTime)
        {
            float offset = spraySpeed * Time.deltaTime;
            newSpray.transform.localPosition = new Vector3(
                newSpray.transform.localPosition.x,
                newSpray.transform.localPosition.y + (upMove ? offset : offset * -1f),
                newSpray.transform.localPosition.z
                );
            yield return new WaitForEndOfFrame();
        }

        Destroy(newSpray.gameObject);
    }
}
