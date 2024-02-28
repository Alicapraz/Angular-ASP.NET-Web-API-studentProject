using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Student.API.DataModels;
using Student.API.DomainModels;
using Student.API.Repositories;

namespace Student.API.Controllers
{
	[ApiController]
	public class StudentsController : Controller
	{
		private readonly IStudentRepository studentRepository;
		private readonly IImageRepository IImageRepository;
		private readonly IMapper mapper;
		public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository IImageRepository)
        {
            this.studentRepository = studentRepository;
			this.mapper = mapper;
			this.IImageRepository = IImageRepository;
        }

		[HttpGet]
		[Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
		{
			var students = await studentRepository.GetStudents();

			
			return Ok(mapper.Map<List<Student.API.DomainModels.Student>>(students));
		}

		[HttpGet]
		[Route("[controller]/{studentId:guid}"), ActionName("GetStudent")]
		public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
		{
			var student = await studentRepository.GetStudent(studentId);

			if(student == null)
			{
				return NotFound();
			}


			return Ok(mapper.Map<Student.API.DomainModels.Student>(student));
		}

		[HttpPut]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
		{
			if(await studentRepository.Exist(studentId))
			{
				var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
				if(updatedStudent != null)
				{
					return Ok(mapper.Map<Student.API.DomainModels.Student>(updatedStudent));
				}
			}
			return NotFound();
		}

		[HttpDelete]
		[Route("[controller]/{studentId:guid}")]
		public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
		{
			if (await studentRepository.Exist(studentId))
			{
				var student = await studentRepository.DeleteStudent(studentId);
				if (student != null)
				{
					return Ok(mapper.Map<Student.API.DomainModels.Student>(student));
				}
			}
			return NotFound();
		}

		[HttpPost]
		[Route("[controller]/Add")]
		public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
		{
			var student = await studentRepository.AddStudent(mapper.Map<DataModels.Student>(request));
			return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id}, mapper.Map<Student.API.DomainModels.Student>(student));
		}

		[HttpPost]
		[Route("[controller]/{studentId:guid}/upload-image")]
		public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
		{
			var validExtensions = new List<string>
			{
				".jpeg",
				".png",
				".gif",
				".jpg"
			};

			if(profileImage != null && profileImage.Length > 0)
			{
				var extension = Path.GetExtension(profileImage.FileName);
				if(validExtensions.Contains(extension))
				{
					if(await studentRepository.Exist(studentId)) 
					{
						var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
						var fileImagePath = await IImageRepository.Upload(profileImage, fileName);
						if (await studentRepository.UpdateProfileImage(studentId, fileImagePath))
						{
							return Ok(fileImagePath);
						}
						return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
					}
				}
				return BadRequest("This is not a valid image format");
			}

			return NotFound();
		}
	}
}
