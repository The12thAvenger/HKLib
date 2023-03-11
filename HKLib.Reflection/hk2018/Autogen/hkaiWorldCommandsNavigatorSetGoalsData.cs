// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;
using HKLib.hk2018.hkaiWorldCommands;

namespace HKLib.Reflection.hk2018;

internal class hkaiWorldCommandsNavigatorSetGoalsData : HavokData<NavigatorSetGoals> 
{
    public hkaiWorldCommandsNavigatorSetGoalsData(HavokType type, NavigatorSetGoals instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_sizePaddedTo16":
            case "sizePaddedTo16":
            {
                if (instance.m_sizePaddedTo16 is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_filterBits":
            case "filterBits":
            {
                if (instance.m_filterBits is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_primaryType":
            case "primaryType":
            {
                if (instance.m_primaryType is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                if ((byte)instance.m_primaryType is TGet byteValue)
                {
                    value = byteValue;
                    return true;
                }
                return false;
            }
            case "m_secondaryType":
            case "secondaryType":
            {
                if (instance.m_secondaryType is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_worldIndex":
            case "worldIndex":
            {
                if (instance.m_worldIndex is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_goals":
            case "goals":
            {
                if (instance.m_goals is null)
                {
                    return true;
                }
                if (instance.m_goals is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                return false;
            }
            case "m_relativeSections":
            case "relativeSections":
            {
                if (instance.m_relativeSections is null)
                {
                    return true;
                }
                if (instance.m_relativeSections is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                return false;
            }
            default:
            return false;
        }
    }

    public override bool TrySetField<TSet>(string fieldName, TSet value)
    {
        switch (fieldName)
        {
            case "m_sizePaddedTo16":
            case "sizePaddedTo16":
            {
                if (value is not ushort castValue) return false;
                instance.m_sizePaddedTo16 = castValue;
                return true;
            }
            case "m_filterBits":
            case "filterBits":
            {
                if (value is not byte castValue) return false;
                instance.m_filterBits = castValue;
                return true;
            }
            case "m_primaryType":
            case "primaryType":
            {
                if (value is hkCommand.PrimaryType castValue)
                {
                    instance.m_primaryType = castValue;
                    return true;
                }
                if (value is byte byteValue)
                {
                    instance.m_primaryType = (hkCommand.PrimaryType)byteValue;
                    return true;
                }
                return false;
            }
            case "m_secondaryType":
            case "secondaryType":
            {
                if (value is not ushort castValue) return false;
                instance.m_secondaryType = castValue;
                return true;
            }
            case "m_worldIndex":
            case "worldIndex":
            {
                if (value is not int castValue) return false;
                instance.m_worldIndex = castValue;
                return true;
            }
            case "m_goals":
            case "goals":
            {
                if (value is null)
                {
                    instance.m_goals = default;
                    return true;
                }
                if (value is hkaiReferencedArray<Vector4> castValue)
                {
                    instance.m_goals = castValue;
                    return true;
                }
                return false;
            }
            case "m_relativeSections":
            case "relativeSections":
            {
                if (value is null)
                {
                    instance.m_relativeSections = default;
                    return true;
                }
                if (value is hkaiReferencedArray<int> castValue)
                {
                    instance.m_relativeSections = castValue;
                    return true;
                }
                return false;
            }
            default:
            return false;
        }
    }

}
