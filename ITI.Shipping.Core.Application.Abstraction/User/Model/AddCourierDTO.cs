using ITI.Shipping.Core.Domin.Entities_Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.User.Model;
public record AddCourierDTO(
        string Email,
        string FullName,
        string PhoneNumber,
        string Address,
        string Password,
        int BranchId,
        DeductionTypes DeductionType,
        decimal DeductionCompanyFromOrder,
        List<SpecialCourierRegionDT0> SpecialCourierRegions
    );

public record SpecialCourierRegionDT0
{
    public int RegionId { get; set; }
    [JsonIgnore]
    public string CourierId { get; set; } = string.Empty;
}
