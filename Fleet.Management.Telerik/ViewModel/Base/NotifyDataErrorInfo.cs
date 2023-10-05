using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Fleet.Management.ViewModel;

/// <summary>
/// Provides functionality to provide errors for the object if it is in an invalid state.
/// </summary>
/// <typeparam name="T">The type of this instance.</typeparam>
public abstract partial class NotifyDataErrorInfo<T> : DisposableObservableObject
    where T : NotifyDataErrorInfo<T>
{
    private const string HasErrorsPropertyName = "HasErrors";

    private static readonly RuleCollection<T> rules = new();

    private Dictionary<string, List<ValidationResult>> errors;


    /// <summary>
    /// Gets the when errors changed observable event. Occurs when the validation errors have changed for a property or for the entire object. 
    /// </summary>
    /// <value>
    /// The when errors changed observable event.
    /// </value>
    public IObservable<string> WhenErrorsChanged
    {
        get
        {
            return Observable
                .FromEventPattern<DataErrorsChangedEventArgs>(
                    h => ErrorsChanged += h,
                    h => ErrorsChanged -= h)
                .Select(x => x.EventArgs.PropertyName);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the object has validation errors. 
    /// </summary>
    /// <value><c>true</c> if this instance has errors, otherwise <c>false</c>.</value>
    public new bool HasErrors
    {
        get
        {
            InitializeErrors();
            
            return base.HasErrors || errors.Count > 0;
        }
    }

    /// <summary>
    /// Gets the rules which provide the errors.
    /// </summary>
    /// <value>The rules this instance must satisfy.</value>
    protected static RuleCollection<T> Rules => rules;

    /// <summary>
    /// Gets the validation errors for the entire object.
    /// </summary>
    /// <returns>A collection of errors.</returns>
    public IEnumerable GetErrors() => GetErrors(null);

    /// <summary>
    /// Gets the validation errors for a specified property or for the entire object.
    /// </summary>
    /// <param name="propertyName">Name of the property to retrieve errors for. <c>null</c> to 
    /// retrieve all errors for this instance.</param>
    /// <returns>A collection of errors.</returns>
    public new IEnumerable<ValidationResult> GetErrors(string propertyName)
    {
        Debug.Assert(
            string.IsNullOrEmpty(propertyName) ||
            (GetType().GetRuntimeProperty(propertyName) != null),
            "Check that the property name exists for this instance.");

        InitializeErrors();

        List<ValidationResult> result = base.GetErrors(propertyName).ToList();
        if (string.IsNullOrEmpty(propertyName))
        {

            foreach (KeyValuePair<string, List<ValidationResult>> keyValuePair in errors)
            {
                result.AddRange(keyValuePair.Value);
            }
        }
        else
        {
            if (errors.ContainsKey(propertyName))
            {
                result.AddRange(errors[propertyName]);
            }
        }

        return result;
    }


    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="e">PropertyChangedEventArgs with the Name of the property.</param>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (string.IsNullOrEmpty(e.PropertyName))
        {
            ApplyRules();
        }
        else
        {
            ApplyRules(e.PropertyName);
        }

        base.OnPropertyChanged(new PropertyChangedEventArgs(HasErrorsPropertyName));
    }

    protected void OnRulesPropertyCanged()
    {
        foreach (var prop in Rules.Select(r => r.PropertyName))
        {
            OnPropertyChanged(prop);
        }
    }

    /// <summary>
    /// Applies all rules to this instance.
    /// </summary>
    private void ApplyRules()
    {
        InitializeErrors();

        foreach (string propertyName in rules.Select(x => x.PropertyName))
        {
            ApplyRules(propertyName);
        }
    }

    /// <summary>
    /// Applies the rules to this instance for the specified property.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    private void ApplyRules(string propertyName)
    {
        InitializeErrors();

        List<ValidationResult> propertyErrors = rules.Apply((T)this, propertyName).ToList();

        if (propertyErrors.Count > 0)
        {
            if (errors.TryGetValue(propertyName, out List<ValidationResult> value))
            {
                value.Clear();
            }
            else
            {
                errors[propertyName] = new List<ValidationResult>();
            }

            errors[propertyName].AddRange(propertyErrors);
            //this.OnErrorsChanged(propertyName);
        }
        else if (errors.ContainsKey(propertyName))
        {
            errors.Remove(propertyName);
            //this.OnErrorsChanged(propertyName);
        }
    }

    /// <summary>
    /// Initializes the errors and applies the rules if not initialized.
    /// </summary>
    private void InitializeErrors()
    {
        if (errors == null)
        {
            errors = new Dictionary<string, List<ValidationResult>>();

            ApplyRules();
        }
    }
}
