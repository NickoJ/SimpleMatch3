using System;
using Klyukay.SimpleMatch3.Core.Systems;
using Leopotam.Ecs;

namespace Klyukay.SimpleMatch3.Core
{

    public class Match3Core : IDisposable
    {

        private readonly ICoreEventsReceiver _eventsReceiver;
        private readonly IMatch3Settings _settings;
        
        private EcsWorld _world;
        private Match3State _state;
        
        private EcsSystems _systems;

        private bool _disposed;
        
        public Match3Core(ICoreEventsReceiver eventsReceiver, IMatch3Settings settings)
        {
            _eventsReceiver = eventsReceiver;
            _settings = settings;
        }

        public void Initialize()
        {
            _world = new EcsWorld();
            _state = new Match3State(_settings);
            
            CreateWorldObserver(_world);

            _systems = new EcsSystems(_world)
                .Add(new FieldInitializeSystem(_world, _state, _eventsReceiver));
//                .Add(new StoneSwapSystem(_world, _settings))
//                .Add(new FallStoneSystem(_world, _settings))
//                .Add(new ExplodeComboSystem(_world, _settings))
//                .Add(new CreateNewStonesSystem(_world, _settings));
            
            _systems.Initialize();

            var sb = new System.Text.StringBuilder();
            for (int x = 0; x < _state.StoneField.GetLength(0); x++)
            {
                for (int y = 0; y < _state.StoneField.GetLength(1); y++)
                {
                    sb.Append((int)_state.StoneField[x, y].color).Append(' ');
                }

                sb.Append('\n');
            }
            UnityEngine.Debug.Log(sb.ToString());
            
            CreateSystemsObserver(_systems);
        }
        
        public void Dispose()
        {
            if (_disposed) return;

            _systems?.Dispose();
            _systems = null;
            
            _world?.Dispose();
            _world = null;
            
            _disposed = true;
        }
        
        #if UNITY_EDITOR

        private static void CreateWorldObserver(EcsWorld world)
        {
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
        }

        private static void CreateSystemsObserver(EcsSystems systems)
        {
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
        }
        
        #else

        private static void CreateWorldObserver(EcsWorld world) {}
        private static void CreateSystemsObserver(EcsSystems systems) {}

        #endif

    }

}