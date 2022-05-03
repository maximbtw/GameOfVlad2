using Shared;
using UnityEngine;

namespace RigidbodyModels.MobModels.MobAngel
{
    public class MobAngelAudioController : AudioControllerBase
    {
        [SerializeField] private AudioClip shootClip;
        [SerializeField] private AudioClip playerEnterInVisibilityZoneClip;
        
        private MobAngel _model;

        protected override void Start()
        {
            base.Start();

            _model = GetComponent<MobAngel>();

            _model.WasShot += () => PlayOneShotIfNotNull(shootClip);
            _model.PlayerEnterInVisibilityDistance += () => PlayOneShotIfNotNull(playerEnterInVisibilityZoneClip);
        }
    }
}