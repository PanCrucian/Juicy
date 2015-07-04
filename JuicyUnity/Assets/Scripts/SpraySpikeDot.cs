using UnityEngine;
using System.Collections;

public class SpraySpikeDot : Spike {

    private bool allowKill = true;

    public override void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("Player") && allowKill)
            Player.Instance.Die();

        if (coll.tag.Equals("DotTrigger"))
            allowKill = false;
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag.Equals("DotTrigger"))
            allowKill = true;
    }
}
