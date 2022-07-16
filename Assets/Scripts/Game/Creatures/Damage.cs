public class Damage
{

    public decimal Value { get; set; }
    public DamageType Type { get; set; }
    public RangeType Range { get; set; }

    public string AnimationType { get; set; }
    public float AnimationSpeed { get; set; }
    public AnimationOrientation AnimOrientation { get; set;}

    public Damage Copy()
    {
        return new Damage(Value, Type, Range, AnimationType, AnimationSpeed, AnimOrientation);
    }

    public Damage() { }

    public Damage(decimal dmg, DamageType type, RangeType range, string animType)
    {
        Value = dmg;
        Type = type;
        Range = range;
        AnimationType = animType;
        AnimationSpeed = 20;
        AnimOrientation = AnimationOrientation.Vertical;
    }

    public Damage(decimal dmg, DamageType type, RangeType range, string animType, float animSpeed) : this(dmg, type, range, animType)
    {
        AnimationSpeed = animSpeed;
    }

    public Damage(decimal dmg, DamageType type, RangeType range, string animType, AnimationOrientation orientation) : this (dmg, type, range, animType)
    {
        AnimOrientation = orientation;
    }

    public Damage(decimal dmg, DamageType type, RangeType range, string animType, float animSpeed, AnimationOrientation orientation) : this(dmg, type, range, animType, animSpeed)
    {
        AnimOrientation = orientation;
    }

    public override string ToString()
    {
        return "Dmg = " + Value + "; AnimationType =" + AnimationType + "; AnimationSpeed = " + AnimationSpeed;
    }
}