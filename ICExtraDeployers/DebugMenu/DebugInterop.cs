using ICExtraDeployers.Deployers;
using MonoMod.ModInterop;

namespace ICExtraDeployers.DebugMenu
{
    internal static class DebugInterop
    {
        internal static List<Deployer> unsavedDeployers = new();
        internal static Vector2 pos1;
        internal static Vector2 pos2;
        private static bool loaded;

        internal static Dictionary<Type, Vector2> pos1Offsets = new()
        {
            [typeof(BaldurDeployer)] = new(0f, 1f),
            [typeof(TollGateDeployer)] = new(0f, 0.85f),
            [typeof(SwitchGateDeployer)] = new(0f, 0.3f),
            [typeof(SpikeDeployer)] = new(0, -0.55f),
            [typeof(QuakeFloorDeployer)] = new(0, -2.3f),

        };

        internal static Dictionary<Type, Vector2> pos2Offsets = new()
        {
            [typeof(TollGateDeployer)] = new(0f, 0.7f),
            [typeof(SwitchGateDeployer)] = new(0f, 1f),
        };

        public const string CATEGORY = "Extra Deployers";
        public const string PANEL = "Extra Deployer Info";

        public static void Setup()
        {
            if (DebugModImport.AddActionToKeyBindList is null || loaded) return;
            
            CreateSimpleInfoPanel(PANEL, 200f);
            AddInfoToSimplePanel(PANEL, "Pos 1", () => pos1.ToString());
            AddInfoToSimplePanel(PANEL, "Pos 2", () => pos2.ToString());
            AddInfoToSimplePanel(PANEL, "Unsaved deployers", () => unsavedDeployers.Count.ToString());
            AddActionToKeyBindList(Save, "Save list to IC settings", CATEGORY);
            AddActionToKeyBindList(Serialize, "Serialize list to IC directory", CATEGORY); 
            AddActionToKeyBindList(Clear, "Clear list", CATEGORY);
            
            AddActionToKeyBindList(() =>
            {
                pos1 = HeroController.instance.transform.position;
            }, "Set pos 1", CATEGORY);
            AddActionToKeyBindList(() =>
            {
                pos2 = HeroController.instance.transform.position;
            }, "Set pos 2", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new BaldurDeployer
            {
                FacingRight = false,
                HP = 5,
            }), "Left Baldur", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new BaldurDeployer
            {
                FacingRight = true,
                HP = 5,
            }), "Right Baldur", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new VengeflyDeployer
            {
                HP = 5,
            }), "Vengefly", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new QuakeFloorDeployer
            {
            }), "Quake Floor", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new ShadowGateDeployer
            {
            }), "Shadow Gate", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SawbladeDeployer
            {
            }), "Sawblade", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SpikeDeployer
            {
                Rotation = 0f,
            }), "Ceiling Spike", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SpikeDeployer
            {
                Rotation = 90f,
            }), "Left Spike", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SpikeDeployer
            {
                Rotation = -90f,
            }), "Right Spike", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SpikeDeployer
            {
                Rotation = 180f
            }), "Floor Spike", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new TollGateDeployer
            {
                Cost = Cost.NewGeoCost(50),
            }), "Toll Gate", CATEGORY);
            AddActionToKeyBindList(() => DeployNow(new SwitchGateDeployer
            {
            }), "Switch Gate", CATEGORY);
            

            loaded = true;
        }

        public static void DeployNow(Deployer d)
        {
            pos1Offsets.TryGetValue(d.GetType(), out Vector2 pos1Offset);

            d = d with
            {
                SceneName = GameManager.instance.sceneName,
                X = pos1.x + pos1Offset.x,
                Y = pos1.y + pos1Offset.y,
            };
            if (d is TollGateDeployer tgd)
            {
                pos2Offsets.TryGetValue(d.GetType(), out Vector2 pos2Offset);
                d = tgd with { GateX = pos2.x + pos2Offset.x, GateY = pos2.y + pos2Offset.y };
            }
            else if (d is SwitchGateDeployer sgd)
            {
                pos2Offsets.TryGetValue(d.GetType(), out Vector2 pos2Offset);
                d = sgd with { GateX = pos2.x + pos2Offset.x, GateY = pos2.y + pos2Offset.y };
            }

            d.OnSceneChange(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            unsavedDeployers.Add(d);
        }

        public static void Save()
        {
            ItemChangerMod.CreateSettingsProfile(false);
            foreach (Deployer d in unsavedDeployers) ItemChangerMod.AddDeployer(d);
            Clear();
        }
        public static void Serialize()
        {
            if (unsavedDeployers.Count == 0) return;
            Finder.Serialize(DateTime.Now.ToString("yyyy-M-dd--HH-mm-ss") + " - " + "extraDeployers.json", unsavedDeployers);
        }

        public static void Clear()
        {
            unsavedDeployers.Clear();
        }


        [ModImportName("DebugMod")]
        private static class DebugModImport
        {
            public static Action<Action, string, string> AddActionToKeyBindList = null;
            public static Action<string, float> CreateSimpleInfoPanel = null;
            public static Action<string, string, Func<string>> AddInfoToSimplePanel = null;

            static DebugModImport()
            {
                typeof(DebugModImport).ModInterop();
            }
        }

        public static void AddActionToKeyBindList(Action method, string name, string category)
        {
            DebugModImport.AddActionToKeyBindList?.Invoke(method, name, category);
        }
        public static void CreateSimpleInfoPanel(string Name, float sep)
        {
            DebugModImport.CreateSimpleInfoPanel?.Invoke(Name, sep);
        }
        public static void AddInfoToSimplePanel(string Name, string label, Func<string> textFunc)
        {
            DebugModImport.AddInfoToSimplePanel?.Invoke(Name, label, textFunc);
        }
    }
}
