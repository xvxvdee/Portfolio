using mongoDBSettings.Models;

//Web App
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings")); // This line loads MongoDB settings from configuration
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

app.Run();