namespace ICExtraDeployers.Deployers
{
    public record ShadowGateDeployer : Deployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.ShadowGate;
        }
    }
}
