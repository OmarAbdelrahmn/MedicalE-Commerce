namespace Medical_E_Commerce.Contracts.Pharmacy;

public record PharmacyRequest
(
     string Name,
     string ImageURL,
     string PhoneNumbers,
     string WhatsUrl,
     string Location,
     string MapsLocation
    );
