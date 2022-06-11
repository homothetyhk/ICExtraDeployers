namespace ICExtraDeployers.Deployers
{
    public record TollGate : Deployer
    {
        public float GateX { get; init; }
        public float GateY { get; init; }
        public int Cost { get; init; }

        public override GameObject Instantiate()
        {
            return ICEDPreloader.Instance.TollGateMachine;
        }

        public override GameObject Deploy()
        {
            GameObject tgm = base.Deploy();
            DeployGate();
            SetTollCost(tgm, Cost);
            AddCheckDarknessLevel(tgm);
            ResetFSMs(tgm);
            return tgm;
        }

        public virtual GameObject DeployGate()
        {
            GameObject gate = ICEDPreloader.Instance.TollGate;
            gate.transform.position = new(GateX, GateY);
            gate.SetActive(true);
            return gate;
        }

        public static void SetTollCost(GameObject tollMachine, int cost)
        {
            FsmState getPrice = tollMachine.LocateMyFSM("Toll Machine").GetState("Get Price");
            getPrice.ClearActions();
            tollMachine.LocateMyFSM("Toll Machine").FsmVariables.GetFsmInt("Toll Cost").Value = cost;
        }

        public static void ResetFSMs(GameObject tollMachine)
        {
            tollMachine.GetComponent<BoxCollider2D>().enabled = true;
            tollMachine.GetComponent<tk2dBaseSprite>().color = Color.white;
            GameObject pm = new();
            pm.name = "Prompt Marker";
            pm.transform.parent = tollMachine.transform;
            pm.transform.position = tollMachine.transform.position + new Vector3(0, 2f);
            tollMachine.LocateMyFSM("Toll Machine").FsmVariables.FindFsmGameObject("Prompt Marker").Value = pm;
        }

        public static void AddCheckDarknessLevel(GameObject tollMachine)
        {
            FsmState check = tollMachine.LocateMyFSM("Disable if No Lantern").GetState("Check");
            check.ClearActions();
            check.AddFirstAction(new Lambda(() =>
            {
                if (!PlayerData.instance.hasLantern && HeroController.instance.vignetteFSM.FsmVariables.GetFsmInt("Darkness Level").Value == 2)
                {
                    check.Fsm.Event("DISABLE");
                }
            }));
        }
    }
}
