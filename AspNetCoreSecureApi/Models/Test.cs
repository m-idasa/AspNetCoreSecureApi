using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSecureApi.Models;

public class Test
{
    public int Id { get; set; }
    public string? Text { get; set; }
}
