using System;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] ParticleSystem cloudDustFx;

    public Team Team;
    [SerializeField] DefendAI defendAI;
    [SerializeField] AttackAI attackAI;
    [SerializeField] MovementController _movement;
    [SerializeField] SoldierVisualizer visualizer;
    [SerializeField] Collider _collider;
    public Collider Collider => _collider;

    public Ball HoldingBall;

    public MovementController Movement => _movement;
    private AIController activeAI;

    private SoldierAIMode mode;

    private void Awake() {
        defendAI.DisableAllBehaviour();
        attackAI.DisableAllBehaviour();
        visualizer.SetTeam(Team);

        if(mode == SoldierAIMode.Attack)
        {
            attackAI.gameObject.SetActive(true);
            attackAI.EnableEntry();
            activeAI = attackAI;
        }
        if(mode == SoldierAIMode.Defend)
        {
            defendAI.gameObject.SetActive(true);
            defendAI.EnableEntry();
            activeAI = defendAI;
        }
    }

    internal bool IsInActive()
    {
        return activeAI.IsInactive();
    }

    internal void SetMode(SoldierAIMode mode)
    {
        this.mode = mode;
    }

    internal void ReceivePassing(Ball ball)
    {
        attackAI.ReceivePassing(ball);
    }

    internal void SetJoyStick(JoyStickController joyStick)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag(Tags.Barrier))
            OnHitBarrier();
        activeAI?.OnCollisionEnter(other);
    }

    private void OnHitBarrier()
    {
        Instantiate(cloudDustFx, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}