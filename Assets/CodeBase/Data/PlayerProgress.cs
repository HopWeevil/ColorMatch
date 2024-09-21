using CodeBase.Enums;
using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int MaxScore;
        public GameDifficultyId LastPlayerDifficulty;
    }
}