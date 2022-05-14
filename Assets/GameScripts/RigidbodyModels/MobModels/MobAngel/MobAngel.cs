using System;
using Components;
using UnityEngine;
using Utils;

namespace RigidbodyModels.MobModels.MobAngel
{
    public class MobAngel : MobModelBase
    {
        [Header("Movements")]
        [SerializeField] private float playerVisibilityDistance;
        [SerializeField] private float minimumDistanceToMove;
        [Header("Attack")]
        [SerializeField] [Range(0.01f, 10)] private float shootCooldown;
        [SerializeField] private MobAngelProjectile bulletPrefab;

        private Timer _shootCooldownTimer;
        
        private AttackState _currentState = AttackState.Stay;
        private AttackState _previousState;

        public event Action WasShot;
        public event Action PlayerEnterInVisibilityDistance;

        protected override void Start()
        {
            base.Start();

            _shootCooldownTimer = new Timer(shootCooldown);
            _shootCooldownTimer.Ended += Shoot;
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            if (PlayerInVisibilityDistance())
            {
                direction = this.TargetPosition - this.Position;
                direction.Normalize();
            }
            else
            {
                direction = this.Direction;
            }

            return true;
        }

        protected override bool TryGetAngleRotation(float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(this.Direction);

            angle -= 180;

            return true;
        }

        protected override void UpdateAdditionalData()
        {
            bool playerInVisibilityDistance = PlayerInVisibilityDistance();
            
            if (playerInVisibilityDistance)
            {
                _shootCooldownTimer.UpdateLoop();
            }

            UpdateState(playerInVisibilityDistance);
        }

        protected override bool TryUpdateDynamicMove(Vector2 velocity, out MoveOptions options)
        {
            Vector2 direction = this.Direction;
            
            bool playerInTheMinimumDistance = PlayerInTheMinimumDistance();
            bool playerInTheMaximumDistance = PlayerInVisibilityDistance();
            
            if (!playerInTheMinimumDistance && !playerInTheMaximumDistance)
            {
                options = new MoveOptions
                {
                    Force = Vector2.zero,
                    Velocity = Vector2.zero
                };
            }
            else
            {
                if (playerInTheMinimumDistance)
                {
                    direction *= -1;
                }
                
                Vector2 force = direction * (acceleration * Time.deltaTime);

                force.Normalize();

                options = new MoveOptions
                {
                    Force = force,
                    Velocity = velocity.magnitude >= maxSpeed ? velocity.normalized * maxSpeed : (Vector2?) null
                };
            }

            return true;
        }
        
        private void UpdateState(bool playerInVisibilityDistance)
        {
            _previousState = _currentState;
            _currentState = playerInVisibilityDistance ? AttackState.Attack : AttackState.Stay;

            if (_previousState == AttackState.Stay && _currentState == AttackState.Attack)
            {
                PlayerEnterInVisibilityDistance?.Invoke();
            }
        }
        
        private Vector2 GetDistanceToTarget() =>
            new Vector2(
                x: Mathf.Abs(this.TargetPosition.x - this.Position.x), 
                y: Mathf.Abs(this.TargetPosition.y - this.Position.y));

        private bool PlayerInVisibilityDistance()
        {
            Vector2 distance = GetDistanceToTarget();

            return distance.x <= playerVisibilityDistance && distance.y <= playerVisibilityDistance;
        }

        private bool PlayerInTheMinimumDistance()
        {
            Vector2 distance = GetDistanceToTarget();

            return distance.x < minimumDistanceToMove && distance.y < minimumDistanceToMove;
        }
        
        private void Shoot()
        {
            WasShot?.Invoke();
            
            MobAngelProjectile bullet = Instantiate(bulletPrefab);

            bullet.Initialize(
                GameObjectLayer.MobObject, 
                parent: this, 
                startPosition: this.Position,
                this.TargetPosition);
        }

        private enum AttackState
        {
            Attack,
            Stay
        }
    }
}