using Plugins.DataStore.InMemory;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;
using Plugins.DataStore.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Build_Market_Management_System.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("QA"))
{
    builder.WebHost.UseStaticWebAssets();
}

    builder.Services.AddControllersWithViews();


if (builder.Environment.IsEnvironment("QA"))
{
    builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
    builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
}
else
{
    builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
    builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
    builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
}



builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();
builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
builder.Services.AddTransient<ISellProductsUseCase, SellProductsUseCase>();
builder.Services.AddTransient<IDecreaseProductQuantityUseCase, DecreaseProductQuantityUseCase>();

builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();
builder.Services.AddTransient<IGetTodayTransactionUseCase, GetTodayTransactionUseCase>();

builder.Services.AddDbContext<MarketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// Musi byæ po Routing
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
