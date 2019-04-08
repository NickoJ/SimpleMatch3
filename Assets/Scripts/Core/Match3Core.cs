using System;
using Klyukay.SimpleMatch3.Core.Components;
using Klyukay.SimpleMatch3.Core.Systems;
using Klyukay.SimpleMatch3.Core.Utils;
using Leopotam.Ecs;
using Unity.Mathematics;

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

        public bool IsBusy { get; private set; }

        public void Initialize()
        {
            _world = new EcsWorld();
            _state = new Match3State(_settings);
            
            CreateWorldObserver(_world);

            _systems = new EcsSystems(_world)
                .Add(new StoneSwapSystem(_world, _eventsReceiver, _state))
                .Add(new FallStoneSystem(_eventsReceiver, _state))
                .Add(new CreateNewStonesSystem(_world, _eventsReceiver, _state))
                .Add(new MarkDestroyingComboSystem(_world, _state))
                .Add(new DestroyStoneSystem(_world, _eventsReceiver, _state))
                .Add(new FieldInitializeSystem(_world, _eventsReceiver, _state));
            
            _systems.Initialize();

            CreateSystemsObserver(_systems);
        }
        
        public void Swap(int2 lhvPos, int2 rhvPos)
        {
            var field = _state.StoneField;
            if (!field.InRange(lhvPos) || !field.InRange(rhvPos)) return;
            
            var lhv = _state.StoneField.Get(lhvPos);
            var rhv = _state.StoneField.Get(rhvPos);
            if (lhv == null || rhv == null || lhv == rhv) return;

            _world.AddComponent<Swapping>(lhv.eid);
            _world.AddComponent<Swapping>(rhv.eid);
            _world.ProcessDelayedUpdates();
        }

        public void Tick()
        {
            _state.TickProcessed = false;
            
            _systems.Run();
            _world.RemoveOneFrameComponents();

            IsBusy = _state.TickProcessed;
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