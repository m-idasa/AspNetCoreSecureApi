using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSecureApi.Models;

public class Shopping
{
    public int Id { get; set; }

    
    public int ItemId { get; set; }

    public int FactorId {  get; set; }

    [Required]  
    public double Number {  get; set; }
}
