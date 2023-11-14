using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.UseCases
{
    public interface IDiscountsApplication
    {
        Task<Response<bool>> Create(DiscountDto discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(DiscountDto discountDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default);
        Task<Response<DiscountDto>> Get(int id, CancellationToken cancellationToken = default);
        Task<Response<List<DiscountDto>>> GetAll(CancellationToken cancellationToken = default);
        Task<ResponsePagination<IEnumerable<DiscountDto>>> GetAllWithPagination(int pageNumber, int pageSize);
    }
}
