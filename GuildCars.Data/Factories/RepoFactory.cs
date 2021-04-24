using GuildCars.Data.Interfaces;
using GuildCars.Data.MockRepo;
using GuildCars.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Factories
{
    public static class RepoFactory
    {
        private static string _mode = Settings.GetMode();

        public static ISpecialRepository CreateSpecialRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new SpecialRepositoryADO();
                case "Mock":
                    return new SpecialRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }            
        }

        public static IVehicleRepository CreateVehicleRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new VehicleRepositoryADO();
                case "Mock":
                    return new VehicleRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }

        public static IMakeRepository CreateMakeRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new MakeRepositoryADO();
                case "Mock":
                    return new MakeRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }

        public static IModelRepository CreateModelRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new ModelRepositoryADO();
                case "Mock":
                    return new ModelRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }

        public static ISalesInfoRepository CreateSaleInfoRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new SalesInfoRepositoryADO();
                case "Mock":
                    return new SalesInfoRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }

        public static IContactRecordRepository CreateContactRecordRepo()
        {
            switch (_mode)
            {
                case "ADO":
                    return new ContactRecordRepositoryADO();
                case "Mock":
                    return new ContactRecordRepoMock();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }

        public static IUserRepository CreateUserRepo()
        {
            return new UserRepositoryADO();
        }
    }
}
