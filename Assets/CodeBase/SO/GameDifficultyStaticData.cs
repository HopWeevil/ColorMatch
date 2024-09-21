using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "GameDifficulty", menuName = "Static Data/GameDifficulty")]
    public class GameDifficultyStaticData : ScriptableObject
    {
        [field: SerializeField] public GameDifficultyId Id { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public float ShapeFallSpeedMultiplayer { get; set; }
        [field: SerializeField] public float ShapeSpawnInterval { get; set; }
        [field: SerializeField] public float BasketColorChangeInterval { get; set; }
        [field: SerializeField] public float BasketColorTransitionDuration { get; set; }
        [field: SerializeField] public int ScoreIncreaseAmount { get; set; }
        [field: SerializeField] public int ScoreDecreaseAmount { get; set; }
    }
}
