namespace FS.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmGenresController : ControllerBase
{
    private readonly IDbService _db;

    public FilmGenresController(IDbService db) => _db = db;
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            List<FilmGenreDTO>? filmgenres = await _db.GetAsync<FilmGenre, FilmGenreDTO>();

            return Results.Ok(JsonUtility.HandleLoops(filmgenres));
        }
        catch
        {
        }
        return Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> Post(FilmGenreCreateDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();
            var filmgenre = await _db.AddAsync<FilmGenre, FilmGenreCreateDTO>(dto);
            var success = await _db.SaveChangesAsync();

            if (!success) return Results.BadRequest();

            return Results.Ok();
        }
        catch { }

        return Results.NotFound();
    }



    [HttpDelete]
    public async Task<IResult> Delete(FilmGenreCreateDTO dto)
    {
        try
        {
            var exist = _db.Delete<FilmGenre, FilmGenreCreateDTO>(dto);

            if (!exist) return Results.NotFound();

            var success = await _db.SaveChangesAsync();

            if (!success) return Results.NotFound();

            return Results.NoContent();
        }
        catch
        {

        }
        return Results.BadRequest();
        
    }
}

