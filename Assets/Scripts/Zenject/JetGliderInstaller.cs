using DigitalRubyShared;
using UnityEngine;

namespace Zenject
{
    public class JetGliderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Bindings
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FingersJoystickScript>().FromComponentInHierarchy().AsTransient();
            //Container.Bind<LeanTouch>()
        
            SignalBusInstaller.Install(Container);
            //Signals
            //Container.DeclareSignal<JetGliderSignals>();
        }
    }
}