using MediatR;
using ProductApi.DatabaseContext;
using ProductApi.Entities.Products;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Services.Commands
{
    public class CreateItemCmd: IRequest<int>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public class CreateItemCmdHandler : IRequestHandler<CreateItemCmd, int>
        {
            private readonly IProductContext _context;
            public CreateItemCmdHandler(IProductContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateItemCmd command, CancellationToken cancellationToken)
            {

                if (_context.Products.Any(p=> p.Name == command.Name))
                {
                    return 0;
                }

                var product = new Product();

                product.Price = command.Price;

                product.Name = command.Name;

                product.Amount = command.Amount;

                product.Description = command.Description;

                product.Created = DateTime.Now;

                product.Updated = DateTime.Now;

                _context.Products.Add(product);

                await _context.SaveChanges();

                return product.Id;
            }
        }
    }
}
