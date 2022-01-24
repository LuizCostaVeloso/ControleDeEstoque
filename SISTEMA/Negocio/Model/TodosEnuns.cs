using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Negocio.Model
{
    /// <summary>
    /// Este enumeration Cargo é onde vai ser definido o tipo de cargo que o
    /// funcionário exerce na empresa.
    /// </summary>
    public enum Cargo
    {
        [Description("---SELECIONE---")]
        selecione = 0,
        [Description("ATENDENTE")]
        ATENDENTE = 1,
        [Description("CAIXA")]
        CAIXA = 2,
        [Description("GERÊNTE")]
        GERENTE = 3,
        [Description("MECÂNICO")]
        MECANICO = 4,
        [Description("SERVIÇOS GERAIS")]
        SERVICOS_GERAIS = 5
    }//end EnumCargoFuncionario

    /// <summary>
    /// Este enumeration Sexo é onde vai ser definido se o Cliente é masculino ou feminino.
    /// </summary> 
    public enum Sexo
    {
         M = 0,
        F = 1 ,
        inexistente = 2
    }     

    /// <summary>
    /// Este enumeration apresentará as opções de estado para o usuário.
    /// </summary>
    public enum Uf
    {
        UF = 0,
        RO = 1,
        AC = 2,
        AL = 3,
        AP = 4,
        AM = 5,
        BA = 6,
        CE = 7,
        DF = 8,
        ES = 9,
        GO = 10,
        MA = 11,
        MT = 12,
        MS = 13,
        MG = 14,
        PA = 15,
        PB = 16,
        PR = 17,
        PE = 18,
        PI = 19,
        RJ = 20,
        RN = 21,
        RS = 22,
        RR = 23,
        SC = 24,
        SP = 25,
        SE = 26,
        TO = 27
    }

}//end namespace Model
