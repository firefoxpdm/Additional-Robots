using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;

namespace Additional_Robots
{
    public class Controller : Mod
    {
        public Controller(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony("com.firefox.additionalrobots");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());
            harmony.Patch(
                original: AccessTools.Method(
                    type: AccessTools.TypeByName("X2_AIRobot"),
                    name: "GetWorkGivers"
                    ),
                prefix: new HarmonyMethod(
                    methodType: typeof(AtomPawnPatch),
                    methodName: "GetWorkGivers"
                    )
                );

            harmony.Patch(
               original: AccessTools.Method(
                   type: typeof(Pawn),
                   name: "TickRare"
                   ),
               prefix: new HarmonyMethod(
                   methodType: typeof(AtomPawnPatch),
                   methodName: "TickRare"
                   )
               );
        }
    }
}
