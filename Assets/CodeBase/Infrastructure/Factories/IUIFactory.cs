using System.Threading.Tasks;
using CodeBase.UI.Elements;
using CodeBase.UI.Panels;
using CodeBase.UI.Menus;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IUIFactory
    {
        Task CreateUIRoot();
        Task<GameObject> CreateHud();
        Task<PopupMessage> CreatePopupMessage(Color color, string text);
        Task WarmUp();
        void CleanUp();
        Task<TimesUpPanel> CreateTimesUpPanel();
        Task<MainMenu> CreateMainMenu();
    }
}