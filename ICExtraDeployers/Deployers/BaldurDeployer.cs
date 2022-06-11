namespace ICExtraDeployers.Deployers
{
    public record BaldurDeployer : EnemyDeployer
    {
        public bool FacingRight { get; init; }

        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.Baldur;
        }
        public override GameObject Deploy()
        {
            GameObject obj = base.Deploy();
            obj.LocateMyFSM("Blocker Control")
                .GetState("Can Roller?")
                .RemoveActionsOfType<HutongGames.PlayMaker.Actions.IntCompare>();
            if (!FacingRight)
            {
                obj.transform.localScale = new(1f, 1f, 1f);
                obj.LocateMyFSM("Blocker Control")
                .FsmVariables
                .FindFsmBool("Facing Right").Value = false;
            }

            return obj;
        }
    }

}
