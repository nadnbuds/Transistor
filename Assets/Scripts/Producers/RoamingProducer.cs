using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RoamingProducer : Producer
{
    [SerializeField]
    private int maxGenTilReroute;

    private int counterTilReroute;

    [SerializeField]
    private float maxDistance;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        counterTilReroute = Random.Range(1, maxGenTilReroute);
        RegisterProducer();
        reroute();

    }

    public override void Generate()
    {
        Vector3 loc = RandDrop();
        VitalResource res = pooler.RetrieveCopy().GetComponent<VitalResource>();
        res.transform.position = loc + transform.position;
        counterTilReroute--;
        if(counterTilReroute == 0)
        {
            counterTilReroute = Random.Range(1, maxGenTilReroute);
            reroute();
        }
    }

    private void reroute()
    {
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * maxDistance;
        randDir += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDir, out hit, maxDistance, 1);
        agent.destination = hit.position;
    }
}
