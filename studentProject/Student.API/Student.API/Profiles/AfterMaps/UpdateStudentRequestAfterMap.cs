using AutoMapper;
using Student.API.DomainModels;

namespace Student.API.Profiles.AfterMaps
{
	public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, Student.API.DataModels.Student>
	{
		public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
		{
			destination.Address = new DataModels.Address()
			{
				PhysicalAddress = source.PhysicalAddress,
				PostalAddress = source.PostalAddress,
			};
		}
	}
}
