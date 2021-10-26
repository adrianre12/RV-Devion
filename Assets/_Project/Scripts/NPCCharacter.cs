using DevionGames.StatSystem;
using RVHonorAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : Character
{
    private StatsHandler statsHandler;
    [SerializeField]
    private string _healthStatName = "Health";
    public void onCurrentValueChange()
    {
        Debug.Log("onCurrentValueChange");
    }

    protected override void Start()
    {
        base.Start();
        statsHandler = GetComponent<StatsHandler>();
        if (statsHandler == null)
            Debug.LogWarning("No StatsHandler found for " + this.name);
        else
        {
            Attribute stat = statsHandler.GetStat(_healthStatName) as Attribute;
            if (stat == null) return;
            stat.onCurrentValueChange += onCurrentValueChange;
        }
        DevionGames.EventHandler.Register<GameObject, string, float>(gameObject, "OnGetHit", OnGetHit);
    }

    public void OnGetHit(GameObject sender, string statName, float value)
    {
        Debug.Log($"recived {statName} value {value}");
        if (sender.TryGetComponent<PlayerCharacter>(out PlayerCharacter pc))
            ReceiveDamage(value, pc, DamageType.Physical, true);
    }
}
