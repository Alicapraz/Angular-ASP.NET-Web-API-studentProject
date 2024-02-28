namespace Student.API.Repositories
{
	public interface IStudentRepository
	{
		Task<List<Student.API.DataModels.Student>> GetStudents();
		Task<Student.API.DataModels.Student> GetStudent(Guid studentId);
		Task<List<Student.API.DataModels.Gender>> GetGenders();
		Task<bool> Exist(Guid studentId);
		Task<Student.API.DataModels.Student> UpdateStudent(Guid studentId, Student.API.DataModels.Student request);
		Task<Student.API.DataModels.Student> DeleteStudent(Guid studentId);
		Task<Student.API.DataModels.Student> AddStudent(Student.API.DataModels.Student request);
		Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);

	}
}
