using _GameLogic.Gameplay.Camera.Systems;
using _GameLogic.Gameplay.Galaxy.Generation.Systems;
using _GameLogic.Gameplay.Galaxy.StarSystems.Systems;
using _GameLogic.Gameplay.Galaxy.Systems;
using _GameLogic.Gameplay.Time.Systems;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;

namespace _GameLogic
{
    public class GalaxyMapInitializer : Initializer
    {
        private SystemsGroup _systemsGroup;
        
        public override void OnAwake()
        {
            _systemsGroup = World.CreateSystemsGroup();
            
            _systemsGroup.AddInitializer(new TimeInitializingSystem());
            _systemsGroup.AddSystem(new TimeProcessingSystem());
            _systemsGroup.AddSystem(new TimeLapseModeSwitchingSystem());

            _systemsGroup.AddSystem(new GameCameraSwitchingRequestProcessingSystem());
            _systemsGroup.AddSystem(new GameCameraManagementSystem());

            _systemsGroup.AddSystem(new GalaxyGenerationRequestProcessingSystem());
            _systemsGroup.AddSystem(new StarSystemOpeningSystem());
            _systemsGroup.AddSystem(new StarSystemClosingSystem());

            _systemsGroup.AddSystem(new StarSystemSubjectLabelsHandlingSystem());
            
            World.AddSystemsGroup(4, _systemsGroup);
        }

        public override void Dispose()
        {
            base.Dispose();
            World.RemoveSystemsGroup(_systemsGroup);
        }
    }
}