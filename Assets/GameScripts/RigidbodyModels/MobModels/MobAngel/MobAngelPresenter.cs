using UnityEngine;

namespace RigidbodyModels.MobModels.MobAngel
{
    public class MobAngelPresenter : MonoBehaviour
    {
        private Animator _animator;
        
        private readonly int _shoot = Animator.StringToHash(name: "Shoot");

        private void Start()
        {
            var mob = GetComponent<MobAngel>();
            
            SetAnimation(mob);

            LoadAnimation();
        }

        private void LoadAnimation()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                _animator = gameObject.AddComponent<Animator>();
            }
        }

        private void SetAnimation(MobAngel mob)
        {
            mob.WasShot += ShootAnimation;
        }

        private void ShootAnimation()
        {
            _animator.SetTrigger(_shoot);
        }
    }
}