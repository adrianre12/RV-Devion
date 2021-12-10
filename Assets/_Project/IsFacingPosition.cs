using RVModules.RVSmartAI.Content.AI.DataProviders;
using RVModules.RVSmartAI.Content.AI.Scorers;
using RVModules.RVUtilities.Extensions;
using UnityEngine;


    /// <summary>
    /// Make sure ITargetProvider.Target won't be null before using this scorer
    /// Required context: ITargetProvider, IAttackAngle, IMovement
    /// </summary>
    public class IsFacingPosition : AiAgentScorer
    {
        [SerializeField]
        private Vector3Provider _position;
        [SerializeField]
        private float notFacingScore;

        [SerializeField]
        private float facingAngle = 5f;

        protected override string DefaultDescription => "";

        protected override void OnContextUpdated()
        {
            base.OnContextUpdated();
        }

        public override float Score(float _deltaTime)
        {
            var angle = Vector2.Angle((movement.Rotation * Vector3.forward).ToVector2(), _position.GetData().ToVector2() - movement.Position.ToVector2());
            return Mathf.Abs(angle) < facingAngle ? score : notFacingScore;
        }
    }
