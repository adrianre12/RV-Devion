using DevionGames.QuestSystem;
using RVModules.RVSmartAI.GraphElements;
using UnityEngine;


[System.Serializable]
public class RVQuestTaskScorer : AiScorer
{
    [QuestPicker(true)]
    [SerializeField]
    protected Quest _quest;
    [SerializeField]
    protected string _taskName;
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
    [Tooltip("Returns the status score once then resets the score to the default value")]
    [SerializeField]
    protected bool _oneShot;

    private float _statusScore;

    public void Start()
    {
        _statusScore = _default;
        if (QuestManager.current.HasQuest(_quest, out Quest quest))
        {
            QuestTask task = quest.tasks.Find(x => x.Name == _taskName);
            SetStatusScore(task.Status);
        }
            

        QuestManager.current.OnTaskStatusChanged += OnTaskStatusChanged;
    }

    public void OnTaskStatusChanged(Quest quest, QuestTask questTask)
    {
        if (quest.Name != _quest.Name || questTask.Name != _taskName)
            return;
        
        SetStatusScore(questTask.Status);
    }

    private void SetStatusScore(Status status)
    {
        switch (status)
        {
            case Status.Inactive:
                {
                    _statusScore = _inactive;
                    break;
                }
            case Status.Active:
                {
                    _statusScore = _active;
                    break;
                }
            case Status.Completed:
                {
                    _statusScore = _completed;
                    break;
                }
            case Status.Failed:
                {
                    _statusScore = _failed;
                    break;
                }
            case Status.Canceled:
                {
                    _statusScore = _canceled;
                    break;
                }
            default:
                {
                    _statusScore = _default;
                    break;
                }
        }
    }

    public override float Score(float _deltaTime)
    {
        float score = _statusScore;
        if (_oneShot)
            _statusScore = _default;
        return score;
    }
}

