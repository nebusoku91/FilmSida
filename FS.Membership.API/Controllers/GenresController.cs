using FS.Membership.Database.Entities;

namespace FS.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IDbService _db;

    public GenresController(IDbService db) => _db = db;
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            List<GenreDTO>? genre = await _db.GetAsync<Genre, GenreDTO>();

            return Results.Ok(genre);
        }
        catch
        {
        }
        return Results.NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            var genre = await _db.SingleAsync<Genre, GenreDTO>(c => c.Id.Equals(id));
            if (genre is null) return Results.NotFound();
            return Results.Ok(JsonUtility.HandleLoops(genre));
        }
        catch
        {
        }
        return Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] GenreDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();

            var genre = await _db.AddAsync<Genre, GenreDTO>(dto);

            var success = await _db.SaveChangesAsync();

            if (!success) return Results.BadRequest();

            return Results.Created(_db.GetURI<Genre>(genre), genre);
        }
        catch
        {
        }
        return Results.BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] GenreDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();
            if (!id.Equals(dto.Id)) return Results.BadRequest();

            var exist = await _db.AnyAsync<Genre>(g => g.Id.Equals(dto.Id));
            if (!exist) return Results.NotFound();

            _db.Update<Genre, GenreDTO>(dto.Id, dto);

            var success = await _db.SaveChangesAsync();
            if (!success) return Results.BadRequest();

            return Results.NoContent();
        }
        catch
        {
        }

        return Results.BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var success = await _db.DeleteAsync<Genre>(id);

            if (!success) return Results.NotFound();

            success = await _db.SaveChangesAsync();

            if (!success) return Results.BadRequest();

            return Results.NoContent();
        }
        catch
        {
        }
        return Results.BadRequest();



    }
}

