using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Helpers
{
    public static class InterimMeasureOptions
    {
        const string CommonFireAlarm = "Common fire alarm";
        const string WakingWatch = "Waking watch";
        const string EvacuationManagement = "Evacuation management";
        const string SimultaneousEvacuationStrategy = "Simultaneous evacuation strategy";
        const string FireHeatSmokeDetection = "Fire, heat, smoke detection";
        const string FireSurpressionSystem = "Fire suppression system";
        const string Other = "Other";

        public static string GetOptionQuestion(EInterimMeasuresType type)
        {
            switch (type)
            {

                case EInterimMeasuresType.CommonFireAlarm:
                    return CommonFireAlarm;
                case EInterimMeasuresType.WakingWatch:
                    return WakingWatch;
                case EInterimMeasuresType.EvacuationManagement:
                    return EvacuationManagement;
                case EInterimMeasuresType.SimultaneousEvacuationStrategy:
                    return SimultaneousEvacuationStrategy;
                case EInterimMeasuresType.FireHeatSmokeDetection:
                    return FireHeatSmokeDetection;
                case EInterimMeasuresType.FireSupressionSystem:
                    return FireSurpressionSystem;
                case EInterimMeasuresType.Other:
                    return Other;
                default: return "Unknown";
            }
        }
    }
}
