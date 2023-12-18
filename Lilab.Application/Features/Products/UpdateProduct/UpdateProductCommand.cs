using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LilabApplication.Features.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest<IActionResult>
    {
        public int Stock { get; set; }
        public int ProductId { get; set; }
    }
}