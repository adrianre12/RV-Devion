using System;
using DevionGames.StatSystem;
using RVHonorAI;
using RVHonorAI.Combat;
using RVHonorAI.Systems;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class PlayerBridge : MonoBehaviour, ICharacter
{
    private StatsHandler statsHandler;

    [SerializeField]
    private string _healthStatName = "Health";

    [Tooltip("This is only used to calcuate the Danger value")]
    [SerializeField]
    private string _attackStatName = "Melee Attack";

    #region Fields

    [SerializeField]
    private AiGroup group;

    [SerializeField]
    private Transform headTransform;

    private float radius = .5f;

    [SerializeField]
    private RelationshipSystem relationshipSystem;

    #endregion

    #region Properties


    public float Danger => .1f * HitPoints * .1f * DamagePerSecond;
    public float MaxHitPoints { get; }
    public UnityEvent OnKilled { get; set; }
    public float RunningSpeed { get; set; }
    public float WalkingSpeed { get; set; }
    public IAttack CurrentAttack { get; set; }
    public IWeapon CurrentWeapon { get; }
    public ICharacterAi CharacterAi { get; }

    public float Radius => radius;
    public Transform Transform => transform;
    public Transform AimTransform => headTransform;
    public Transform HeadTransform => headTransform;

    public float HitPoints
    {
        get => (statsHandler.GetStat(_healthStatName) as DevionGames.StatSystem.Attribute).CurrentValue;
        set { }
    }

    public bool TreatNeutralCharactersAsEnemies { get; }

    public float DamagePerSecond => statsHandler.GetStat(_attackStatName).Value;
    
    public float Armor => 20;

    public float Courage => 50;

    public float AttackRange => 2;

    AiGroup IRelationship.AiGroup
    {
        get { return group; }
        set { }
    }
    
    public Transform VisibilityCheckTransform => HeadTransform;
    public Vector3 FovPosition => HeadTransform.position;
    public ITarget Target { get; }
    public TargetInfo CurrentTarget { get; set; }

    #endregion

    #region Private methods

    void Start()
    {
        statsHandler = GetComponent<StatsHandler>();
        if (statsHandler == null)
            Debug.LogWarning($"No StatsHandler found for {this.name}");
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// This is called by the NPC to apply damage to the player. The return value should be the actuall damage done to the player after all protections (buffs, equipment etc) have been applied. 
    /// </summary>
    public float ReceiveDamage(float _damage, Object _damageSource, DamageType _damageType, bool _damageEnemyOnly = false, Vector3 hitPoint = default, Vector3 _hitForce = default, float forceRadius = default)
    {
        //Debug.LogFormat("Damage recieved {0}", _damage);
        if (statsHandler != null)
        {
            statsHandler.ApplyDamage(_healthStatName, _damage);
        }
        return _damage;
    }

    public void Kill() => throw new NotImplementedException();

    public void Kill(Vector3 hitPoint, Vector3 hitForce = default, float forceRadius = default) => throw new NotImplementedException();

    public void Heal(float _amount)
    {
    }

    public virtual bool IsEnemy(IRelationship _other, bool _contraCheck = false) => relationshipSystem.IsEnemy(this, _other, _contraCheck);

    public virtual bool IsAlly(IRelationship _other) => relationshipSystem.IsAlly(this, _other);

    #endregion
}
