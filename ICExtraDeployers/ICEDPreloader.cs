namespace ICExtraDeployers
{
    public class ICEDPreloader : ItemChanger.Internal.Preloaders.Preloader
    {
        public static ICEDPreloader Instance { get; } = new();

        public override IEnumerable<(string, string)> GetPreloadNames()
        {
            yield return (SceneNames.Mines_33, "Toll Gate Machine");
            yield return (SceneNames.Mines_33, "Toll Gate");
            yield return (SceneNames.Fungus3_05, "Gate Switch");
            yield return (SceneNames.Fungus3_05, "Metal Gate v2");
            yield return (SceneNames.Fungus1_28, "Buzzer (3)");
            yield return (SceneNames.Fungus1_28, "Battle Music/Blocker 1");
            yield return (SceneNames.Fungus3_44, "shadow_gate");
            yield return (SceneNames.Crossroads_52, "Quake Floor");
            yield return (SceneNames.White_Palace_18, "saw_collection/wp_saw (6)");
            yield return (SceneNames.Crossroads_25, "Cave Spikes tile (5)");
        }

        public override void SavePreloads(Dictionary<string, Dictionary<string, GameObject>> objectsByScene)
        {
            _tollGateMachine = objectsByScene[SceneNames.Mines_33]["Toll Gate Machine"];
            UObject.DontDestroyOnLoad(_tollGateMachine);
            _tollGate = objectsByScene[SceneNames.Mines_33]["Toll Gate"];
            UObject.DontDestroyOnLoad(_tollGate);
            _gateSwitch = objectsByScene[SceneNames.Fungus3_05]["Gate Switch"];
            UObject.DontDestroyOnLoad(_gateSwitch);
            _switchGate = objectsByScene[SceneNames.Fungus3_05]["Metal Gate v2"];
            UObject.DontDestroyOnLoad(_switchGate);
            _vengefly = objectsByScene[SceneNames.Fungus1_28]["Buzzer (3)"];
            UObject.DontDestroyOnLoad(_vengefly);
            _baldur = objectsByScene[SceneNames.Fungus1_28]["Battle Music/Blocker 1"];
            UObject.DontDestroyOnLoad(_baldur);
            _shadowGate = objectsByScene[SceneNames.Fungus3_44]["shadow_gate"];
            UObject.DontDestroyOnLoad(_shadowGate);
            _quakeFloor = objectsByScene[SceneNames.Crossroads_52]["Quake Floor"];
            UObject.DontDestroyOnLoad(_quakeFloor);
            _sawblade = objectsByScene[SceneNames.White_Palace_18]["saw_collection/wp_saw (6)"];
            UObject.DontDestroyOnLoad(_sawblade);
            _spike = objectsByScene[SceneNames.Crossroads_25]["Cave Spikes tile (5)"];
            UObject.DontDestroyOnLoad(_spike);
        }

        private GameObject _tollGateMachine;
        private GameObject _tollGate;
        private GameObject _gateSwitch;
        private GameObject _switchGate;
        private GameObject _vengefly;
        private GameObject _baldur;
        private GameObject _shadowGate;
        private GameObject _quakeFloor;
        private GameObject _sawblade;
        private GameObject _spike;

        public GameObject TollGateMachine => UObject.Instantiate(_tollGateMachine);
        public GameObject TollGate => UObject.Instantiate(_tollGate);
        public GameObject GateSwitch => UObject.Instantiate(_gateSwitch);
        public GameObject SwitchGate => UObject.Instantiate(_switchGate);
        public GameObject Vengefly => UObject.Instantiate(_vengefly);
        public GameObject Baldur => UObject.Instantiate(_baldur);
        public GameObject ShadowGate => UObject.Instantiate(_shadowGate);
        public GameObject QuakeFloor => UObject.Instantiate(_quakeFloor);
        public GameObject Sawblade => UObject.Instantiate(_sawblade);
        public GameObject Spike => UObject.Instantiate(_spike);
    }
}
