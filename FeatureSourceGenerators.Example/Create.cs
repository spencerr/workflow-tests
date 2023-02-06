using FluentValidation;

namespace FeatureSourceGenerators.Example;
[GenerateMediator]
public static partial class Create
{
    public sealed partial record Command(
        string CustomerName,
        string Id
    )
    {
        private static void AddValidation(AbstractValidator<Command> v)
        {
            v.RuleFor(x => x.CustomerName).NotEmpty();
            v.RuleFor(x => x.Id).NotEmpty();
        }
    }

    private static async Task CommandHandler(
        Command command,
        ApplicationDbContext context,
        ILogger logger
    )
    {

    }
}

public class ApplicationDbContext
{

}

public interface ILogger
{

}