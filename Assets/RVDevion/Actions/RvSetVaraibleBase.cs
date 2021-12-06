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
        public string graphName = "BaseGraph";
        public string variableName;

        private AiGraph _graph;
        protected GraphVarType graphVarType;
        protected object graphVarValue;
        
        public override ActionStatus OnUpdate()
        {
            _graph = null;
            if (!gameObject.TryGetComponent<CharacterAi>(out CharacterAi _characterAi))
            {
                Debug.Log("CharacterAI component not found");
                return ActionStatus.Failure;
            }
            if (_characterAi?.Ai.MainAiGraph.name == graphName)
            {
                _graph = _characterAi?.Ai.MainAiGraph;
            } else
            {
                AiGraph[] graphs = _characterAi?.Ai.SecondaryGraphs;
                _graph = graphs.FirstOrDefault<AiGraph>(g => g.name == graphName);
            }

            if (_graph == null)
            {
                Debug.Log($"Graph {graphName} not found");
                return ActionStatus.Failure;
            }

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
                        v.AssureStringExist(variableName);
                        v.SetVector2(variableName, (Vector2)graphVarValue);
                        break;
                    }
                case GraphVarType.Vector3:
                    {
                        v.AssureStringExist(variableName);
                        v.SetVector3(variableName, (Vector3)graphVarValue);
                        break;
                    }
                case GraphVarType.Vector3List:
                    {
                        v.AssureStringExist(variableName);
                        v.SetVector3List(variableName, (List<Vector3>)graphVarValue);
                        break;
                    }
                case GraphVarType.LayerMask:
                    {
                        v.AssureStringExist(variableName);
                        v.SetLayerMask(variableName, (LayerMask)graphVarValue);
                        break;
                    }
                case GraphVarType.AnimationCurve:
                    {
                        v.AssureStringExist(variableName);
                        v.SetAnimationCurve(variableName, (AnimationCurve)graphVarValue);
                        break;
                    }
                case GraphVarType.UnityObject:
                    {
                        v.AssureStringExist(variableName);
                        v.SetUnityObject(variableName, (Object)graphVarValue);
                        break;
                    }
            }

            return ActionStatus.Success;
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

