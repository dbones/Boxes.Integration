namespace Boxes.Integration.Setup.Registrations
{
    /// <summary>
    /// The scope to apply to an instace of a type.
    /// </summary>
    public abstract class LifeStyle
    {
    }


    public class Transient : LifeStyle
    {
    }

    public class Singleton : LifeStyle
    {
    }


}