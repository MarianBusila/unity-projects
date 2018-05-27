using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{

    public GameObject areaObject;
    public int lastAgentHit;

    // Use this for initialization
    void Start()
    {
        lastAgentHit = -1;
    }

    private void OnTriggerExit(Collider other)
    {
        TennisArea area = areaObject.GetComponent<TennisArea>();
        TennisAgent agentA = area.agentA.GetComponent<TennisAgent>();
        TennisAgent agentB = area.agentB.GetComponent<TennisAgent>();

        // agent gets rewarded for hitting ball over the net
        if (other.name == "over")
        {
            if (lastAgentHit == 0)
            {
                agentA.AddReward(0.1f);
            }
            else
            {
                agentB.AddReward(0.1f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TennisArea area = areaObject.GetComponent<TennisArea>();
        TennisAgent agentA = area.agentA.GetComponent<TennisAgent>();
        TennisAgent agentB = area.agentB.GetComponent<TennisAgent>();

        // ball either entered in collision with the floor or exited
        // the goal for both agents is to keep the ball in the air
        if(collision.gameObject.tag == "iWall")
        {
            if (collision.gameObject.name == "WallA" || collision.gameObject.name == "WallB" || collision.gameObject.name == "Net")
            {
                // agentA is the last who touched the ball
                if (lastAgentHit == 0)
                {
                    Debug.Log("The ball hit the back wall or net and agentA was the last to touch the ball");
                    agentA.AddReward(-0.01f);
                    agentB.SetReward(0);
                    agentB.score += 1;
                }
                // agentB is the last who touched the ball
                else
                {
                    Debug.Log("The ball hit the back wall or net and agentB was the last to touch the ball");
                    agentA.SetReward(0);
                    agentB.SetReward(-0.01f);
                    agentA.score += 1;
                }
            }
            else if (collision.gameObject.name == "FloorA")
            {
                Debug.Log("The ball hit floorA");
                // ball touched floor A
                agentA.AddReward(-0.01f);
                agentB.SetReward(0);
                agentB.score += 1;
            }
            else if (collision.gameObject.name == "FloorB")
            {
                Debug.Log("The ball hit floorB");
                // ball touched floor B
                agentB.AddReward(-0.01f);
                agentA.SetReward(0);
                agentA.score += 1;
            }

            agentA.Done();
            agentB.Done();
            area.MatchReset();
        }

        if(collision.gameObject.tag == "agent")
        {
            Debug.Log("An agent hit the ball");
            lastAgentHit = collision.gameObject.name == "AgentA" ? 0 : 1;
        }
    }
}
