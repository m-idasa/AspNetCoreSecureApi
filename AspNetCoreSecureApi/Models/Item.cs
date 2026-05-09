using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSecureApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }

       
    }
}
