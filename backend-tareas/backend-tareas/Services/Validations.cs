namespace backend_tareas.Services
{
    public class Validations
    {
        public static bool DataValidation(object objValidate, string[] exceptions, ref string msgValidation)
        {
            if (objValidate == null)
            {
                msgValidation = "El objeto de entrada no tiene el formato correcto o es nulo.";
                return true;
            }

            Type t = objValidate.GetType();
            System.Reflection.PropertyInfo[] properties = t.GetProperties();

            bool flagException = false;
            if (exceptions != null)
            {
                flagException = true;
            }

            foreach (System.Reflection.PropertyInfo propItem in properties)
            {
                bool isException = false;
                if (flagException)
                {
                    isException = Array.Exists(exceptions, element => element == propItem.Name);
                }

                if (!isException)
                {
                    object valueItem = propItem.GetValue(objValidate, null);

                    if (valueItem == null)
                    {
                        msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                        return true;
                    }
                    else if (valueItem is string)
                    {
                        if (string.IsNullOrEmpty((string)valueItem))
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is byte)
                    {
                        if ((byte)valueItem <= 0)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is short)
                    {
                        if ((short)valueItem <= 0)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is int)
                    {
                        if ((int)valueItem <= 0)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is long)
                    {
                        if ((long)valueItem <= 0)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is double)
                    {
                        if ((double)valueItem <= 0)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                    else if (valueItem is DateTime)
                    {
                        if ((DateTime)valueItem == null)
                        {
                            msgValidation = string.Format("El campo {0} es requerido.", propItem.Name);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
