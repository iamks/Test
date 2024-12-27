using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test.Asc.Contract.PostalIndex;
using Test.Asc.WebApi.PostalIndex.Dto;

namespace Test.Asc.WebApi.PostalIndex
{
    [ApiController]
    [Route("api/postal-index")]
    public class PostalIndexController : ControllerBase
    {
        private const string PostalCodeDoesNotExists = "No response retreived for the requested Postal Code";

        private readonly IPostalIndexService postalIndexService;
        private readonly IMapper mapper;

        public PostalIndexController(
            IPostalIndexService postalIndexService, 
            IMapper mapper)
        {
            this.postalIndexService = postalIndexService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("get-postal-index-by-postal-code")]
        public async Task<ActionResult<GetPostalIndexByPostalCodeResponseDto>> GetPostalIndexByCode(string postalCode)
        {
            if(postalCode == null)
            {
                return BadRequest();
            }

            var responseDto = new GetPostalIndexByPostalCodeResponseDto();

            var responseSo = await this.postalIndexService.GetPostalIndexByPostalCode(postalCode);
            if (responseSo != null)
            {
                responseDto.postalIndex = this.mapper.Map<PostalIndexDto>(responseSo);
            }
            else
            {
                responseDto.ErrorMessage = PostalCodeDoesNotExists;
            }

            return Ok(responseDto);
        }
    }
}
