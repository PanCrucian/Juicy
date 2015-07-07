using UnityEngine;
using System.Collections;

public class SurpriseDot : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag.Equals("Player"))
        {
            GetComponentInParent<SurpriseSpike>().SurpriseMotherFucker();
        }
        if (coll == GetComponentInParent<SurpriseSpike>().surprise.GetComponent<Collider>())
            Destroy(gameObject);
    }
}
