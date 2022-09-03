using Core;
using System.Reflection;
using SimpleInjector;

Container container = new SimpleInjector.Container();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Sets up the basic configuration that for integrating Simple Injector with
// ASP.NET Core by setting the DefaultScopedLifestyle, and setting up auto
// cross wiring.
builder.Services.AddSimpleInjector(container, options =>
{
    // AddAspNetCore() wraps web requests in a Simple Injector scope and
    // allows request-scoped framework services to be resolved.
    options.AddAspNetCore()

        // Ensure activation of a specific framework type to be created by
        // Simple Injector instead of the built-in configuration system.
        // All calls are optional. You can enable what you need. For instance,
        // ViewComponents, PageModels, and TagHelpers are not needed when you
        // build a Web API.
        .AddControllerActivation()
        .AddViewComponentActivation()
        .AddPageModelActivation()
        .AddTagHelperActivation();

    // Optionally, allow application components to depend on the non-generic
    // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
    // (Microsoft.Extensions.Localization) abstractions.
    options.AddLogging();
    //options.AddLocalization();


    // *** Registering validators dynamically 
    string validatorsDirectory =
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Validators");

    Directory.CreateDirectory(validatorsDirectory);

    var validatorAssemblies =
        from file in new DirectoryInfo(validatorsDirectory).GetFiles()
        where file.Extension.ToLower() == ".dll"
        select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));

    // Include IValidator interface assembly
    validatorAssemblies = validatorAssemblies.Append(typeof(IValidator<>).Assembly);
    // ***

    // Register all IValidator's
    options.Container.Collection.Register(typeof(IValidator<>), validatorAssemblies);

    // Register open-generic ValidatationProcessor
    options.Container.Register(typeof(IValidationAggregator<>), typeof(ValidationAggregator<>));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
