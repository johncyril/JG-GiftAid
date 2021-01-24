using Autofac;
using LiteDB;

namespace JG.FinTech.GiftAid.Data.IoC
{
    public class DataModule : Module
    {
        private string _connectionString;

        public DataModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new LiteDatabase(_connectionString)).As<ILiteDatabase>();
            builder.RegisterType<LiteRepository>().As<ILiteRepository>();
            builder.RegisterType<Repository>().As<IRepository>();
        }
    }
}
