using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.DatabaseContext;
using ProductApi.Entities.Products;

namespace ProductApi.Services.Queries
{
    public class GetAllQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<Product>>
        {
            private readonly IProductContext _context;
            public GetAllProductsQueryHandler(IProductContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }
        }
    }
}
