using System.Collections.Generic;
using UnityEngine;

public class TitanBossAgent : IGoapAgent
{
    [Header("Sensors")]
    private Sensor ChaseSensor => Sensors["ChaseSensor"];
    private Sensor AttackSensor => Sensors["AttackSensor"];
    
    protected override void OnStart()
    {
        DisableGoap();
    }

    protected override void SetupBeliefs()
    {
        Beliefs = new Dictionary<string, AgentBelief>();
        BeliefFactory factory = new(this, Beliefs);

        factory.AddBelief("Nothing", () => false);

        factory.AddBelief("AgentIdle", () => !AgentNavmesh.hasPath);
        factory.AddBelief("AgentMoving", () => AgentNavmesh.hasPath);

        factory.AddSensorBelief("PlayerInChaseRange", ChaseSensor);
        factory.AddSensorBelief("PlayerInAttackRange", AttackSensor);

        factory.AddBelief("AttackingPlayer", () => false); // Player can always be attacked, this will never become true
    }

    protected override void SetupActions()
    {
        Actions = new HashSet<AgentAction>
        {
            new AgentAction.Builder("Relax")
            .WithStrategy(new IdleStrategy(5))
            .AddEffect(Beliefs["Nothing"])
            .Build(),
            
            new AgentAction.Builder("Wander Around")
            .WithStrategy(new WanderStrategy(AgentNavmesh, 10))
            .AddEffect(Beliefs["AgentMoving"])
            .Build(),

            new AgentAction.Builder("ChasePlayer")
            .WithStrategy(new MoveStrategy(AgentNavmesh, () => Beliefs["PlayerInChaseRange"].Location, GAnimator))
            .AddPrecondition(Beliefs["PlayerInChaseRange"])
            .AddEffect(Beliefs["PlayerInAttackRange"])
            .Build(),
            
            new AgentAction.Builder("AttackPlayer")
            .WithStrategy(new AttackStrategy(GAnimator))
            .AddPrecondition(Beliefs["PlayerInAttackRange"])
            .AddEffect(Beliefs["AttackingPlayer"])
            .Build()
        };
    }

    protected override void SetupGoals()
    {
        Goals = new HashSet<AgentGoal>
        {
            new AgentGoal.Builder("Chill Out")
                .WithPriority(1)
                .WithDesiredEffect(Beliefs["Nothing"])
                .Build(),

            new AgentGoal.Builder("Wander")
                .WithPriority(1)
                .WithDesiredEffect(Beliefs["AgentMoving"])
                .Build(),
            
            new AgentGoal.Builder("DestroyPlayer")
                .WithPriority(3)
                .WithDesiredEffect(Beliefs["AttackingPlayer"])
                .Build()
        };
    }
}
