﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace EjemploPruebasUnitariasXUnit.Aceptacion.Paises.PaisesPorNombre
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class PaisesPorNombreFeature : object, Xunit.IClassFixture<PaisesPorNombreFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "PaisesPorNombre.feature"
#line hidden
        
        public PaisesPorNombreFeature(PaisesPorNombreFeature.FixtureData fixtureData, EjemploPruebasUnitariasXUnit_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("es-AR"), "PaisesPorNombre", "\tPara obtener información de\r\n\tpaises cuyo nombre a partir de su\r\n\tnombre", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Búsqueda por nombre exacto con resultado")]
        [Xunit.TraitAttribute("FeatureTitle", "PaisesPorNombre")]
        [Xunit.TraitAttribute("Description", "Búsqueda por nombre exacto con resultado")]
        [Xunit.TraitAttribute("Category", "mytag")]
        public virtual void BusquedaPorNombreExactoConResultado()
        {
            string[] tagsOfScenario = new string[] {
                    "mytag"};
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Búsqueda por nombre exacto con resultado", null, new string[] {
                        "mytag"});
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given("que la API de terceros devolvera el codigo 200 y el json \'[{\"name\":\"Argentina\",\"c" +
                        "allingCodes\":[\"54\"],\"alpha2Code\":\"AR\",\"alpha3Code\":\"ARG\",\"region\":\"Americas\",\"su" +
                        "bregion\":\"South America\",\"population\":43590400,\"borders\":[\"BOL\",\"BRA\",\"CHL\",\"PRY" +
                        "\",\"URY\"]}]\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 9
 testRunner.When("se invoca el endpoint \'/pais/nombre\' con los parametros \'argentina\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Cuando ");
#line hidden
#line 10
 testRunner.Then("la API devuelve el codigo 200 y el json \'[{\"name\":\"Argentina\",\"callingCodes\":[\"54" +
                        "\"],\"alpha2Code\":\"AR\",\"alpha3Code\":\"ARG\",\"region\":\"Americas\",\"subregion\":\"South A" +
                        "merica\",\"population\":43590400,\"borders\":[\"BOL\",\"BRA\",\"CHL\",\"PRY\",\"URY\"]}]\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entonces ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="La API de terceros NO devuelve 200 o 204")]
        [Xunit.TraitAttribute("FeatureTitle", "PaisesPorNombre")]
        [Xunit.TraitAttribute("Description", "La API de terceros NO devuelve 200 o 204")]
        public virtual void LaAPIDeTercerosNODevuelve200O204()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("La API de terceros NO devuelve 200 o 204", null, ((string[])(null)));
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 14
 testRunner.Given("que la API de terceros devolvera el codigo 500", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 15
 testRunner.When("se invoca el endpoint \'/pais/nombre\' con los parametros \'argentina\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Cuando ");
#line hidden
#line 16
 testRunner.Then("la API devuelve codigo 500", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Entonces ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                PaisesPorNombreFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                PaisesPorNombreFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion