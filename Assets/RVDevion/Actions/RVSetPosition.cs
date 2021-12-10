using DevionGames;
using RVModules.RVUtilities;
using UnityEngine;

namespace RVDevion
{
    public class RVSetPosition : RvSetVaraibleBase
    {
        [SerializeField]
        private TargetType _sourcePosition = TargetType.Player;

        public override ActionStatus OnUpdate()
        {
            graphVarValue = GetTarget(_sourcePosition).transform.position;       
            graphVarType = GraphVarType.Vector3;
            return base.OnUpdate();
        }
    }
}
