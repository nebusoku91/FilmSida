var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices();
ConfigureAutoMapper();

// Configure the HTTP request pipeline.
ConfigureMiddleware();

void ConfigureMiddleware()
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("CorsAllAccessPolicy");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

void ConfigureServices()
{
    builder.Services.AddCors(policy =>
    {
        policy.AddPolicy("CorsAllAccessPolicy", opt =>
            opt.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
        );
    });

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<FSContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("FSConnection")));

    builder.Services.AddScoped<IDbService, DbService>();
}

void ConfigureAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Film, FilmDTO>().ReverseMap();

        cfg.CreateMap<FilmCreateDTO, Film>()
        .ForMember(d => d.Director, s => s.Ignore())
        .ForMember(d => d.Genres, s => s.Ignore())
        .ForMember(d => d.SimilarFilms, s => s.Ignore());

        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();

        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

        cfg.CreateMap<FilmGenre, FilmGenreCreateDTO>().ReverseMap();

        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();

        cfg.CreateMap<SimilarFilms, SimilarFilmsDTO>().ReverseMap();

        cfg.CreateMap<SimilarFilms, SimilarFilmsCreateDTO>().ReverseMap();

    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}