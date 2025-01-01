using DataLib.Business;
using DataLib.Model.Admin;

namespace Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var user = new AdminUserDTO();
            var testCategory = new TestCategory();
            Console.WriteLine($"Categories Count: {testCategory.GetTestCategoriesCount()}");
            //var categories = testCategory.GetTestCategories(3,20);
            //categories.ForEach(c => Console.WriteLine(c.Name));
        }
    }
}