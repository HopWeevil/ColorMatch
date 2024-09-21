using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Logic.GameBasket;
using CodeBase.Logic.Spawner;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory
    {
        Task WarmUp();
        void CleanUp();
        Task<Shape> CreateRandomShape(Vector2 position, float speedModifier, Palette palette);
        Task<ShapeSpawner> CreateShapeSpawner(Palette palette);
        Task<Basket> CreateBasket(Vector3 at, Palette palette);
    }
}