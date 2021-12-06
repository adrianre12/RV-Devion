using DevionGames;
using RVModules.RVUtilities;
using UnityEngine;

namespace RVDevion
{
    public class RVSetFloat : RvSetVaraibleBase
    {
        public bool useBlackboard;
        [Tooltip("If empty Variable Name will be used")]
        [ConditionalHide("useBlackboard")]
        public string blackboardVarName;
        [ConditionalHide("useBlackboard", inverse = true)]
        public float value;

        public override ActionStatus OnUpdate()
        {
            if (useBlackboard)
            {
                blackboardVarName = blackboardVarName == "" ? variableName : blackboardVarName;
                if (blackboard == null)
                {
                    Debug.LogWarning("Blackboard not found");
                    return ActionStatus.Failure;
                }
                Variable bvar = blackboard.GetVariable(blackboardVarName);
                if (bvar == null)
                {
                    Debug.LogWarning($"Blackboard variable {blackboardVarName} not found");
                    return ActionStatus.Failure;
                }
                if (bvar.GetType() != typeof(float))
                {
                    Debug.LogWarning($"Blackboard variable {blackboardVarName} is not a float");
                    return ActionStatus.Failure;
                }
                graphVarValue = bvar.RawValue;
            }
            else
            {
                graphVarValue = value;
            }
            graphVarType = GraphVarType.@float;
            return base.OnUpdate();
        }
    }
}
