using System;
using UnityEngine;

namespace RigidbodyModels
{
    public partial class RigidbodyModelBase
    {
        protected virtual bool TryUpdateDynamicMove(Vector2 velocity, out MoveOptions options)
        {
            options = null;

            return false;
        }

        protected virtual bool TryUpdateKinematicMove(out MoveOptions options)
        {
            options = null;

            return false;
        }

        protected void AddForce(Vector2 force)
        {
            if (_body.bodyType == RigidbodyType2D.Dynamic)
            {
                _body.AddForce(force);
            }
            else if (_body.bodyType == RigidbodyType2D.Kinematic)
            {
                // TODO: Добавление силы для кинетических тел
            }
        }

        private void UpdateDynamicMove()
        {
            if (TryUpdateDynamicMove(_body.velocity, out MoveOptions options))
            {
                options.SetDynamicOption(_body);

                return;
            }

            Vector2 force = _direction * (acceleration * Time.deltaTime);

            force.Normalize();

            _body.AddForce(force);
        }

        private void UpdateKinematicMove()
        {
            if (TryUpdateKinematicMove(out MoveOptions options))
            {
                options.SetKinematicOption(_body);

                return;
            }

            var positionToChange =
                new Vector2(_direction.x * acceleration, _direction.y * acceleration + _body.gravityScale);

            _body.MovePosition(_body.position + positionToChange);
        }

        private void UpdateMove()
        {
            switch (_body.bodyType)
            {
                case RigidbodyType2D.Kinematic:
                    UpdateKinematicMove();
                    break;
                case RigidbodyType2D.Dynamic:
                    UpdateDynamicMove();
                    break;
                default: throw new Exception("This model body type can't be static");
            }
        }

        private void UpdateMaxVelocity()
        {
            if (_body.bodyType == RigidbodyType2D.Static)
            {
                return;
            }
            
            if (_body.velocity.magnitude >= maxSpeed)
            {
                _body.velocity = _body.velocity.normalized * maxSpeed;
            }
        }

        protected class MoveOptions
        {
            public Vector2? Force { get; set; }

            public Vector2? Velocity { get; set; }

            public Vector2? Position { get; set; }

            public void SetKinematicOption(Rigidbody2D body)
            {
                if (Velocity != null)
                {
                    body.velocity = (Vector2) Velocity;
                }

                if (Position != null)
                {
                    body.MovePosition((Vector2) Position);
                }
            }

            internal void SetDynamicOption(Rigidbody2D body)
            {
                if (Force != null)
                {
                    body.AddForce((Vector2) Force);
                }

                if (Velocity != null)
                {
                    body.velocity = (Vector2) Velocity;
                }

                if (Position != null)
                {
                    body.position = (Vector2) Position;
                }
            }
        }
    }
}