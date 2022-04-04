using UnityEngine;

namespace RigidbodyModels.Player
{
    public class Player : DynamicRigidbodyModelBase
    {
        private PlayerMoveController _moveController;
        private PlayerWeaponController _weaponController;
        
        public int HeatPoint { get; private set; }
        
        public int Armor { get; private set; }
        
        protected override void Start()
        {
            base.Start();

            LoadMoveController();
            LoadWeaponController();
        }

        private void LoadWeaponController()
        {
            _weaponController = gameObject.GetComponent<PlayerWeaponController>();

            if (_weaponController == null)
            {
                gameObject.AddComponent(typeof(PlayerWeaponController));

                LoadWeaponController();
            }
        }

        private void LoadMoveController()
        {
            _moveController = gameObject.GetComponent<PlayerMoveController>();

            if (_moveController == null)
            {
                gameObject.AddComponent(typeof(PlayerMoveController));
                
                LoadMoveController();
            }
        }

        protected override bool TryGetDirection(out Vector2 direction)
        {
            return _moveController.TryGetDirection(out direction);
        }
    }
}