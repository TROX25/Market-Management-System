using Build_Market_Management_System.Models;

namespace Build_Market_Management_System.Models
{
    public static class CategoriesRepository
    {
        private static List<Category> _categories = new List<Category>()
        {
            new Category { ID = 1, Name = "Beverage", Description = "Beverage" },
            new Category { ID = 2, Name = "Bakery", Description = "Bakery" },
            new Category { ID = 3, Name = "Meat", Description = "Meat" }
        };

        public static void AddCategory(Category category)
        {
            var maxId = _categories.Max(x => x.ID);
            category.ID = maxId + 1;
            _categories.Add(category);
        }

        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(int ID)
        {
            var category = _categories.FirstOrDefault(x => x.ID == ID);
            if (category != null)
            {
                return new Category
                {
                    ID = category.ID,
                    Name = category.Name,
                    Description = category.Description,
                };
            }

            return null;
        }

        public static void UpdateCategory(int ID, Category category)
        {
            if (ID != category.ID) return;

            var categoryToUpdate = _categories.FirstOrDefault(x => x.ID == ID);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        public static void DeleteCategory(int ID)
        {
            var category = _categories.FirstOrDefault(x => x.ID == ID);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }

    }
}