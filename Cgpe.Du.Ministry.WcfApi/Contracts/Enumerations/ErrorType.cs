namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    /// <summary>
    /// 
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Servicio no disponible
        /// </summary>
        TEC_1001, 
        /// <summary>
        /// Esquema de Datos inválido
        /// Dado a que la validación de los formatos se realiza por WCF,
        /// si el formato no es valido, fallara antes de poder ser gestionado.
        /// </summary>
        TEC_1002, 
        /// <summary>
        /// Numero de petición inválido
        /// </summary>
        FUN_1003, 
        /// <summary>
        /// Numero de página inválido
        /// </summary>
        FUN_1004, 
        /// <summary>
        /// Fecha Desde inválida
        /// Solo hay que validar el formato de la fecha.
        /// Dado a que la validación de los formatos se realiza por WCF,
        /// si el formato no es valido, fallara antes de poder ser gestionado.
        /// </summary>
        FUN_1005, 
        /// <summary>
        /// Destino/Origen Petición incorrecto
        /// </summary>
        FUN_1006,
        /// <summary>
        /// Otros errores técnicos producidos en el servicio
        /// </summary>
        TEC_9100x,
        /// <summary>
        /// Otros errores funcionales producidos en el servicio
        /// </summary>
        FUN_9100x
    }

}