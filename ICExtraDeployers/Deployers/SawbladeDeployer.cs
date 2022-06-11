namespace ICExtraDeployers.Deployers
{
    public record SawbladeDeployer : Deployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.Sawblade;
        }
    }
}
