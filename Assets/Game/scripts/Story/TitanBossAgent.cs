using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        
        factory.AddBelief("FacingPlayer", () => 
        {
            if (!ChaseSensor.IsTargetInRange) return false;
    
            Vector3 toPlayer = (ChaseSensor.TargetPosition - transform.position).With(y: 0f).normalized;
            Vector3 forward = transform.forward.With(y: 0f).normalized;
    
            float dot = Vector3.Dot(forward, toPlayer);
            return dot > 0.95f; // Roughly 18 degrees tolerance
        });
        factory.AddBelief("AttackingPlayer", () => false);
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
            
            new AgentAction.Builder("FacePlayer")
                .WithStrategy(new LookAtStrategy(
                    AgentNavmesh.transform, 
                    () => ChaseSensor.TargetPosition, 
                    rotationSpeed: 8f,
                    onComplete: null))
                .AddPrecondition(Beliefs["PlayerInAttackRange"]) // Only face when close enough to attack soon
                .AddEffect(Beliefs["FacingPlayer"])
                .Build(),
            
            new AgentAction.Builder("AttackPlayer")
            .WithStrategy(new AttackStrategy(GAnimator))
            .AddPrecondition(Beliefs["PlayerInAttackRange"])
            .AddPrecondition(Beliefs["FacingPlayer"])
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
