using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities.Common;
using ProductApi.Entities.Products;
using ProductApi.Services.Commands;
using ProductApi.Services.Queries;
using ProductApi.Utils.CommonConstants;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = CommonConstants.UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemCmd command)
        {
            try
            {
                var result = await Mediator.Send(command);

                if (result == CommonConstants.CustomStatusCode.ProductNameDuplicated)
                {
                    return BadRequest(new ResponseHanlder { Status = CommonConstants.StatusType.BadRequest, Message = CommonConstants.CustomErrorMessage.NameDuplicated });
                }

                return Ok(new ResponseHanlder { Status = CommonConstants.StatusType.Success, Message = "Product Created with Id = " + result });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHanlder { Status = CommonConstants.StatusType.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHanlder { Status = CommonConstants.StatusType.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHanlder { Status = CommonConstants.StatusType.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = CommonConstants.UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await Mediator.Send(new DeleteItemCmd { Id = id });

                if (result == CommonConstants.CustomStatusCode.ProductNotFound)
                {
                    return BadRequest(new ResponseHanlder { Status = CommonConstants.StatusType.BadRequest, Message = CommonConstants.CustomErrorMessage.ProductNotExist });
                }

                return Ok(new ResponseHanlder { Status = CommonConstants.StatusType.Success, Message = "Product with Id = " + result + " has been deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHanlder { Status = CommonConstants.StatusType.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Update Existing Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = CommonConstants.UserRoles.Admin)]
        public async Task<IActionResult> Update(int id, UpdateItemCmd command)
        {
            if (id != command.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseHanlder { Status = CommonConstants.StatusType.BadRequest, Message = CommonConstants.CustomErrorMessage.InvalidId });
            }

            try
            {
                var result = await Mediator.Send(command);

                if (result == CommonConstants.CustomStatusCode.ProductNotFound)
                {
                    return BadRequest(new ResponseHanlder { Status = CommonConstants.StatusType.BadRequest, Message = CommonConstants.CustomErrorMessage.ProductNotExist });
                }
                if (result == CommonConstants.CustomStatusCode.ProductNameDuplicated)
                {
                    return BadRequest(new ResponseHanlder { Status = CommonConstants.StatusType.BadRequest, Message = CommonConstants.CustomErrorMessage.NameDuplicated });
                }
                return Ok(new ResponseHanlder { Status = CommonConstants.StatusType.Success, Message = "Product with Id = " + result + " has been deleted." });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHanlder { Status = CommonConstants.StatusType.InternalServerError, Message = ex.Message });
            }
        }

    }
}
