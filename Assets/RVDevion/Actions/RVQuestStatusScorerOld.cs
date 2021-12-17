using DevionGames.QuestSystem;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;


[System.Serializable]
public class RVQuestStatusScorerOld : AiScorer
{
    [QuestPicker(true)]
    [SerializeField]
    protected Quest _quest;
    [SerializeField]
    protected float _inactive = (int)Status.Inactive;
    [SerializeField]
    protected float _active = (int)Status.Active;
    [SerializeField]
    protected float _completed = (int)Status.Completed;
    [SerializeField]
    protected float _failed = (int)Status.Failed;
    [SerializeField]
    protected float _canceled = (int)Status.Canceled;
    [Tooltip("The value returned if the quest is not found")]
    [SerializeField]
    protected float _default = -1f;

    public override float Score(float _deltaTime)
    {
        if (QuestManager.current.HasQuest(_quest, out Quest quest))
        {
            switch (quest.Status)
            {
                case Status.Inactive:
                    {
                        return _inactive;
                    }
                case Status.Active:
                    {
                        return _active;
                    }
                case Status.Completed:
                    {
                        return _completed;
                    }
                case Status.Failed:
                    {
                        return _failed;
                    }
                case Status.Canceled:
                    {
                        return _canceled;
                    }
            }
        }
        return _default;
    }
}