using DataLib.Model;

namespace DataLib.Business;

public class TestCategory : DataLib.Data.TestCategoryBase
{
    public List<TestCategoryDTO> GetTestCategories(
        int? pageSize = null,
        int? pageNumber = null,
        bool bRetrieveAllTestCategories = false
    )
    {
        return GetTestCategoriesBase(pageSize, pageNumber, bRetrieveAllTestCategories);
    }

    public long GetTestCategoriesCount()
    {
        return GetTestCategoryCountBase();
    }
}