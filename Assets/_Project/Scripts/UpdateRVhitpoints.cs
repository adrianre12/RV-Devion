using DevionGames;
using DevionGames.StatSystem;
using RVHonorAI;
using UnityEngine;

public class UpdateRVhitpoints : Action
{
    StatsHandler _statsHandler;
    ICharacter _character;
    [SerializeField]
    private string _healthStatName = "Health";

    public override void OnSequenceStart()
    {
        base.OnSequenceStart();
        _statsHandler = gameObject.GetComponent<StatsHandler>();
        _character = gameObject.GetComponent<ICharacter>();
    } 
    public override ActionStatus OnUpdate()
    {
        Attribute attribute = _statsHandler.GetStat(_healthStatName) as Attribute;
        _character.HitPoints = attribute.CurrentValue;

        return ActionStatus.Success;
    }
}
