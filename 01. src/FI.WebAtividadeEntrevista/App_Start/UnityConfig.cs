using AutoMapper;
using FI.AtividadeEntrevista.BLL.Beneficiario;
using FI.AtividadeEntrevista.BLL.Cliente;
using FI.AtividadeEntrevista.BLL.Services;
using FI.AtividadeEntrevista.CONTRACTS.Beneficiario;
using FI.AtividadeEntrevista.CONTRACTS.Cliente;
using FI.AtividadeEntrevista.CONTRACTS.Validation;
using FI.WebAtividadeEntrevista.Configuration;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace FI.WebAtividadeEntrevista
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            // Registre suas dependências aqui
            container.RegisterType<IValidationService, ValidationService>();
            container.RegisterType<IBoCliente, BoCliente>();
            container.RegisterType<IBoBeneficiario, BoBeneficiario>();

            // Configuração do AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); // Adicione seus perfis de mapeamento aqui
            });

            // Criar e registrar uma instância única do IMapper
            IMapper mapper = new Mapper(config);
            container.RegisterInstance<IMapper>(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}