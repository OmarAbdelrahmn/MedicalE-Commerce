using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Contracts;
public class SearchResultGroup
{
    public List<PhResponse> Pharmacies { get; set; } = [];
    public List<ItemResponse> Items { get; set; } = [];
}
