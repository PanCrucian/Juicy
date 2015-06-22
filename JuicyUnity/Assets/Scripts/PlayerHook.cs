using UnityEngine;
using System.Collections;

public class PlayerHook : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("DotTrigger"))
            Player.Instance.triggeredDot = coll.GetComponentInParent<Dot>();
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag.Equals("DotTrigger"))
            Player.Instance.triggeredDot = null;
    }
}
