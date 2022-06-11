namespace ICExtraDeployers.Deployers
{
    public abstract record EnemyDeployer : Deployer
    {
        public int HP { get; init; }
        public override GameObject Deploy()
        {
            GameObject obj = base.Deploy();
            obj.GetComponent<HealthManager>().hp = HP;
            return obj;
        }

    }

}
