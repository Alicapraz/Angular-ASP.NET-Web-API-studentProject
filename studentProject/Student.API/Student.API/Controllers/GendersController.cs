using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.API.Repositories;

namespace Student.API.Controllers
{
	[ApiController]
	public class GendersController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IMapper mapper;
		public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
			this.studentRepository = studentRepository;
			this.mapper = mapper;
		}

        [HttpGet]
		[Route("[controller]")]
		public async Task<IActionResult> GetGenders()
		{
			var genderList = await studentRepository.GetGenders();

			if (genderList == null || !genderList.Any())
			{
				return NotFound();
			}

			return Ok(mapper.Map<List<Student.API.DomainModels.Gender>>(genderList));
		}
	}
}
