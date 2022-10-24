using School.Controller;
using School.Services;
using SchoolDomain;
using System.Net.Http.Headers;

namespace School
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            StudentsController studentsController = new StudentsController();
            await studentsController.GetStudets();
        }
    }
}