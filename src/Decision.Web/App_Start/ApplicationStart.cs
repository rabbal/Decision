using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web.Mvc;
using CaptchaMvc.Infrastructure;
using Decision.Common.Controller;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.DomainClasses.Entities.Users;
using Decision.IocConfig;
using Decision.ServiceLayer.Contracts;
using Decision.ServiceLayer.Contracts.Common;
using ElmahEFLogger.CustomElmahLogger;
using EntityFramework.Audit;
using Microsoft.AspNet.SignalR;

namespace Decision.Web
{
    public static class ApplicationStart
    {
        public static void Config()
        {
            // disable response header for protection  attak
            MvcHandler.DisableMvcResponseHeader = true;

            // change captcha provider for using cookie
            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
            CaptchaUtils.ImageGenerator.Height = 50;
            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            //set current Filter factory as StructureMapFitlerProvider
            var filterProider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProider);
            FilterProviders.Providers.Add(ProjectObjectFactory.Container.GetInstance<StructureMapFilterProvider>());
            
            var defaultJsonFactory = ValueProviderFactories.Factories
                .OfType<JsonValueProviderFactory>().FirstOrDefault();
            var index = ValueProviderFactories.Factories.IndexOf(defaultJsonFactory);
            ValueProviderFactories.Factories.Remove(defaultJsonFactory);
            ValueProviderFactories.Factories.Insert(index, new JsonNetValueProviderFactory());
            
            foreach (var task in ProjectObjectFactory.Container.GetAllInstances<IRunAtInit>())
            {
                task.Execute();
            }

            GlobalHost.DependencyResolver = ProjectObjectFactory.Container.GetInstance<Microsoft.AspNet.SignalR.IDependencyResolver>();
            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            // DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ConfigEf();
        }

        #region Dbconfig

        static void ConfigEf()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, DataLayer.Migrations.Configuration>());
            //ProjectObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();

            // config audit when your application is starting up...
            var auditConfiguration = AuditConfiguration.Default;
            auditConfiguration.IncludeRelationships = false;
            auditConfiguration.LoadRelationships = false;
            auditConfiguration.DefaultAuditable = false;
            AuditConfiguration.Default.IsAuditable<User>();
            AuditConfiguration.Default.IsAuditable<Applicant>();
            AuditConfiguration.Default.IsAuditable<Article>();
            AuditConfiguration.Default.IsAuditable<EntireEvaluation>();
            
            //ad interception for logg EF errors
            DbInterception.Add(new ElmahEfInterceptor());
        }
        #endregion

    }
}