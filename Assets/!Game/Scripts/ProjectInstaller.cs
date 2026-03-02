using Game.Contracts;
using UnityEngine;
using Zenject;
using Game.InputSystem;
using Game.LocalizationSystem;
using Game.Raycast;
using Game.SaveSystem;
using UnityEngine.EventSystems;

namespace Game
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CubeData _cubeData;
        [SerializeField] private HUD _hud;
        [SerializeField] private Cube _cubeTemplate;
        [SerializeField] private Camera _camera;
        [SerializeField] private Dictionary _dictionary;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LanguageCodes _language = LanguageCodes.ru;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SaveManager>().AsSingle().NonLazy();
            Container.Bind<IInput>().FromMethod(_ => Application.isMobilePlatform ? new Mobile() : new PC()).AsSingle();
            Container.Bind<Raycaster>().AsSingle();
            Container.Bind<Factory>().AsSingle();
            Container.Bind<Tower>().AsSingle();
            Container.Bind<CameraBounds>().AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<IGameConfigProvider>().FromInstance(_cubeData).AsSingle();
            Container.Bind<HUD>().FromInstance(_hud).AsSingle();
            Container.Bind<EventSystem>().FromInstance(_eventSystem).AsSingle();
            Container.Bind<Cube>().FromInstance(_cubeTemplate);
            Container.Bind<LanguageCodes>().FromInstance(_language);
            Container.Bind<Dictionary>().FromInstance(_dictionary);
            Container.Bind<LayerMask>().FromInstance(_layerMask).AsSingle();
            Container.BindInterfacesTo<InputUpdater>().AsSingle();
            Container.BindInterfacesAndSelfTo<Interaction>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoader>().AsSingle();
            Container.Bind<ILocalization>().To<Localization>().AsSingle();
        }
    }
}