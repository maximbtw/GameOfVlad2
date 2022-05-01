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

        protected override void Start()
        {
            base.Start();

            _shootCooldownTimer = new Timer(shootCooldown);
            _shootCooldownTimer.Ended += Shoot;
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            if (CanAttackPlayer())
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

        protected override bool TryGetAngleRotation(Vector2 direction, float rotation, out float angle)
        {
            angle = Helpers.GetAngleFromDirection(direction);

            angle -= 180;

            return true;
        }

        protected override void UpdateAdditionalData()
        {
            if (CanAttackPlayer())
            {
                _shootCooldownTimer.UpdateLoop();
            }
        }

        protected override bool TryUpdateDynamicMove(Vector2 direction, Vector2 velocity, out MoveOptions options)
        {
            if (!CanMoveToTarget())
            {
                options = new MoveOptions
                {
                    Force = Vector2.zero,
                    Velocity = Vector2.zero
                };
            }
            else
            {
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
        
        private Vector2 GetDistanceToTarget() =>
            new Vector2(
                x: Mathf.Abs(this.TargetPosition.x - this.Position.x), 
                y: Mathf.Abs(this.TargetPosition.y - this.Position.y));

        private bool CanAttackPlayer()
        {
            Vector2 distance = GetDistanceToTarget();

            return distance.x <= playerVisibilityDistance && distance.y <= playerVisibilityDistance;
        }

        private bool CanMoveToTarget()
        {
            Vector2 distance = GetDistanceToTarget();

            return (distance.x >= minimumDistanceToMove || distance.y >= minimumDistanceToMove) && CanAttackPlayer();
        }
        
        private void Shoot()
        {
            MobAngelProjectile bullet = Instantiate(bulletPrefab);

            bullet.Initialize(parent: this, startPosition: this.Position, this.TargetPosition);
        }
    }
}