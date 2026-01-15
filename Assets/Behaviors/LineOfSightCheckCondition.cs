using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[Condition(
    name: "Line Of Sight Check",
    story: "Check [Target] with Line Of Sight [Detector]",
    category: "Conditions",
    id: "LOS_CHECK_001"
)]
public partial class LineOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<LineOfSightDetector> Detector;

    public override bool IsTrue()
    {
        return Detector.Value != null &&
               Detector.Value.HasLineOfSight(Target.Value);
    }
}
