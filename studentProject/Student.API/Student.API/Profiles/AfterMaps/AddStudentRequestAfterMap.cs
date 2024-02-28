using AutoMapper;
using Student.API.DomainModels;

namespace Student.API.Profiles.AfterMaps
{
	public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Student.API.DataModels.Student>
	{
		public void Process(AddStudentRequest source, DataModels.Student destination, ResolutionContext context)
		{
			destination.Id = Guid.NewGuid();
			destination.Address = new DataModels.Address()
			{
				Id = Guid.NewGuid(),
				PhysicalAddress = source.PhysicalAddress,
				PostalAddress = source.PostalAddress,
			};
		}
	}
}
