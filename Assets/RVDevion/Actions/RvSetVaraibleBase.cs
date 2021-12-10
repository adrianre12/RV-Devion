using UnityEngine;
using DevionGames;
using RVHonorAI;
using RVModules.RVSmartAI;
using System.Linq;
using System.Collections.Generic;

namespace RVDevion
{
    public abstract class RvSetVaraibleBase : DevionGames.Action
    {
        [Tooltip("Name of the graph to use, if blank select main graph")]
        public string graphName = "";
        public string variableName;
        [Tooltip("Defer setting the variable until the action sequence ends.")]
        public bool setOnEnd;

        private AiGraph _graph;
        private bool _setDone;
        protected GraphVarType graphVarType;
        protected object graphVarValue;
        protected bool _useBlackboard;
        protected string _blackboardVarName;

        public override ActionStatus OnUpdate()
        {
            _graph = null;
            if (!gameObject.TryGetComponent<CharacterAi>(out CharacterAi _characterAi))
            {
                Debug.Log("CharacterAI component not found");
                return ActionStatus.Failure;
            }
            if (graphName == "" || _characterAi?.Ai.MainAiGraph.name == graphName)
            {
                _graph = _characterAi?.Ai.MainAiGraph;
            }
            else
            {
                AiGraph[] graphs = _characterAi?.Ai.SecondaryGraphs;
                _graph = graphs.FirstOrDefault<AiGraph>(g => g.name == graphName);
            }

            if (_graph == null)
            {
                Debug.Log($"Graph {graphName} not found");
                return ActionStatus.Failure;
            }

            if (_useBlackboard)
            {
                _blackboardVarName = _blackboardVarName == "" ? variableName : _blackboardVarName;
                if (blackboard == null)
                {
                    Debug.LogWarning("Blackboard not found");
                    return ActionStatus.Failure;
                }
                Variable bvar = blackboard.GetVariable(_blackboardVarName);
                if (bvar == null)
                {
                    Debug.LogWarning($"Blackboard variable {_blackboardVarName} not found");
                    return ActionStatus.Failure;
                }
                if (bvar.GetType() != graphVarValue.GetType())
                {
                    Debug.LogWarning($"Blackboard variable {_blackboardVarName} is not a {graphVarValue.GetType()}");
                    return ActionStatus.Failure;
                }
                graphVarValue = bvar.RawValue;
            }

            _setDone = false;
            if (!setOnEnd)
                SetVariable();

            return ActionStatus.Success;
        }

        public override void OnSequenceEnd()
        {
            if (!_setDone) // this stops the variable being set twice.
                SetVariable();
            base.OnSequenceEnd();
        }

        protected void SetVariable() {
            Debug.Log("Setting var");
            AiVariables v = _graph.GraphAiVariables;
            switch (graphVarType)
            {
                case GraphVarType.@object:
                {
                    v.AssureObjectExist(variableName);
                    v.SetObject(variableName, graphVarValue);
                    break;
                }
                case GraphVarType.@bool:
                    {
                        v.AssureBoolExist(variableName);
                        v.SetBool(variableName, (bool)graphVarValue);
                        break;
                    }
                case GraphVarType.@int:
                    {
                        v.AssureIntExist(variableName);
                        v.SetInt(variableName, (int)graphVarValue);
                        break;
                    }
                case GraphVarType.@float:
                    {
                        v.AssureFloatExist(variableName);
                        v.SetFloat(variableName, (float)graphVarValue);
                        break;
                    }
                case GraphVarType.@string:
                    {
                        v.AssureStringExist(variableName);
                        v.SetString(variableName, (string)graphVarValue);
                        break;
                    }
                case GraphVarType.Vector2:
                    {
                        v.AssureVector2Exist(variableName);
                        v.SetVector2(variableName, (Vector2)graphVarValue);
                        break;
                    }
                case GraphVarType.Vector3:
                    {
                        v.AssureVector3Exist(variableName);
                        v.SetVector3(variableName, (Vector3)graphVarValue);
                        break;
                    }
                case GraphVarType.Vector3List:
                    {
                        v.AssureVector3ListExist(variableName);
                        v.SetVector3List(variableName, (List<Vector3>)graphVarValue);
                        break;
                    }
                case GraphVarType.LayerMask:
                    {
                        v.AssureLayerMaskExist(variableName);
                        v.SetLayerMask(variableName, (LayerMask)graphVarValue);
                        break;
                    }
                case GraphVarType.AnimationCurve:
                    {
                        v.AssureAnimationCurveExist(variableName);
                        v.SetAnimationCurve(variableName, (AnimationCurve)graphVarValue);
                        break;
                    }
                case GraphVarType.UnityObject:
                    {
                        v.AssureUnityObjectExist(variableName);
                        v.SetUnityObject(variableName, (Object)graphVarValue);
                        break;
                    }
            }
            _setDone = true;
        }


    }

    public enum GraphVarType
    {
        @object,
        @bool,
        @int,
        @float,
        @string,
        Vector2,
        Vector3,
        Vector3List,
        LayerMask,
        AnimationCurve,
        UnityObject
    }
        
}

