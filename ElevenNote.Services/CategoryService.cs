using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ElevenNote.Data;
using ElevenNote.Models.CatFolder;
using Microsoft.AspNet.Identity;


namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCat(CatCreate cat)
        {
            var entity = new Category()
            {
                Title = cat.Title
            };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CatListItem> GetCats()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Categories
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CatListItem
                                {
                                    CategoryId = e.CategoryId,
                                    Title = e.Title
                                }
                        );
                return query.ToArray();
            }
        }
    }
}
