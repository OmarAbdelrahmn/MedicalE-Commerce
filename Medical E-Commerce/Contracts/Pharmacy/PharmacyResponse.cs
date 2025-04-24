using Medical_E_Commerce.Contracts.Item;

namespace Medical_E_Commerce.Contracts.Pharmacy;

public record PharmacyResponse
(
     int Id,
     string Name,
     string ImageURL,
     string PhoneNumbers,
     string WhatsUrl,
     string Location,
     string MapsLocation,
     IList<ItemResponse> Items
    );

public record PhResponse
(
     int Id,
     string Name,
     string ImageURL,
     string PhoneNumbers,
     string WhatsUrl,
     string Location,
     string MapsLocation
    );
