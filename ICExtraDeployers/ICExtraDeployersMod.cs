using Modding;

namespace ICExtraDeployers
{
    public class ICExtraDeployersMod : Mod
    {
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            ICEDPreloader.Instance.SavePreloads(preloadedObjects);
            DebugMenu.DebugInterop.Setup();
        }

        public override List<(string, string)> GetPreloadNames()
        {
            return new(ICEDPreloader.Instance.GetPreloadNames());
        }

        public static string Version { get; }
        public override string GetVersion()
        {
            return Version;
        }

        static ICExtraDeployersMod() 
        {
            Version v = typeof(ICExtraDeployersMod).Assembly.GetName().Version;
            Version = $"{v.Major}.{v.Minor}.{v.Build}";
        }
    }
}
