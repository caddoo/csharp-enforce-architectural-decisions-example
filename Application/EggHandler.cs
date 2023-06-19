
using Domain;

namespace Application;

public sealed class EggHandler
{
    public void Handle(EggCommand eggCommand)
    {
        var chicken = new Chicken(eggCommand.ChickenId);
        chicken.LayEgg();
    }
}