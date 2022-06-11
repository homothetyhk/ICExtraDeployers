namespace ICExtraDeployers.Deployers
{
    public record Vengefly : EnemyDeployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.Vengefly;
        }
    }
}
