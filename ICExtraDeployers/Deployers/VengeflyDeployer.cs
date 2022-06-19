namespace ICExtraDeployers.Deployers
{
    public record VengeflyDeployer : EnemyDeployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.Vengefly;
        }
    }
}
