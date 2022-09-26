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
        public static string NAME_NO_EXISTE = "El nombre viene vacio.";
        public static string EMAIL_VACIO = "Campo eMail vacio - Campo no puede ser vacio, debe establecer usuario.";
        #endregion

        //COUNTRIES MESSAGES
        public static string ISO_COUNTRIES_NO_EXISTE = "El iso viene vacio.";
        public static string NATIONALITY_COUNTRIES_NO_EXISTE = "El nationality viene vacio.";
        public static string ID_COUNTRIES_NO_EXISTE = "El pais que usted quiere modificar no existe.";
        public static string ID_COUNTRIES_GETOBJECT_NO_EXISTE = "El pais que usted esta buscando no existe.";
        //STRUCTURETYPE MESSAGES
        public static string ID_STRUCTURETYPE_GETOBJECT_NO_EXISTE = "EL tipo de estructura que usted esta buscando no existe.";
        public static string ID_STRUCTURETYPE_NO_EXISTE = "EL tipo de estructura que usted quiere modificar no existe.";
        public static string STRUCTURETYPE_POSITION = "La posicion no puede ser negativa.";
        //STRUCTURE MESSAGES
        public static string ID_STRUCTURES_NO_EXISTE = "La estructura que usted quiere modificar no existe.";
        public static string ID_STRUCTURES_GETOBJECT_NO_EXISTE = "La estructura que usted esta buscando no existe.";
        //BRANCHES MESSAGES
        public static string ID_BRANCHES_GETOBJECT_NO_EXISTE = "La rama que usted esta buscando no existe.";
        public static string ID_BRANCHES_NO_EXISTE = "La rama que usted quiere modificar no existe.";
        //RELIGIONS MESSAGES
        public static string ID_RELIGIONS_GETOBJECT_NO_EXISTE = "La religion que usted esta buscando no existe.";
        public static string ID_RELIGIONS_NO_EXISTE = "La religion que usted quiere modificar no existe.";
        public static string RELIGIONS_CONFESION_OUTVALOR = "El valor de la confesion esta fuera de rango.";
        //POSITIONS MESSAGES
        public static string ID_POSITIONS_GETOBJECT_NO_EXISTE = "La posicion que usted esta buscando no existe.";
        public static string ID_POSITIONS_NO_EXISTE = "La posicion que usted quiere modificar no existe.";
        public static string ID_POSITIONS_STRUCTURETYPE_OBJ_NO_EXISTE = "No existe el tipo de estructura que usted puso.";
        //UNITATTRIBUTES MESSAGES
        public static string ID_UNITATTRIBUTES_GETOBJECT_NO_EXISTE = "Los atributos unitarios que usted esta buscando no existe.";
        public static string ID_UNITATTRIBUTES_REPEATED = "Los atributos unitarios que usted esta insertando ya existen.";
        public static string ID_UNITATTRIBUTES_STRUCTURE_NO_EXISTE = "No existe la estructura que usted puso.";
        public static string ID_UNITATTRIBUTES_BRANCH_NO_EXISTE = "No existe la rama que usted puso.";
        //REGIONS MESSAGES
        public static string REGIONS_NO_NAME = "Nombre de la Región no indicado.";
        public static string ID_REGIONS_NO_EXISTE = "La region que usted quiere modificar no existe.";
        public static string ID_REGIONS_GETOBJECT_NO_EXISTE = "La region que usted esta buscando no existe.";
        //SOCIOECONOMICS MESSAGE
        public static string ID_SOCIOECONOMICS_GETOBJECT_NO_EXISTE = "El tipo de socioeconomico que usted esta buscando no existe.";
        public static string ID_SOCIOECONOMICS_NO_EXISTE = "El tipo de socioeconomico que usted quiere modificar no existe.";
        //PROVINCES MESSAGES
        public static string ID_PROVINCES_GETOBJECT_NO_EXISTE = "La provincia que usted esta buscando no existe.";
        public static string ID_PROVINCES_NO_EXISTE = "La provincia que usted quiere modificar no existe.";
        public static string ID_PROVINCES_REGIONS_OBJ_NO_EXISTE = "No existe la region que usted puso.";
        //COMMUNES MESSAGES
        public static string ID_COMMUNES_GETOBJECT_NO_EXISTE = "La comuna que usted esta buscando no existe.";
        public static string ID_COMMUNES_NO_EXISTE = "La comuna que usted quiere modificar no existe.";
        public static string ID_COMMUNES_PROVINCES_OBJ_NO_EXISTE = "No existe la provincia que usted puso.";
        //GENDERS MESSAGES
        public static string ID_GENDERS_GETOBJECT_NO_EXISTE = "El genero que usted esta buscando no existe.";
        public static string ID_GENDERS_NO_EXISTE = "El genero que usted quiere modificar no existe.";
        //SEXES MESSAGES
        public static string ID_SEXES_GETOBJECT_NO_EXISTE = "El sexo que usted esta buscando no existe.";
        public static string ID_SEXES_NO_EXISTE = "El sexo que usted quiere modificar no existe.";


        public static string LOGIN_USUARIO_NO_VALIDO = "Usuario no valido o sin acceso - Contactar al Administrador.";
    }
}