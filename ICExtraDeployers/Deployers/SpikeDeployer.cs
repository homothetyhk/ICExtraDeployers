namespace ICExtraDeployers.Deployers
{
    public record SpikeDeployer : RotatableDeployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.Spike;
        }
    }
}
