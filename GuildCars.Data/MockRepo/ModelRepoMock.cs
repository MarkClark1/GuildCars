using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuildCars.Data.Interfaces;
using System.Threading.Tasks;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Data.MockRepo
{
    public class ModelRepoMock : IModelRepository
    {
        private static List<Model> _models = new List<Model>()
        {
            new Model(){ DateAdded = new DateTime(2000,1,1), MakeId = 1, ModelId = 1, Name ="Impreza",UserId ="00000000-0000-0000-0000-000000000000"},
            new Model(){ DateAdded = new DateTime(2000,1,1), MakeId = 2, ModelId = 2, Name ="Focus",UserId ="00000000-0000-0000-0000-000000000000"}
        };
        public Model Create(Model model)
        {
            model.ModelId = GetNextId();
            _models.Add(model);
            return model;
        }

        public IEnumerable<ModelViewModel> GetAll()
        {
            List<ModelViewModel> models = new List<ModelViewModel>();
            MakeRepoMock repo = new MakeRepoMock();
            List<MakeViewModel> makes = repo.GetAll().ToList();
            foreach(var m in _models)
            {
                ModelViewModel current = new ModelViewModel()
                {
                    DateAdded = m.DateAdded,
                    EmailOfAdder = "TestMcgee@test.com",
                    ModelId = m.ModelId,
                    ModelName = m.Name,
                    MakeName = makes.FirstOrDefault(x => x.MakeId == m.MakeId).Name,

                };
                models.Add(current);
            }

            return models;
        }

        private int GetNextId()
        {
            return _models.Max(x => x.ModelId) + 1;
        }
    }
}
