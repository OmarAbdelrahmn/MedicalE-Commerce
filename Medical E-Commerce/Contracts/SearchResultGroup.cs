namespace Medical_E_Commerce.Contracts;
public class SearchResultGroup
{
    public List<Entities.Pharmacy> Pharmacies { get; set; } = [];
    public List<Entities.Item> Items { get; set; } = [];
}
