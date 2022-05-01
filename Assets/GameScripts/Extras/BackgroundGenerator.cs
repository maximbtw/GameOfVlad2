using UnityEngine;

namespace Extras
{
    public class BackgroundGenerator : MonoBehaviour
    {
        [SerializeField] 
        [Header("1024 x 1024")]
        private Sprite backgroundTitleSprite;

        [SerializeField] private Vector2 firstTitlePosition;
        [SerializeField] private Vector2 lastTitlePosition;

        private const int StepBetweenTitle = 10;

        private void Awake()
        {
            GenerateBackground();
        }

        private void GenerateBackground()
        {
            for (float x = firstTitlePosition.x; x <= lastTitlePosition.x; x += StepBetweenTitle)
            {
                for (float y = firstTitlePosition.y; y <= lastTitlePosition.y; y += StepBetweenTitle)
                {
                    CreateTitle(new Vector3(x, y, z:0));
                }
            }
        }

        private void CreateTitle(Vector3 position)
        {
            var title = new GameObject();
            
            var titleTransform = title.AddComponent<RectTransform>();
            var titleSpriteRenderer = title.AddComponent<SpriteRenderer>();

            titleTransform.position = position;
            titleTransform.SetParent(this.transform);
            titleSpriteRenderer.sprite = backgroundTitleSprite;
            titleSpriteRenderer.sortingOrder = -100;

            title.name = $"TitleBackground_{position.x}_{position.y}";
        }
    }
}