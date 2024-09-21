using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class Palette
    {
        [field: SerializeField] public Color[] Colors { get; private set; }
    }
}