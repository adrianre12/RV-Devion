using DevionGames;
using RVModules.RVUtilities;
using UnityEditor;
using UnityEngine;

namespace RVDevion
{
    public class RVSetBool : RvSetVaraibleBase
    {
        public bool useBlackboard;
        [Tooltip("If empty Variable Name will be used")]
        [ConditionalHide("useBlackboard")]
        public string blackboardVarName;
        [ConditionalHide("useBlackboard", inverse = true)]
        public bool value;

        public override ActionStatus OnUpdate()
        {
            _useBlackboard = useBlackboard; // done this way as the ConditionalHide does not work in super class
            _blackboardVarName = blackboardVarName;
            graphVarValue = value;
            graphVarType = GraphVarType.@bool;
            return base.OnUpdate();
        }
    }
}
