using VerifyXunit;

namespace FeatureSourceGenerators.IntegrationTests;

[UsesVerify]
public class ValidatorSourceGeneratorTests
{
    [Fact]
    public Task RecordGeneratesValidation()
    {
        var source = @"
using FeatureSourceGenerators.Tests;

public partial record FooDto(
    int Id,
    string Name
)
{
    private static void AddValidation(AbstractValidator<FooDto> v)
    {
        v.RuleFor(e => e.Id).NotEmpty();
        v.RuleFor(e => e.Name).NotEmpty();
    }
}";

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task ClassGeneratesValidation()
    {
        var source = @"
using FeatureSourceGenerators.Tests;

public partial class FooDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    private static void AddValidation(AbstractValidator<FooDto> v)
    {
        v.RuleFor(e => e.Id).NotEmpty();
        v.RuleFor(e => e.Name).NotEmpty();
    }
}";

        return TestHelper.Verify(source);
    }

    [Fact]
    public Task MediatorGenerator()
    {
        var source = @"
using FeatureSourceGenerators.Tests;
using System.Threading.Tasks;

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
            v.RuleFor(x => x.CustomerName).NotEmpty().MaximumLength(CustomerEntity.CustomerNameMaxLength);
            v.RuleFor(x => x.Id).NotEmpty().MaximumLength(CustomerEntity.IdMaxLength).LowerCase();
        }
    }

    private static async Task CommandHandler(
        Command command,
        ApplicationDbContext context,
        ILogger logger
    )
    {
        if (await context.Customers.AnyAsync(c => c.Id == command.Id))
            throw new InvalidOperationException($@""Customer """"{command.Id}"""" already exists."");

        var customer = new CustomerEntity
        {
            CustomerName = command.CustomerName,
            Id = command.Id,
        };

        context.Customers.Add(customer);
        await context.SaveChangesAsync();

        using (logger.AddProperties((""@NewCustomerInformation"", command)))
            logger.LogInformation(""New customer created"");
    }
}";

        return TestHelper.Verify(source);
    }
}
