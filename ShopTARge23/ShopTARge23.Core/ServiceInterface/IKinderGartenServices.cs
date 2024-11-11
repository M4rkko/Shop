using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface IKinderGartenServices
    {
        Task<KinderGarten> Create(KinderGartenDto dto);
        Task<KinderGarten> GetAsync(Guid id);
        Task<KinderGarten> Update(KinderGartenDto dto);
        Task<KinderGarten> Delete(Guid id);

    }
}
