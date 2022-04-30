using UnityEngine;

namespace Extras
{
    public class CameraMotionHandler : MonoBehaviour
    {
        [SerializeField] private RigidbodyModels.Player.Player player;

        private void Update()
        {
            Vector3 playerPosition = player.gameObject.transform.localPosition;

            transform.position = new Vector3(playerPosition.x, playerPosition.y, z: -100);
        }
    }
}