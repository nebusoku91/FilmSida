﻿namespace FS.Membership.Database.Entities;

public class Genre : IEntity
{
    public Genre()
    {
        Films = new HashSet<Film>();
    }

    public int Id { get; set; }
    [MaxLength(50), Required] public string Name { get; set; } = string.Empty;
    public virtual ICollection<Film> Films { get; set; }
}
