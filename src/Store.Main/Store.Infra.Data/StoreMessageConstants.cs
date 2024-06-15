using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Infra.Data;
public static class StoreMessageConstants
{
    public const string CreateError = $"Failure to save object:";
    public const string DeleteError = "Failure to delete object:";
    public const string UpdateError = "Failure to update object:";
    public const string CreateProcessStarted = "Try to create object with id:";
    public const string UpdateProcessStarted = "Try to update object with id:";
    public const string DeleteProcessStarted = "Try to delete object with id:";
}
