using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafValidationsFlechaAmarilla.Module.BusinessObjects
{

    //HACK devexpress criteria language https://docs.devexpress.com/CoreLibraries/4928/devexpress-data-library/criteria-language-syntax
    [DefaultClassOptions()]
    //[RuleCriteria("Employee_Age_Over18", DefaultContexts.Save,
    //       "DateDiffYear(BirthDate, LocalDateTimeToday()) >= 18",
    //       CustomMessageTemplate = "el empleado tiene que ser mayor a 18 axo")]

    [RuleCriteria("Cargo tiene que ser administrador",DefaultContexts.Save,"Cargo.Nombre = 'Administrador'",CustomMessageTemplate ="El cargo tiene que ser administrador",SkipNullOrEmptyValues =false)]
    public class Employee : BaseObject
    {
        bool validacionConSistemaExterno;
        Cargo cargo;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private string email;
        private decimal salary;

        public Employee(Session session) : base(session) { }

        //[RuleRequiredField("Employee_FirstName_Required", DefaultContexts.Save,
        //    CustomMessageTemplate = "El campo primer nombre debe estar lleno")]
        public string FirstName
        {
            get { return firstName; }
            set { SetPropertyValue(nameof(FirstName), ref firstName, value); }
        }

        public Cargo Cargo
        {
            get => cargo;
            set => SetPropertyValue(nameof(Cargo), ref cargo, value);
        }
        //[RuleRequiredField("Employee_LastName_Required", DefaultContexts.Save,
        //    CustomMessageTemplate = "Last name must not be empty.")]
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue(nameof(LastName), ref lastName, value); }
        }

        //[RuleRange("Employee_BirthDate_Range", DefaultContexts.Save, "1900-01-01", "2000-01-01",
        //    CustomMessageTemplate = "fecha de nacimiento debe ser entre 01/01/1900 y 01/01/2000.")]
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { SetPropertyValue(nameof(BirthDate), ref birthDate, value); }
        }

        //[RuleRegularExpression("Employee_Email_Regex", DefaultContexts.Save,
        //    @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
        //    CustomMessageTemplate = "Email format is not valid.")]
        public string Email
        {
            get { return email; }
            set { SetPropertyValue(nameof(Email), ref email, value); }
        }

        //[RuleValueComparison("Employee_Salary_Minimum", DefaultContexts.Save,
        //    ValueComparisonType.GreaterThanOrEqual, 1000,
        //    CustomMessageTemplate = "salario debe ser por lo menos $1000.")]
        public decimal Salary
        {
            get { return salary; }
            set { SetPropertyValue(nameof(Salary), ref salary, value); }
        }

        //[RuleStringComparison("Employee_FirstName_NotEqualAdmin", DefaultContexts.Save,
        //    StringComparisonType.NotEquals, "Admin",
        //    CustomMessageTemplate = "First name cannot be 'Admin'.")]
        public string AdminNameCheck
        {
            get { return FirstName; }
        }


        public DateTime AgeCheck
        {
            get { return BirthDate; } // Just a dummy property for validation
        }

        //[VisibleInListView(false)]
        //[VisibleInDetailView(false)]
        [RuleFromBoolProperty("El primer nombre tiene que ser oscar","Process",CustomMessageTemplate ="El primer nombre tiene que ser Oscar")]
        [Browsable(false)]
        public bool ValidacionConSistemaExterno
        {
            get
            {
                //TODO llamar a webservice 
                //var MiServicioDeValidacion=this.Session.ServiceProvider.GetService(typeof(MiServiceDeValidacion));
                var Resultado= this.Session.ExecuteScalar($"Select FirstName from Employee where FirstName = '{this.FirstName}'") as string;
                if (Resultado == "Oscar")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}