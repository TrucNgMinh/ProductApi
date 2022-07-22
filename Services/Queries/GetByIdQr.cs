using MediatR;
using ProductApi.DatabaseContext;
using ProductApi.Entities.Products;

namespace ProductApi.Services.Queries
{
    public class GetByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Product>
        {
            private readonly IProductContext _context;
            public GetByIdQueryHandler(IProductContext context)
            {
                _context = context;
            }
            public async Task<Product> Handle(GetByIdQuery query, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == query.Id).FirstOrDefault();
                if (product == null) return null;
                return product;
            }
        }
    }
}
