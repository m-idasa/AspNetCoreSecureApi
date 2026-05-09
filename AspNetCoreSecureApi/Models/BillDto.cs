namespace AspNetCoreSecureApi.Models;

class BillDto
{
    public int FactorId {  get; set; }
    public Double TotalCost {  get; set; }

    public BillDto(int FactorId, Double TotalCost) { 
        this.FactorId = FactorId;
        this.TotalCost = TotalCost;
    }
}
