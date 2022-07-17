using HutongGames.PlayMaker.Actions;
using ItemChanger.Util;

namespace ICExtraDeployers.Deployers
{
    public record TollGateDeployer : Deployer
    {
        public float GateX { get; init; }
        public float GateY { get; init; }
        public Cost Cost { get; init; }

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

        public static void SetTollCost(GameObject tollMachine, Cost cost)
        {
            PlayMakerFSM fsm = tollMachine.LocateMyFSM("Toll Machine");
            FsmState getPrice = fsm.GetState("Get Price");
            getPrice.ClearActions();
            FsmState sendText = fsm.GetState("Send Text");
            sendText.Actions = new FsmStateAction[]
            {
                new Lambda(OpenYNDialogue)
            };
            
            void OpenYNDialogue()
            {
                if (cost is GeoCost gc)
                {
                    string text = Language.Language.Get("TOLLBOOTH_GATE", "Prompts");
                    YNUtil.OpenYNDialogue(tollMachine, text, gc.amount);
                }
                else if (cost is not null)
                {
                    string text = cost.GetCostText();
                    YNUtil.OpenYNDialogue(tollMachine, text, cost.CanPay());
                }
                else
                {
                    fsm.SendEvent("YES");
                }
            }
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
                if (!PlayerData.instance.GetBool(nameof(PlayerData.hasLantern)) && HeroController.instance.vignetteFSM.FsmVariables.GetFsmInt("Darkness Level").Value == 2)
                {
                    check.Fsm.Event("DISABLE");
                }
            }));
        }
    }
}
