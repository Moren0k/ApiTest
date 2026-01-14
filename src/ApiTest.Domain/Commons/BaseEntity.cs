using System.ComponentModel.DataAnnotations;

namespace ApiTest.Domain.Commons;

public class BaseEntity
{
    [Key]public Guid Id { get; protected set; }
}