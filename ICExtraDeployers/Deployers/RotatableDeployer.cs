namespace ICExtraDeployers.Deployers
{
    public abstract record RotatableDeployer : Deployer
    {
        public float Rotation { get; init; }
        public override GameObject Deploy()
        {
            GameObject obj = base.Deploy();
            obj.transform.SetRotation2D(Rotation);
            return obj;
        }
    }
}
