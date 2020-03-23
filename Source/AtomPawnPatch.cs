using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace Additional_Robots
{
    static class AtomPawnPatch
    {
        private static List<WorkGiver> nuclearWorkGivers = new List<WorkGiver>();

        static AtomPawnPatch()
        {
            List<WorkTypeDef> allDefsListForReading = DefDatabase<WorkTypeDef>.AllDefsListForReading;
            allDefsListForReading.ForEach(def =>
            {
                if (def.defName.Equals("NuclearWork"))
                {
                    def.workGiversByPriority.ForEach(workGiver =>
                    {
                        nuclearWorkGivers.Add(workGiver.Worker);
                    });
                    return;
                }
            });
        }

        public static bool GetWorkGivers(object __instance, ref List<WorkGiver> __result)
        {
            if (atom((Pawn)__instance))
            {
                __result = nuclearWorkGivers;
                return false;
            }

            return true;
        }

        public static void TickRare(object __instance)
        {
            Log.Error("Tick");
            Pawn bot = (Pawn)__instance;
            if (atom(bot))
            {
                bot.health.hediffSet.hediffs.ForEach(hediff => {
                    if (hediff.def.defName.Equals("RadiationMechanoid"))
                    {
                        hediff.Severity = 0;
                    }
                });
            }
        }

        private static bool atom(Pawn bot)
        {
            return bot.kindDef.defName.StartsWith("RPP_Bot_Atom");
        }
    }
}
