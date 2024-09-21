using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "ShapeStaticData", menuName = "Static Data/Shape")]
    public class ShapeStaticData : ScriptableObject
    {
        [field: SerializeField] public ShapeTypeId TypeId { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public float FallingSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}