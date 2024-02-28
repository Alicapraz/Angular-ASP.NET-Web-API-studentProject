using AutoMapper;
using Student.API.DomainModels;
using Student.API.Profiles.AfterMaps;

namespace Student.API.Profiles
{
	public class AutoMapperProfiles : Profile
	{
        public AutoMapperProfiles()
        {
            CreateMap<Student.API.DataModels.Student, Student.API.DomainModels.Student>().ReverseMap();
			CreateMap<Student.API.DataModels.Gender, Student.API.DomainModels.Gender>().ReverseMap();
			CreateMap<Student.API.DataModels.Address, Student.API.DomainModels.Address>().ReverseMap();
			CreateMap<UpdateStudentRequest, Student.API.DataModels.Student>().AfterMap<UpdateStudentRequestAfterMap>();
			CreateMap<AddStudentRequest, Student.API.DataModels.Student>().AfterMap<AddStudentRequestAfterMap>();
		}
	}
}
