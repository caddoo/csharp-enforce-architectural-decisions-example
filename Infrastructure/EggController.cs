namespace Infrastructure;

using Application; 

public sealed class EggController: BaseController
{
    private readonly EggHandler _eggHandler;

    public EggController(EggHandler eggHandler)
    {
        _eggHandler = eggHandler;
    }

    public void LayEgg(int chickenId)
    {
        var command = new EggCommand(chickenId);
        _eggHandler.Handle(command);
    }
}