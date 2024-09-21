using CodeBase.Data;
using UnityEngine;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public Vector2 InitialBasketPosition { get; set;}
        [field: SerializeField] public Palette Palette { get; set; }
        [field: SerializeField] public float TimeLimit { get; set; }
    }
}
