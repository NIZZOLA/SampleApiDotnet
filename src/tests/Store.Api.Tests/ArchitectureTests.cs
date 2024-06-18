using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;

namespace Store.Api.Tests;
/*
public class SplitArchitecture
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
        new ArchLoader()
            .LoadAssemblies(
                // We load the different Assemblies to run the Check
                typeof(Goat.Controllers.GoatController).Assembly,
                typeof(Goat.UseCases.FeedGoat).Assembly,
                typeof(Goat.Domain.Goat).Assembly,
                typeof(Goat.Repositories.GoatRepository).Assembly
            )
            .Build();

    private static GivenTypesConjunction TypesIn(string @namespace) =>
        ArchRuleDefinition.Types().That().ResideInNamespace(@namespace, true);

    private static GivenTypesConjunctionWithDescription Controllers() =>
        TypesIn("Controllers").As("Interface Adapters");

    private static GivenTypesConjunctionWithDescription UseCases() =>
        TypesIn("UseCases").As("Application Business Rules");

    private static GivenTypesConjunctionWithDescription Frameworks_Drivers() =>
        TypesIn("Repositories").As("Framework & Drivers");

    private static GivenTypesConjunctionWithDescription Domain() =>
        TypesIn("Domain").As("Enterprise Business Rules");

    [Fact(DisplayName = "Lower layers can not depend on outer layers")]
    public void CheckRule() =>
        Domain()
            .Should()
            .OnlyDependOn(Domain())
            .And(
                UseCases().Should()
                    .NotDependOnAny(Controllers()).AndShould()
                    .NotDependOnAny(Frameworks_Drivers())
            )
            .And(
                Controllers().Should()
                    .NotDependOnAny(Frameworks_Drivers())
            ).Check(Architecture);
}
*/

public class ArchitectureTests
{

    private static readonly ArchUnitNET.Domain.Architecture Architecture =
            new ArchLoader()
                .LoadAssemblies(
                    // We load the different Assemblies to run the Check
                    typeof(Endpoints.StorePostRequestModelEndpoints).Assembly,
                    typeof(Application.AppServices.StoreAppService).Assembly,
                    typeof(Domain.Domain.BaseModel).Assembly,
                    typeof(Infra.Data.Repositories.StoreRepository).Assembly
                )
                .Build();

    private static GivenTypesConjunction TypesIn(string @namespace) =>
        ArchRuleDefinition.Types().That().ResideInNamespace(@namespace, true);

    private static GivenTypesConjunctionWithDescription Endpoints() =>
        TypesIn("Endpoints").As("Minimal Api Endpoints");

    private static GivenTypesConjunctionWithDescription AppServices() =>
        TypesIn("AppServices").As("Application Business Rules");

    private static GivenTypesConjunctionWithDescription Services() =>
        TypesIn("Services").As("Business Services");

    private static GivenTypesConjunctionWithDescription Repositories() =>
            TypesIn("Repositories").As("Repositories");

    private static GivenTypesConjunctionWithDescription Domain() =>
        TypesIn("Domain").As("Enterprise Business Rules");

    [Fact(DisplayName = "Domain can not depend on outer layers")]
    public void CheckDomainRule() =>
        Domain()
            .Should()
            .OnlyDependOn(Domain()
            ).Check(Architecture);

    [Fact(DisplayName = "Repository can not depend on outer layers")]
    public void CheckServiceRule() =>
        Repositories()
            .Should()
            .DependOnAny(Domain()).AndShould()
            .NotDependOnAny(Endpoints()).AndShould()
            .NotDependOnAny(AppServices()).AndShould()
            .NotDependOnAny(Services()
            ).Check(Architecture);

    [Fact(DisplayName = "Repository can not depend on outer layers")]
    public void CheckRepositoryRule() =>
        Repositories()
            .Should()
            .DependOnAny(Domain()).AndShould()
            .NotDependOnAny(Endpoints()).AndShould()
            .NotDependOnAny(AppServices()).AndShould()
            .NotDependOnAny(Services()
            ).Check(Architecture);

    [Fact(DisplayName = "Lower layers can not depend on outer layers")]
    public void CheckRule() =>
        Domain()
            .Should()
            .OnlyDependOn(Domain())
            .And(
                Endpoints().Should()
                    .DependOnAny(AppServices()).AndShould()
                    .NotDependOnAny(Services()).AndShould()
                    .NotDependOnAny(Repositories())
            )
            .And(
                AppServices().Should()
                    .DependOnAny(Services()).AndShould()
                    .NotDependOnAny(Repositories())
            ).Check(Architecture);
}
