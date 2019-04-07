using Leopotam.Ecs;

namespace Klyukay.SimpleMatch3.Core.Systems
{
    internal class StoneSwapSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        private readonly IMatch3Settings _settings;

        public StoneSwapSystem(EcsWorld world, IMatch3Settings settings)
        {
            _world = world;
            _settings = settings;
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
        
    }
    
}