using Shared.Authentication;

namespace Shared.Orders;

public record OrderRequest(string BasketId,AddressDto Address,int DeliveryMethodId);