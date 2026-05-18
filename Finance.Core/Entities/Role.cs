using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Entities;

[Table("Roles")]
public class Role
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<User> Users { get; set; } = new List<User>();
}