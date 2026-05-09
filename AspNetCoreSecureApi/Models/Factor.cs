using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSecureApi.Models;

public class Factor
{
    public int Id { get; set; }
    public string Buyyer { get; set; }
    public DateTime DateTime { get; set; }

}
