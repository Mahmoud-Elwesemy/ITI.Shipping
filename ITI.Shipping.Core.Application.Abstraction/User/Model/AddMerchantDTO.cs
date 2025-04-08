using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.User.Model;
public record AddMerchantDTO(
        string Email,
        string Password,
        string FullName,
        string PhoneNumber,
        string Address,
        int BranchId,
        int RegionId,
        int CityId,
        string StoreName,
        List<SpecialCityCostDT0>? SpecialCityCosts
    );

public record SpecialCityCostDT0
{
    public decimal Price { get; set; }
    public int CitySettingId { get; set; }
    [JsonIgnore]
    public string MerchantId { get; set; } = string.Empty;
}
