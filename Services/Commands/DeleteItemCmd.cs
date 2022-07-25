using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.DatabaseContext;
using ProductApi.Utils.CommonConstants;

namespace ProductApi.Services.Commands
{
    public class DeleteItemCmd : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteItemCmdHandler : IRequestHandler<DeleteItemCmd, int>
        {
            private readonly IProductContext _context;
            public DeleteItemCmdHandler(IProductContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteItemCmd command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (product == null) return CommonConstants.CustomStatusCode.ProductNotFound;
                _context.Products.Remove(product);
                await _context.SaveChanges();
                return product.Id;
            }
        }
    }
}
