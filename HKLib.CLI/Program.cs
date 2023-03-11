// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using HKLib.hk2018;
using HKLib.Serialization.hk2018.Binary;

namespace HKLib.CLI;

public static class Program
{
    public static void Main()
    {
        HavokBinarySerializer serializer = new();
        //serializer.LoadCompendium(@"D:\Elden Ring Unpacked\map\m10\m10_00_00_00\f10_00_00_00-hkxbhd\m10_00_00_00\f10_00_00_00_000700.hkx-out");
        IHavokObject obj =
            serializer.Read(
                @"D:\Elden Ring Unpacked\map\m10\m10_00_00_00\f10_00_00_00-hkxbhd\m10_00_00_00\f10_00_00_00_000700.hkx");

        hkRootLevelContainer rlc = (hkRootLevelContainer)obj;
        hkbBehaviorGraph behaviorGraph = (hkbBehaviorGraph)rlc.m_namedVariants[0].m_variant!;
        hkbStateMachine rootSm = (hkbStateMachine)behaviorGraph.m_rootGenerator!;
        Dictionary<int, CustomManualSelectorGenerator> cmsgs = new();
        FindCMSGS(rootSm, cmsgs);
        CustomManualSelectorGenerator r1Cmsg = cmsgs[30000];
        CustomManualSelectorGenerator r12Cmsg = cmsgs[30010];
        hkbClipGenerator referenceClip = (hkbClipGenerator)r1Cmsg.m_generators[0]!;
        hkbClipGenerator newClipGenerator = new()
        {
            m_animationName = "a677_030000",
            m_animationInternalId = short.MaxValue,
            m_cropEndAmountLocalTime = referenceClip.m_cropEndAmountLocalTime,
            m_cropStartAmountLocalTime = referenceClip.m_cropStartAmountLocalTime,
            m_enforcedDuration = referenceClip.m_enforcedDuration,
            m_flags = referenceClip.m_flags,
            m_mode = referenceClip.m_mode,
            m_name = "a677_030000",
            m_playbackSpeed = referenceClip.m_playbackSpeed,
            m_triggers = referenceClip.m_triggers,
            m_userPartitionMask = referenceClip.m_userPartitionMask,
            m_userControlledTimeFraction = referenceClip.m_userControlledTimeFraction
        };
        r1Cmsg.m_generators.Add(newClipGenerator);

        Stopwatch sw = Stopwatch.StartNew();
        serializer.Write(obj, @"C:\Users\Admin\RiderProjects\Havok\c0000-out.hkx");
        sw.Stop();

        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    public static void FindCMSGS(hkbGenerator? generator, Dictionary<int, CustomManualSelectorGenerator> cmsgs)
    {
        if (generator is null) return;
        if (generator is CustomManualSelectorGenerator cmsg) cmsgs.TryAdd(cmsg.m_animId, cmsg);
        else if (generator is hkbStateMachine stateMachine)
        {
            foreach (hkbStateMachine.StateInfo? stateInfo in stateMachine.m_states)
            {
                FindCMSGS(stateInfo?.m_generator, cmsgs);
            }
        }
        else if (generator is hkbModifierGenerator modifierGenerator)
        {
            FindCMSGS(modifierGenerator.m_generator, cmsgs);
        }
        else if (generator is hkbManualSelectorGenerator msg)
        {
            foreach (hkbGenerator? childGenerator in msg.m_generators)
            {
                FindCMSGS(childGenerator, cmsgs);
            }
        }
        else if (generator is hkbScriptGenerator scriptGenerator)
        {
            FindCMSGS(scriptGenerator.m_child, cmsgs);
        }
        else if (generator is hkbLayerGenerator layerGenerator)
        {
            foreach (hkbLayer? hkbLayer in layerGenerator.m_layers)
            {
                FindCMSGS(hkbLayer!.m_generator, cmsgs);
            }
        }
        else if (generator is hkbBlenderGenerator blenderGenerator)
        {
            foreach (hkbBlenderGeneratorChild? child in blenderGenerator.m_children)
            {
                FindCMSGS(child?.m_generator, cmsgs);
            }
        }
    }
}