// Automatically Generated

using System.Diagnostics.CodeAnalysis;
using HKLib.hk2018;

namespace HKLib.Reflection.hk2018;

internal class hknpConstraintCollisionFilterData : HavokData<hknpConstraintCollisionFilter> 
{
    public hknpConstraintCollisionFilterData(HavokType type, hknpConstraintCollisionFilter instance) : base(type, instance) {}

    public override bool TryGetField<TGet>(string fieldName, [MaybeNull] out TGet value)
    {
        value = default;
        switch (fieldName)
        {
            case "m_propertyBag":
            case "propertyBag":
            {
                if (instance.m_propertyBag is not TGet castValue) return false;
                value = castValue;
                return true;
            }
            case "m_type":
            case "type":
            {
                if (instance.m_type is TGet castValue)
                {
                    value = castValue;
                    return true;
                }
                if ((byte)instance.m_type is TGet byteValue)
                {
                    value = byteValue;
                    return true;
                }
                return false;
            }
            case "m_childFilter":
            case "childFilter":
            {
                if (instance.m_childFilter is null)
                {
                    return true;
                }
                if (instance.m_childFilter is TGet castValue)
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
            case "m_propertyBag":
            case "propertyBag":
            {
                if (value is not hkPropertyBag castValue) return false;
                instance.m_propertyBag = castValue;
                return true;
            }
            case "m_type":
            case "type":
            {
                if (value is hknpCollisionFilter.Type castValue)
                {
                    instance.m_type = castValue;
                    return true;
                }
                if (value is byte byteValue)
                {
                    instance.m_type = (hknpCollisionFilter.Type)byteValue;
                    return true;
                }
                return false;
            }
            case "m_childFilter":
            case "childFilter":
            {
                if (value is null)
                {
                    instance.m_childFilter = default;
                    return true;
                }
                if (value is hknpCollisionFilter castValue)
                {
                    instance.m_childFilter = castValue;
                    return true;
                }
                return false;
            }
            default:
            return false;
        }
    }

}
