﻿using MediatR;
using ProductApi.DatabaseContext;

namespace ProductApi.Services.Commands
{
    public class UpdateItemCmd : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public class UpdateItemCmdHandler : IRequestHandler<UpdateItemCmd, int>
        {
            private readonly IProductContext _context;
            public UpdateItemCmdHandler(IProductContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateItemCmd command, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(e => e.Id == command.Id).FirstOrDefault();
                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.Price = command.Price;
                    product.Name = command.Name;
                    product.Amount = command.Amount;
                    product.Description = command.Description;
                    await _context.SaveChanges();
                    return product.Id;
                }
            }
        }
    }
}
