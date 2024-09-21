using CodeBase.Enums;
using CodeBase.SO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        Task Initialize();
        ShapeStaticData ForShape(ShapeTypeId shape);
        LevelStaticData ForLevel(string sceneKey);
        List<ShapeStaticData> GetAllShapes();
        List<GameDifficultyStaticData> GetGameDifficulties();
        GameDifficultyStaticData ForGameDifficult(GameDifficultyId id);
    }
}