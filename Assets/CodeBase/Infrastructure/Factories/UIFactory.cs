using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Menus;
using CodeBase.UI.Panels;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;
        private Canvas _uiRoot;

        public UIFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.TimesUpPanel);       
        }

        public void CleanUp()
        {
            _assets.CleanUp();
        }

        public async Task<PopupMessage> CreatePopupMessage(Color color, string text)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.PopupMessage);
            PopupMessage message = Object.Instantiate(prefab, _uiRoot.transform).GetComponent<PopupMessage>();
            message.SetColor(color);
            message.SetText(text);
            return message;
        }

        public async Task CreateUIRoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.UIRootPath);
            _uiRoot = Object.Instantiate(prefab).GetComponent<Canvas>();
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.HudPath);
            GameObject hud = Object.Instantiate(prefab);
            _container.InjectGameObject(hud);
            return hud;
        }

        public async Task<TimesUpPanel> CreateTimesUpPanel()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.TimesUpPanel);
            GameObject window = _container.InstantiatePrefab(prefab, _uiRoot.transform);
            return window.GetComponent<TimesUpPanel>();
        }

        public async Task<MainMenu> CreateMainMenu()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.MainMenu);
            GameObject menu = _container.InstantiatePrefab(prefab, _uiRoot.transform);
            return menu.GetComponent<MainMenu>();
        }
    }
}