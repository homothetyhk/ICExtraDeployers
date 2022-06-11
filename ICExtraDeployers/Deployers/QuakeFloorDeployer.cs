namespace ICExtraDeployers.Deployers
{
    public record QuakeFloorDeployer : Deployer
    {
        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.QuakeFloor;
        }
    }
}
