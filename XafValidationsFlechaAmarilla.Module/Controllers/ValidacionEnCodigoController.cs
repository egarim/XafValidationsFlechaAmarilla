using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafValidationsFlechaAmarilla.Module.BusinessObjects;

namespace XafValidationsFlechaAmarilla.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ValidacionEnCodigoController : ViewController
    {
        SimpleAction VerificacionDeReglasSinException;
        SimpleAction VerificarEmpleado;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public ValidacionEnCodigoController()
        {
            InitializeComponent();

            this.TargetObjectType = typeof(Employee);
            this.TargetViewType = ViewType.DetailView;

            VerificarEmpleado = new SimpleAction(this, "VerificacionDeEmpleado", "View");
            VerificarEmpleado.Caption = "Verificacion en codigo";
            VerificarEmpleado.Execute += VerificarEmpleado_Execute;


            VerificacionDeReglasSinException = new SimpleAction(this, "VerificacionDeReglasSinException", "View");
            VerificacionDeReglasSinException.Execute += VerificacionDeReglasSinException_Execute;
            VerificacionDeReglasSinException.Caption = "Verificacion de reglas sin Exception";


            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void VerificacionDeReglasSinException_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IRuleSet ruleSet = Validator.GetService(Application.ServiceProvider);
            var result=ruleSet.ValidateTarget(this.View.ObjectSpace, this.View.CurrentObject, "Process");


            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        private void VerificarEmpleado_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

          


            IRuleSet ruleSet = Validator.GetService(Application.ServiceProvider);
            ruleSet.Validate(this.View.ObjectSpace, this.View.CurrentObject, "Process");
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
