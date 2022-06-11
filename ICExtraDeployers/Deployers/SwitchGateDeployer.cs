namespace ICExtraDeployers.Deployers
{
    public record SwitchGateDeployer : Deployer
    {
        public float GateX { get; init; }
        public float GateY { get; init; }

        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.GateSwitch;
        }

        public override GameObject Deploy()
        {
            GameObject gateSwitch = base.Deploy();
            GameObject gate = DeployGate();
            FixGateSwitchAnimation(gateSwitch);
            SetGateSwitchTarget(gateSwitch, gate);
            return gateSwitch;
        }

        public virtual GameObject DeployGate()
        {
            GameObject gate = ICEDPreloader.Instance.SwitchGate;
            gate.transform.position = new(GateX, GateY);
            gate.SetActive(true);
            return gate;
        }

        public static void SetGateSwitchTarget(GameObject gateSwitch, GameObject gate)
        {
            gateSwitch.LocateMyFSM("Switch Control").FsmVariables.GetFsmGameObject("Target").Value = gate;
        }

        public static void FixGateSwitchAnimation(GameObject gateSwitch)
        {
            gateSwitch.LocateMyFSM("Switch Control")
                .GetState("Open")
                .GetFirstActionOfType<HutongGames.PlayMaker.Actions.SendEventByName>()
                .sendEvent = "OPEN";
        }
    }

}
