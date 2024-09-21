using CodeBase.Enums;
using CodeBase.SO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string ShapeDataLabel = "ShapeData";
        private const string LevelDataLabel = "LevelData";
        private const string DifficultyDataLabel = "DifficultyData";

        private Dictionary<ShapeTypeId, ShapeStaticData> _shapes;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<GameDifficultyId, GameDifficultyStaticData> _difficultyLevelsData;

        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async Task Initialize()
        {
            _levels = (await _assetProvider.LoadAll<LevelStaticData>(LevelDataLabel)).ToDictionary(x => x.SceneName, x => x);
            _shapes = (await _assetProvider.LoadAll<ShapeStaticData>(ShapeDataLabel)).ToDictionary(x => x.TypeId, x => x);
            _difficultyLevelsData = (await _assetProvider.LoadAll<GameDifficultyStaticData>(DifficultyDataLabel)).ToDictionary(x => x.Id, x => x);
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public ShapeStaticData ForShape(ShapeTypeId shape)
        {
            if (_shapes.TryGetValue(shape, out ShapeStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public GameDifficultyStaticData ForGameDifficult(GameDifficultyId id)
        {
            if (_difficultyLevelsData.TryGetValue(id, out GameDifficultyStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public List<ShapeStaticData> GetAllShapes()
        {
            return _shapes.Values.ToList();
        }

        public List<GameDifficultyStaticData> GetGameDifficulties() 
        { 
            return _difficultyLevelsData.Values.ToList();
        }
    }
}