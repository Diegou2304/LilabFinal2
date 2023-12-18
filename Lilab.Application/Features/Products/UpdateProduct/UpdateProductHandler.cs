using Lilab.Domain;
using Lilab.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace LilabApplication.Features.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, IActionResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IHistoricTransactionRepository _historicTransactionRepository;

        public UpdateProductHandler(IProductRepository productRepository, IHistoricTransactionRepository historicTransactionRepository)
        {
            _productRepository = productRepository;
            _historicTransactionRepository = historicTransactionRepository;
        }

        public async Task<IActionResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var currentProduct = await _productRepository.GetProductById(request.ProductId);

            await _historicTransactionRepository.InsertProductHistoric(currentProduct);

            var newProduct = new Product
            {
                Id = request.ProductId,
                Stock = request.Stock,

            };

            var result = await _productRepository.UpdateProduct(newProduct);

            return result > 0 ? new OkResult() : new BadRequestResult();
        }
    }
}
