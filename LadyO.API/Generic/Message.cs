using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Generic
{
    public class Message
    {
        #region Generic Message
        public static string OBJETO_NO_CORRESPONDE = "Objeto de llamada no corresponde.";
        public static string TOKEN_INVALIDO_EXPIRADO = "Token Expirado o Invalido.";
        public static string EMAIL_INVALIDO = "e-Mail ingresado es Invalido y/o Incorrecto.";
        #endregion
        public static string ISO_COUNTRIES_NO_EXISTE = "El iso viene vacio.";
        public static string NAME_COUNTRIES_NO_EXISTE = "El nombre viene vacio.";
        public static string NATIONALITY_COUNTRIES_NO_EXISTE = "El nationality viene vacio.";
        public static string ID_COUNTRIES_NO_EXISTE = "El pais que usted quiere modificar no existe.";
        public static string ID_COUNTRIES_GETOBJECT_NO_EXISTE = "El pais que usted esta buscando no existe.";

        public static string LOGIN_USUARIO_NO_VALIDO = "Usuario no valido o sin acceso - Contactar al Administrador.";
        
    }
}