namespace FS.Membership.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorsController : ControllerBase
{
    private readonly IDbService _db;
    public DirectorsController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            List<DirectorDTO>? director = await _db.GetAsync<Director, DirectorDTO>();

            return Results.Ok(director);
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
            _db.Include<Film>();
            var director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(id));
            if (director is null) return Results.NotFound();
            Results.Ok(JsonUtility.HandleLoops(director));
        }
        catch
        {
        }
        return Results.NotFound();
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] DirectorDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();

            var director = await _db.AddAsync<Director, DirectorDTO>(dto);

            var success = await _db.SaveChangesAsync();

            if (!success) return Results.BadRequest();

            return Results.Created(_db.GetURI<Director>(director), director);
        }
        catch
        {
        }
        return Results.BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
    {
        try
        {
            if (dto == null) return Results.BadRequest();
            if (!id.Equals(dto.Id)) return Results.BadRequest();

            var exist = await _db.AnyAsync<Director>(d => d.Id.Equals(dto.Id));
            if (!exist) return Results.NotFound();

            _db.Update<Director, DirectorDTO>(dto.Id, dto);

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
            var success = await _db.DeleteAsync<Director>(id);

            if (!success) return Results.NotFound("Director not found.");

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
